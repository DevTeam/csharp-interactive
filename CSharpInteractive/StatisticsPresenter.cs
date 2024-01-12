// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

[ExcludeFromCodeCoverage]
internal class StatisticsPresenter(ILog<StatisticsPresenter> log) : IPresenter<IStatistics>
{
    public void Show(IStatistics statistics)
    {
        foreach (var error in statistics.Errors)
        {
            log.Info(Text.Tab, new Text(error, Color.Error));
        }

        foreach (var warning in statistics.Warnings)
        {
            log.Info(Text.Tab, new Text(warning, Color.Warning));
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