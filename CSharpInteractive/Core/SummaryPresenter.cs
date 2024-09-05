// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
namespace CSharpInteractive.Core;

using HostApi;

internal class SummaryPresenter(
    ILog<SummaryPresenter> log,
    IStatistics statistics,
    IPresenter<IStatistics> statisticsPresenter)
    : IPresenter<Summary>
{
    private static readonly Text[] Tab = [Text.Tab];
    internal const string RunningSucceeded = "Running succeeded.";
    internal const string RunningSucceededWithWarnings = "Running succeeded with warnings.";
    internal const string RunningFailed = "Running FAILED.";
    
    public void Show(Summary summary)
    {
        log.Info();
        if (!statistics.IsEmpty)
        {
            log.Info(new Text("Summary:", Color.Header));
            foreach (var commandLineInfo in statistics.CommandLines)
            {
                log.Info(commandLineInfo.ProcessResult.Description.AddPrefix(_ => Tab));
            }
            
            statisticsPresenter.Show(statistics);
        }

        if (summary.Success == false || statistics.Errors.Count > 0)
        {
            log.Info(new Text(RunningFailed, Color.Error));
            return;
        }
        
        if (statistics.Warnings.Count > 0)
        {
            log.Info(new Text(RunningSucceededWithWarnings, Color.Warning));
            return;
        }
        
        log.Info(new Text(RunningSucceeded, Color.Success));
    }
}