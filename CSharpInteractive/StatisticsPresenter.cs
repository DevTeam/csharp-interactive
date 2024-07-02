// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

[ExcludeFromCodeCoverage]
internal class StatisticsPresenter(ILog<StatisticsPresenter> log) : IPresenter<IStatistics>
{
    public void Show(IStatistics statistics)
    {
        log.Info();
        if (statistics.ProcessResults.Count > 0 || statistics.Warnings.Count > 0 || statistics.Errors.Count > 0)
        {
            log.Info(new Text("Summary:", Color.Header));

            foreach (var processResult in statistics.ProcessResults)
            {
                log.Info(Text.Tab + processResult.Description);
            }

            foreach (var warning in statistics.Warnings)
            {
                log.Info(Text.Tab, new Text(warning, Color.Warning));
            }

            foreach (var error in statistics.Errors)
            {
                log.Info(Text.Tab, new Text(error, Color.Error));
            }
        }

        if (statistics.Warnings.Count > 0)
        {
            log.Info(new Text($"{statistics.Warnings.Count} Warning(s)"));
        }

        if (statistics.Errors.Count > 0)
        {
            log.Info(new Text($"{statistics.Errors.Count} Error(s)", Color.Error));
        }

        log.Info(new Text($"Time Elapsed {statistics.TimeElapsed:g}"));
    }
}