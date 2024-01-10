// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;
using Pure.DI;

internal class BuildMessageLogWriter(
    ILog<BuildMessageLogWriter> log,
    [Tag("Default")] IStdOut stdOut,
    [Tag("Default")] IStdErr stdErr) : IBuildMessageLogWriter
{
    public void Write(BuildMessage message)
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
                log.Warning(message.Text);
                break;

            case BuildMessageState.Failure:
            case BuildMessageState.BuildProblem:
                log.Error(ErrorId.Build, message.Text);
                break;
        }
    }
}