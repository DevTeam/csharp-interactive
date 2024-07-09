// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

[ExcludeFromCodeCoverage]
internal class StatisticsPresenter(ILog<StatisticsPresenter> log) : IPresenter<IStatistics>
{
    private static readonly Text[] Tab = [Text.Tab];
    
    public void Show(IStatistics statistics)
    {
        foreach (var warning in statistics.Warnings)
        {
            log.Info(warning.AddPrefix(_ => Tab));
        }
            
        foreach (var error in statistics.Errors)
        {
            log.Info(error.AddPrefix(_ => Tab));
        }

        if (statistics.Warnings.Count > 0)
        {
            log.Info(new Text($"{statistics.Warnings.Count} Warning(s)", Color.Warning));
        }

        if (statistics.Errors.Count > 0)
        {
            log.Info(new Text($"{statistics.Errors.Count} Error(s)", Color.Error));
        }

        log.Info(new Text($"Time Elapsed {statistics.TimeElapsed:g}"));
    }
}