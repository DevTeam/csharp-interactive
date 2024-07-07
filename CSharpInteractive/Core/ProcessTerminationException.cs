namespace CSharpInteractive.Core;

[Serializable]
internal class ProcessTerminationException(int exitCode): OperationCanceledException
{
    public int ExitCode { get; } = exitCode;
}