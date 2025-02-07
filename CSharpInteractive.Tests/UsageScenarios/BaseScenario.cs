// ReSharper disable MemberCanBeProtected.Global

// ReSharper disable MemberCanBeMadeStatic.Global
namespace CSharpInteractive.Tests.UsageScenarios;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using CSharpInteractive;
using NuGet.Versioning;
using Environment = System.Environment;

[SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
public class BaseScenario : IHost, IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly string _tempDir;
    private readonly string _prevCurDir;
    private readonly StringBuilder _outputText = new();

    public BaseScenario(ITestOutputHelper output)
    {
        _output = output;
        Composition.Shared.Root.TestEnvironment.IsTesting = true;
        Composition.Shared.Root.TestEnvironment.ExitCode = null;
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
        Directory.CreateDirectory(_tempDir);
        _prevCurDir = Environment.CurrentDirectory;
        Environment.CurrentDirectory = _tempDir;
        Composition.Shared.Root.ConsoleHandler.OutputHandler += ConsoleHandlerOnOutputHandler;
        Composition.Shared.Root.ConsoleHandler.ErrorHandler += ConsoleHandlerOnErrorHandler;
    }
    
    public int? ExpectedExitCode { get; set; } = 0;

    [SuppressMessage("Performance", "CA1822:Пометьте члены как статические")]
    public bool HasSdk(string sdkVersion)
    {
        var versions = new List<NuGetVersion>();
        new DotNetSdkCheck()
            .Run(output =>
            {
                if (output.Line.Split(' ', StringSplitOptions.RemoveEmptyEntries) is ["Microsoft.NETCore.App", var versionStr, ..]
                    && NuGetVersion.TryParse(versionStr, out var version))
                {
                    versions.Add(version);
                }
            })
            .EnsureSuccess();

        var sdkVersionValue = NuGetVersion.Parse(sdkVersion);
        return versions.Any(i => i >= sdkVersionValue);
    }

    // ReSharper disable once MemberCanBeProtected.Global
    public IHost Host => this;

    public IReadOnlyList<string> Args { get; } = new List<string>
    {
        "Arg1",
        "Arg2"
    };

    public IProperties Props { get; } = new Properties();

    public T GetService<T>() => Composition.Shared.Root.Host.GetService<T>();

    public void WriteLine() => Composition.Shared.Root.Host.WriteLine();

    public void WriteLine<T>(T line, Color color = Color.Default) => Composition.Shared.Root.Host.WriteLine(line, color);

    public void Error(string? error, string? errorId = null) => Composition.Shared.Root.Host.Error(error, errorId);

    public void Warning(string? warning) => Composition.Shared.Root.Host.Warning(warning);

    public void Summary(string? summary) => Composition.Shared.Root.Host.Summary(summary);

    public void Info(string? text) => Composition.Shared.Root.Host.Info(text);

    public void Trace(string? trace, string? origin = null) => Composition.Shared.Root.Host.Trace(trace, origin);

    private class Properties : IProperties
    {
        private readonly Dictionary<string, string> _dict = new()
        {
            {"version", "1.1.5"}
        };

        public int Count => _dict.Count;

        public string this[string key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _dict.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool TryGetValue(string key, out string value) => _dict.TryGetValue(key, out value!);
    }

    void IDisposable.Dispose()
    {
        Composition.Shared.Root.ConsoleHandler.OutputHandler -= ConsoleHandlerOnOutputHandler;
        Composition.Shared.Root.ConsoleHandler.ErrorHandler -= ConsoleHandlerOnErrorHandler;
        if (_outputText.Length > 0)
        {
            _output.WriteLine(_outputText.ToString());
        }

        try
        {
            Directory.Delete(_tempDir, true);
        }
        catch
        {
            // ignored
        }

        Environment.CurrentDirectory = _prevCurDir;
        if (ExpectedExitCode is {} expectedExitCode && Composition.Shared.Root.TestEnvironment.ExitCode is { } exitCode)
        {
            exitCode.ShouldBe(expectedExitCode);
        }
    }
    
    private void ConsoleHandlerOnOutputHandler(object? sender, string text) => 
        AddOutput(text, false);
    
    private void ConsoleHandlerOnErrorHandler(object? sender, string text) => 
        AddOutput(text, true);

    private void AddOutput(string text, bool isError)
    {
        _outputText.Append(text.Replace("\x001B", ""));
        text = _outputText.ToString();
        do
        {
            var endOfLine = text.IndexOf(Environment.NewLine, StringComparison.Ordinal);
            if (endOfLine == -1)
            {
                break;
            }

            var line = text[..endOfLine];
            _output.WriteLine((isError ? "ERR: " : "STD: ") + line);
            text = text[(endOfLine + Environment.NewLine.Length)..];
        } while (text.Length > 0);

        _outputText.Clear();
        _outputText.Append(text);
    }
}