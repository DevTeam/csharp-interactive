// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class ProcessResultHandler(
    ILog<ProcessResultHandler> log,
    IExitTracker exitTracker) : IProcessResultHandler
{
    public void Handle<T>(ProcessResult result, Action<T>? handler)
    {
        if (exitTracker.IsTerminating)
        {
            return;
        }

        var description = result.Description;
        if (result.Error != null)
        {
            description = description + Text.Space + new Text(result.Error.Message);
        }

        if (handler == null)
        {
            switch (result.State)
            {
                case ProcessState.FailedToStart:
                    log.Error(ErrorId.Process, description);
                    break;

                case ProcessState.Canceled:
                    log.Warning(description);
                    break;

                case ProcessState.Finished:
                default:
                    log.Summary(description);
                    break;
            }
        }
        else
        {
            log.Info(description);
        }
    }
}