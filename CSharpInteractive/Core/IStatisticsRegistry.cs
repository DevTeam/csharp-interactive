namespace CSharpInteractive.Core;

using HostApi;

internal interface IStatisticsRegistry
{
    IDisposable Start();

    void Register(StatisticsType type, Text[] message);
}