// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

internal class SummaryPresenter(
    ILog<SummaryPresenter> log,
    IStatistics statistics,
    IPresenter<IStatistics> statisticsPresenter) : IPresenter<Summary>
{
    internal const string RunningSucceeded = "Running succeeded.";
    internal const string RunningSucceededWithWarnings = "Running succeeded with warnings.";
    internal const string RunningFailed = "Running FAILED.";
    
    public void Show(Summary summary)
    {
        statisticsPresenter.Show(statistics);
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