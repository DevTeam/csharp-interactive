namespace CSharpInteractive.Core;

internal interface IStatisticsRegistry
{
    IDisposable Start();

    void RegisterError(Text[] error);

    void RegisterWarning(Text[] warning);
}