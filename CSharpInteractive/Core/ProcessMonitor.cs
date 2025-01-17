// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class ProcessMonitor(
    ILog<ProcessMonitor> log,
    IEnvironment environment,
    IStartInfoDescription startInfoDescription) : IProcessMonitor
{
    private int? _processId;

    public void Started(IStartInfo startInfo, int processId)
    {
        _processId = processId;
        var executable = new List<Text>(startInfoDescription.GetDescriptionText(startInfo, _processId))
        {
            new(" started "),
            new(startInfo.ExecutablePath.Escape())
        };

        foreach (var arg in startInfo.Args)
        {
            executable.Add(Text.Space);
            executable.Add(new Text(arg.Escape()));
        }

        log.Info(executable.ToArray());

        var workingDirectory = startInfo.WorkingDirectory;
        if (string.IsNullOrWhiteSpace(workingDirectory))
        {
            workingDirectory = environment.GetPath(SpecialFolder.Working);
        }

        if (!string.IsNullOrWhiteSpace(workingDirectory))
        {
            log.Info(new Text("in directory: "), new Text(workingDirectory.Escape()));
        }
    }

    public ProcessResult Finished(ProcessInfo processInfo, long elapsedMilliseconds, ProcessState state, int? exitCode = null, Exception? error = null) =>
        new(
            processInfo,
            state,
            elapsedMilliseconds,
            GetFooter(processInfo.StartInfo, exitCode, elapsedMilliseconds, state).ToArray(),
            exitCode,
            error);

    private IEnumerable<Text> GetFooter(IStartInfo startInfo, int? exitCode, long elapsedMilliseconds, ProcessState? state)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        string? stateText;
        Color stateColor;
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (state)
        {
            case ProcessState.FailedToStart:
                stateText = "failed to start";
                stateColor = Color.Error;
                break;

            case ProcessState.Canceled:
                stateText = "canceled";
                stateColor = Color.Warning;
                break;

            default:
                stateText = "finished";
                stateColor = Color.Success;
                break;
        }

        foreach (var text in startInfoDescription.GetDescriptionText(startInfo, _processId))
        {
            yield return text;
        }

        yield return Text.Space;
        yield return new Text(stateText, stateColor);
        yield return new Text($" (in {elapsedMilliseconds} ms)");
        if (exitCode.HasValue)
        {
            yield return new Text($" with exit code {exitCode}");
        }

        yield return new Text(".");
    }
}