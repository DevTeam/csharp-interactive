namespace CSharpInteractive.Core;

internal interface IStatistics
{
    bool IsEmpty { get; }
    
    IReadOnlyCollection<Text[]> Errors { get; }

    IReadOnlyCollection<Text[]> Warnings { get; }
    
    TimeSpan TimeElapsed { get; }
}