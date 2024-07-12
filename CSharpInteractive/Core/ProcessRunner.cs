// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ForCanBeConvertedToForeach
namespace CSharpInteractive.Core;

using System.Diagnostics;
using HostApi;

internal class ProcessRunner(Func<IProcessManager> processManagerFactory) : IProcessRunner
{
    public ProcessResult Run(ProcessInfo processInfo, TimeSpan timeout)
    {
        var processManager = processManagerFactory();
        var process = new Process(processInfo, processManager);
        if (!process.TryStart(out var processResult))
        {
            return processResult;
        }

        var finished = processManager.WaitForExit(timeout);
        return process.Finish(finished);
    }

    public async Task<ProcessResult> RunAsync(ProcessInfo processInfo, CancellationToken cancellationToken)
    {
        var processManager = processManagerFactory();
        var process = new Process(processInfo, processManager);
        if (!process.TryStart(out var processResult))
        {
            return processResult;
        }

        try
        {
            await processManager.WaitForExitAsync(cancellationToken);
            var finished = !cancellationToken.IsCancellationRequested;
            return process.Finish(finished);
        }
        catch (OperationCanceledException canceledException)
        {
            return process.Finish(false, canceledException);
        }
        catch (Exception error)
        {
            process.Finish(false, error);
            throw;
        }
    }

    private class Process(ProcessInfo processInfo, IProcessManager processManager)
    {
        private IProcessManager ProcessManager { get; } = processManager;

        private Stopwatch Stopwatch { get; } = new();

        private IStartInfo StartInfo => processInfo.StartInfo;

        private IProcessMonitor Monitor => processInfo.Monitor;

        private Action<Output>? Handler => processInfo.Handler;
        
        public bool TryStart([MaybeNullWhen(true)] out ProcessResult processResult)
        {
            if (Handler != default)
            {
                ProcessManager.OnOutput += Handler;
            }

            Stopwatch.Start();
            if (!ProcessManager.Start(StartInfo, out var error))
            {
                Stopwatch.Stop();
                {
                    processResult = Monitor.Finished(processInfo, Stopwatch.ElapsedMilliseconds, ProcessState.FailedToStart, default, error);
                    return false;
                }
            }

            Monitor.Started(StartInfo, ProcessManager.Id);
            processResult = default;
            return true;
        }
    
        public ProcessResult Finish(bool finished, Exception? error = default)
        {
            if (finished)
            {
                Stopwatch.Stop();
                return Monitor.Finished(processInfo, Stopwatch.ElapsedMilliseconds, ProcessState.Finished, ProcessManager.ExitCode, error);
            }

            ProcessManager.Kill();
            Stopwatch.Stop();
            return Monitor.Finished(processInfo, Stopwatch.ElapsedMilliseconds, ProcessState.Canceled);
        }
    }
}