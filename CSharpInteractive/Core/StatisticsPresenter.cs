// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi;

[ExcludeFromCodeCoverage]
internal class StatisticsPresenter(ILog<StatisticsPresenter> log) : IPresenter<IStatistics>
{
    private static readonly Text[] Tab = [Text.Tab];
    private static readonly Text[] Empty = [Text.Tab, new Text("     ")];
    private static readonly Text[] Error = [Text.Tab, new Text("ERR: ", Color.Error)];
    private static readonly Text [] Warning = [Text.Tab, new Text("WRN: ", Color.Warning)];
    
    public void Show(IStatistics statistics)
    {
        log.Info();
        if (statistics.ProcessResults.Count > 0 || statistics.Warnings.Count > 0 || statistics.Errors.Count > 0)
        {
            log.Info(new Text("Summary:", Color.Header));

            foreach (var processResult in statistics.ProcessResults)
            {
                log.Info(processResult.Description.AddPrefix(_ => Tab));
            }

            foreach (var warning in statistics.Warnings)
            {
                log.Info(warning.AddPrefix(i => i == 0 ? Warning : Empty));
            }
            
            foreach (var error in statistics.Errors)
            {
                log.Info(error.AddPrefix(i => i == 0 ? Error : Empty));
            }
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