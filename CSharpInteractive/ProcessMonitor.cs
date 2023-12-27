// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

internal class ProcessMonitor(
    ILog<ProcessMonitor> log,
    IEnvironment environment) : IProcessMonitor
{
    private int? _processId;

    public void Started(IStartInfo startInfo, int processId)
    {
        _processId = processId;
        var executable = new List<Text>
        {
            new($"{startInfo.GetDescription(processId)} process started ", Color.Highlighted),
            new(startInfo.ExecutablePath.EscapeArg())
        };

        foreach (var arg in startInfo.Args)
        {
            executable.Add(Text.Space);
            executable.Add(new Text(arg.EscapeArg()));
        }

        log.Info(executable.ToArray());

        var workingDirectory = startInfo.WorkingDirectory;
        if (string.IsNullOrWhiteSpace(workingDirectory))
        {
            workingDirectory = environment.GetPath(SpecialFolder.Working);
        }

        if (!string.IsNullOrWhiteSpace(workingDirectory))
        {
            log.Info(new Text("in directory: "), new Text(workingDirectory.EscapeArg()));
        }
    }

    public ProcessResult Finished(IStartInfo startInfo, long elapsedMilliseconds, ProcessState state, int? exitCode = default, Exception? error = default) => 
        new(
            startInfo,
            state,
            elapsedMilliseconds,
            GetFooter(startInfo,exitCode, elapsedMilliseconds, state).ToArray(),
            exitCode,
            error);

    private IEnumerable<Text> GetFooter(IStartInfo startInfo, int? exitCode, long elapsedMilliseconds, ProcessState? state)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        var stateText = state switch
        {
            ProcessState.Failed => exitCode.HasValue ? "failed" : "failed to start",
            ProcessState.Canceled => "canceled",
            _ => "finished"
        };

        yield return new Text($"{startInfo.GetDescription(_processId)} process ", Color.Highlighted);
        yield return new Text(stateText, Color.Highlighted);
        yield return new Text($" (in {elapsedMilliseconds} ms)");
        if (exitCode.HasValue)
        {
            yield return new Text($" with exit code {exitCode}");
        }

        yield return new Text(".");
    }
}