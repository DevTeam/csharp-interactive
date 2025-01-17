// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

internal class HelpCommandRunner(IInfo info) : ICommandRunner
{
    public CommandResult TryRun(ICommand command)
    {
        if (command is not HelpCommand)
        {
            return new CommandResult(command, null);
        }

        info.ShowReplHelp();
        return new CommandResult(command, true);
    }
}