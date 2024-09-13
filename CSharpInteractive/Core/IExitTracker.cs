namespace CSharpInteractive.Core;

internal interface IExitTracker
{
    bool IsTerminating { get; }

    IDisposable Track();

    void Exit(int exitCode);
}