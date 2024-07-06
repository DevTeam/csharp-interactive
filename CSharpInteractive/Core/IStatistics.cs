namespace CSharpInteractive.Core;

internal interface IStatistics
{
    IReadOnlyCollection<Text[]> Errors { get; }

    IReadOnlyCollection<Text[]> Warnings { get; }
    
    IReadOnlyCollection<ProcessResult> ProcessResults { get; }

    TimeSpan TimeElapsed { get; }

    IDisposable Start();

    void RegisterError(Text[] error);

    void RegisterWarning(Text[] warning);
    
    void RegisterProcessResult(ProcessResult result);
}