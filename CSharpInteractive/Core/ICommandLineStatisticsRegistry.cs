namespace CSharpInteractive.Core;

internal interface ICommandLineStatisticsRegistry
{
    void Register(CommandLineInfo info);

    void RegisterWarning(ProcessInfo processInfo, Text[] warning);

    void RegisterError(ProcessInfo processInfo, Text[] error);
}