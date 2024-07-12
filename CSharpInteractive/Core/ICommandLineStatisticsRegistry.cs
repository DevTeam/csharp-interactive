namespace CSharpInteractive.Core;

internal interface ICommandLineStatisticsRegistry
{
    void Register(CommandLineInfo info);
}