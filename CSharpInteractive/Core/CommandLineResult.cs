namespace CSharpInteractive.Core;

using System.Text;
using HostApi;

internal record CommandLineResult(
    IStartInfoDescription StartInfoDescription,
    IStartInfo StartInfo,
    ProcessState State,
    long ElapsedMilliseconds,
    int? ExitCode = default,
    Exception? Error = default)
    : ICommandLineResult, ISuccessDeterminant
{
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(StartInfoDescription.GetDescription(StartInfo));
        if (sb.Length == 0)
        {
            sb.Append("Process");
        }

        sb.Append(' ');
        switch (State)
        {
            case ProcessState.FailedToStart:
                sb.Append("Failed to start");
                break;

            case ProcessState.Canceled:
                sb.Append("canceled");
                break;

            default:
            case ProcessState.Finished:
                sb.Append("finished");
                break;
        }

        // ReSharper disable once InvertIf
        if (ExitCode is { } exitCode)
        {
            sb.Append(" with exit code ");
            sb.Append(exitCode);
        }

        return sb.ToString();
    }

    public bool? IsSuccess =>
        ExitCode == 0
        && Error is null
        && State == ProcessState.Finished;
}