namespace CSharpInteractive.Core;

internal interface ICommandLineStatistics
{
    bool IsEmpty { get; }
    
    IReadOnlyCollection<CommandLineInfo> CommandLines { get; }

    void Register(CommandLineInfo info);
}