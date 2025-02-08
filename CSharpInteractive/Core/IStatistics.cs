namespace CSharpInteractive.Core;

internal interface IStatistics
{
    bool IsEmpty { get; }

    IReadOnlyCollection<StatisticsItem> Items { get; }

    TimeSpan TimeElapsed { get; }

    IReadOnlyCollection<CommandLineInfo> CommandLines { get; }
}