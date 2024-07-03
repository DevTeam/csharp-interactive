namespace CSharpInteractive.Core;

internal interface IStatistics
{
    IReadOnlyCollection<string> Errors { get; }

    IReadOnlyCollection<string> Warnings { get; }
    
    IReadOnlyCollection<ProcessResult> ProcessResults { get; }

    TimeSpan TimeElapsed { get; }

    IDisposable Start();

    void RegisterError(string error);

    void RegisterWarning(string warning);
    
    void RegisterProcessResult(ProcessResult result);
}