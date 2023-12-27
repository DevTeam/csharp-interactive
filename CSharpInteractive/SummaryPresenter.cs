// ReSharper disable UnusedType.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

internal class SummaryPresenter(
    ILog<SummaryPresenter> log,
    IStatistics statistics,
    IPresenter<IStatistics> statisticsPresenter) : IPresenter<Summary>
{

    public void Show(Summary summary)
    {
        statisticsPresenter.Show(statistics);
        var state = summary.Success == false || statistics.Errors.Any()
            ? new Text("Running FAILED.", Color.Error)
            : new Text("Running succeeded.", Color.Success);
        log.Info(state);
    }
}