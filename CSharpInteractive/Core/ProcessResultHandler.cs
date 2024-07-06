// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

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
        if (result.Error != default)
        {
            description = description + Text.Space + new Text(result.Error.Message);
        }

        if (handler == default)
        {
            switch (result.State)
            {
                case ProcessState.Failed:
                    log.Error(ErrorId.Process, description);
                    break;

                case ProcessState.Canceled:
                    log.Warning(description);
                    break;

                case ProcessState.Finished:
                default:
                    log.Info(description);
                    break;
            }
        }
        else
        {
            log.Info(description);
        }
    }
}