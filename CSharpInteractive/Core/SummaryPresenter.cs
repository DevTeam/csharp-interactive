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
    internal const string RunningSucceeded = "Running succeeded.";
    internal const string RunningSucceededWithWarnings = "Running succeeded with warnings.";
    internal const string RunningFailed = "Running FAILED.";

    public void Show(Summary summary)
    {
        log.Info();
        if (!statistics.IsEmpty)
        {
            log.Info(new Text("Summary:", Color.Header));
            statisticsPresenter.Show(statistics);
        }

        var statisticsItems = statistics.Items;
        if (summary.Success == false || statisticsItems.Any(i => i.Type == StatisticsType.Error))
        {
            log.Info(new Text(RunningFailed, Color.Error));
            return;
        }

        if (statisticsItems.Any(i => i.Type == StatisticsType.Warning))
        {
            log.Info(new Text(RunningSucceededWithWarnings, Color.Warning));
            return;
        }

        log.Info(new Text(RunningSucceeded, Color.Success));
    }
}