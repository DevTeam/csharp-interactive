// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages;

internal class BuildContext : IBuildContext
{
    private readonly List<BuildMessage> _errors = [];
    private readonly List<BuildMessage> _warnings = [];
    private readonly List<TestResult> _tests = [];
    private readonly HashSet<BuildMessage.TestKey> _testKeys = [];
    private readonly Dictionary<BuildMessage.TestKey, TestContext> _currentTests = new();

    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public IReadOnlyList<BuildMessage> ProcessMessage(in Output output, IServiceMessage message) => (
        message.Name.ToLowerInvariant() switch
        {
            "teststdout" => OnStdOut(message, output.StartInfo, output.ProcessId),
            "teststderr" => OnStdErr(message, output.StartInfo, output.ProcessId),
            "testfinished" => OnTestFinished(message),
            "testignored" => OnTestIgnored(message),
            "testfailed" => OnTestFailed(message),
            "message" => OnMessage(message),
            "buildproblem" => OnBuildProblem(message),
            _ => []
        }).ToArray();

    public IReadOnlyList<BuildMessage> ProcessOutput(in Output output)
    {
        BuildMessage message;
        if (output.IsError)
        {
            message = new BuildMessage(BuildMessageState.StdError, default, output.Line);
            _errors.Add(message);
        }
        else
        {
            message = new BuildMessage(BuildMessageState.StdOut, default, output.Line);
        }

        return new[] {message};
    }

    public IBuildResult Create(IStartInfo startInfo, int? exitCode) =>
        new BuildResult(startInfo,
            _errors.AsReadOnly(),
            _warnings.AsReadOnly(),
            _tests.AsReadOnly(),
            exitCode);

    private IEnumerable<BuildMessage> OnStdOut(IServiceMessage message, IStartInfo startInfo, int processId)
    {
        var testKey = BuildMessage.CreateKey(message);
        var output = message.GetValue("out") ?? string.Empty;
        GetTestContext(testKey).AddStdOut(startInfo, processId, output);
        yield return new BuildMessage(BuildMessageState.StdOut).WithText(output);
    }

    private IEnumerable<BuildMessage> OnStdErr(IServiceMessage message, IStartInfo info, int processId)
    {
        var testKey = BuildMessage.CreateKey(message);
        var output = message.GetValue("out") ?? string.Empty;
        GetTestContext(testKey).AddStdErr(info, processId, output);
        var buildMessage = new BuildMessage(BuildMessageState.StdError).WithText(output);
        _errors.Add(buildMessage);
        yield return buildMessage;
    }
    
    private IEnumerable<BuildMessage> OnTestFinished(IServiceMessage message)
    {
        var testKey = BuildMessage.CreateKey(message);
        if (_testKeys.Remove(testKey))
        {
            yield break;
        }

        var ctx = GetTestContext(testKey, true);
        var durationStrMs = message.GetValue("duration");
        var duration = TimeSpan.Zero;
        if (!string.IsNullOrWhiteSpace(durationStrMs) && int.TryParse(durationStrMs, out var durationMs))
        {
            duration = TimeSpan.FromMilliseconds(durationMs);
        }
        
        _tests.Add(BuildMessage.CreateResult(testKey, message, TestState.Finished).WithDuration(duration).WithOutput(ctx.Output));
    }

    private IEnumerable<BuildMessage> OnTestIgnored(IServiceMessage message)
    {
        var testKey = BuildMessage.CreateKey(message);
        _testKeys.Add(testKey);
        var ctx = GetTestContext(testKey, true);
        _tests.Add(BuildMessage.CreateResult(testKey, message, TestState.Ignored).WithMessage(message.GetValue("message") ?? string.Empty).WithOutput(ctx.Output));
        yield break;
    }

    private IEnumerable<BuildMessage> OnTestFailed(IServiceMessage message)
    {
        var testKey = BuildMessage.CreateKey(message);
        _testKeys.Add(testKey);
        var ctx = GetTestContext(testKey, true);
        _tests.Add(BuildMessage.CreateResult(testKey, message, TestState.Failed).WithMessage(message.GetValue("message") ?? string.Empty).WithDetails(message.GetValue("details") ?? string.Empty).WithOutput(ctx.Output));
        yield break;
    }
    
    
    private IEnumerable<BuildMessage> OnMessage(IServiceMessage message)
    {
        var text = message.GetValue("text") ?? string.Empty;
        var state = message.GetValue("status").ToUpperInvariant() switch
        {
            "WARNING" => BuildMessageState.Warning,
            "FAILURE" => BuildMessageState.Failure,
            "ERROR" => BuildMessageState.StdError,
            _ => BuildMessageState.StdOut
        };
        
        var buildMessage = CreateMessage(message, state, text);
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        if (!string.IsNullOrWhiteSpace(buildMessage.Text))
        {
            switch (state)
            {
                case BuildMessageState.Warning:
                    _warnings.Add(buildMessage);
                    break;

                case BuildMessageState.Failure:
                case BuildMessageState.StdError:
                // ReSharper disable once UnreachableSwitchCaseDueToIntegerAnalysis
                case BuildMessageState.BuildProblem:
                    _errors.Add(buildMessage);
                    break;
            }
        }

        yield return buildMessage;
    }

    private IEnumerable<BuildMessage> OnBuildProblem(IServiceMessage message)
    {
        var description = message.GetValue("description") ?? string.Empty;
        var buildMessage = CreateMessage(message, BuildMessageState.BuildProblem, description);
        _errors.Add(buildMessage);
        yield return buildMessage;
    }

    private static BuildMessage CreateMessage(IServiceMessage message, BuildMessageState state, string text)
    {
        var buildMessage = new BuildMessage(
            state,
            default,
            text,
            message.GetValue("errorDetails") ?? string.Empty,
            message.GetValue("code") ?? string.Empty,
            message.GetValue("file") ?? string.Empty,
            message.GetValue("subcategory") ?? string.Empty,
            message.GetValue("projectFile") ?? string.Empty,
            message.GetValue("senderName") ?? string.Empty);
        
        if (int.TryParse(message.GetValue("columnNumber"), out var columnNumber))
        {
            buildMessage = buildMessage.WithColumnNumber(columnNumber);
        }
        
        if (int.TryParse(message.GetValue("endColumnNumber"), out var endColumnNumber))
        {
            buildMessage = buildMessage.WithEndColumnNumber(endColumnNumber);
        }
        
        if (int.TryParse(message.GetValue("lineNumber"), out var lineNumber))
        {
            buildMessage = buildMessage.WithLineNumber(lineNumber);
        }
        
        if (int.TryParse(message.GetValue("endLineNumber"), out var endLineNumber))
        {
            buildMessage = buildMessage.WithEndLineNumber(endLineNumber);
        }
        
        if (Enum.TryParse<DotNetMessageImportance>(message.GetValue("importance"), out var importance))
        {
            buildMessage = buildMessage.WithImportance(importance);
        }

        return buildMessage;
    }

    private TestContext GetTestContext(BuildMessage.TestKey testKey, bool remove = false)
    {
        lock (_currentTests)
        {
            if (!_currentTests.TryGetValue(testKey, out var testContext))
            {
                testContext = new TestContext();
                if (!remove)
                {
                    _currentTests.Add(testKey, testContext);
                }
            }
            else
            {
                if (remove)
                {
                    _currentTests.Remove(testKey);
                }
            }
            
            return testContext;
        }
    }

    private class TestContext
    {
        public readonly List<Output> Output = [];

        public void AddStdOut(IStartInfo info, int processId, string? text)
        {
            if (text != default)
            {
                Output.Add(new Output(info, false, text, processId));
            }
        }

        public void AddStdErr(IStartInfo info, int processId, string? error)
        {
            if (error != default)
            {
                Output.Add(new Output(info, true, error, processId));
            }
        }
    }
}