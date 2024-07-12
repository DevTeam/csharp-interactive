namespace CSharpInteractive.Core;

using HostApi;
using Immutype;

[Target]
internal record ProcessInfo(
    IStartInfo StartInfo,
    IProcessMonitor Monitor,
    Action<Output>? Handler = default)
{
    private static int _lastRunId;
    private readonly int _runId = Interlocked.Increment(ref _lastRunId);

    public int RunId => _runId;
}