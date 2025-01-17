namespace CSharpInteractive.Core;

using HostApi;

internal interface IProcessMonitor
{
    void Started(IStartInfo startInfo, int processId);

    ProcessResult Finished(ProcessInfo processInfo, long elapsedMilliseconds, ProcessState state, int? exitCode = null, Exception? error = null);
}