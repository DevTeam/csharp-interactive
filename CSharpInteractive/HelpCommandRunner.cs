// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

internal class HelpCommandRunner(IInfo info) : ICommandRunner
{
    public CommandResult TryRun(ICommand command)
    {
        if (command is not HelpCommand)
        {
            return new CommandResult(command, default);
        }

        info.ShowReplHelp();
        return new CommandResult(command, true);
    }
}