namespace CSharpInteractive.Core;

using HostApi;
using Immutype;

[Target]
internal record ProcessInfo(
    IStartInfo StartInfo,
    IProcessMonitor Monitor,
    int RunId,
    Action<Output>? Handler = null)
{
    private static int _lastRunId;

    public static int CreateRunId() =>
        Interlocked.Increment(ref _lastRunId);
}