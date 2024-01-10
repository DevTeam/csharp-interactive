namespace CSharpInteractive;

using System.Diagnostics;

[ExcludeFromCodeCoverage]
internal class Debugger(
    ILog<Debugger> log,
    IEnvironmentVariables environmentVariables) : IActive
{

    public IDisposable Activate()
    {
        if (environmentVariables.GetEnvironmentVariable("DEBUG_CSI") == null)
        {
            return Disposable.Empty;
        }

        log.Warning($"\nWaiting for debugger in process [{System.Environment.ProcessId}] \"{Process.GetCurrentProcess().ProcessName}\".");
        while (!System.Diagnostics.Debugger.IsAttached)
        {
            Thread.Sleep(100);
        }

        System.Diagnostics.Debugger.Break();

        return Disposable.Empty;
    }
}