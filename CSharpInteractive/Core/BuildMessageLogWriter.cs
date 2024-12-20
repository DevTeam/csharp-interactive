// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;
using Pure.DI;
using static Pure.DI.Tag;

internal class BuildMessageLogWriter(
    ILog<BuildMessageLogWriter> log,
    [Tag(Base)] IStdOut stdOut,
    [Tag(Base)] IStdErr stdErr)
    : IBuildMessageLogWriter
{
    public void Write(ProcessInfo processInfo, BuildMessage message)
    {
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (message.State)
        {
            case BuildMessageState.StdOut:
                stdOut.WriteLine([new Text(message.Text)]);
                break;

            case BuildMessageState.StdError:
                stdErr.WriteLine([new Text(message.Text)]);
                break;

            case BuildMessageState.Warning:
                var warning = new Text(message.Text, Color.Warning);
                log.Warning(warning);
                break;

            case BuildMessageState.Failure:
            case BuildMessageState.BuildProblem:
                var error = new Text(message.Text, Color.Error);
                log.Error(ErrorId.Build, error);
                break;
        }
    }
}