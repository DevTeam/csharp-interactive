namespace CSharpInteractive.Tests.Integration.Core;

internal interface IProcessResult
{
    int ExitCode { get; }

    IReadOnlyCollection<string> StdOut { get; }

    IReadOnlyCollection<string> StdErr { get; }
}