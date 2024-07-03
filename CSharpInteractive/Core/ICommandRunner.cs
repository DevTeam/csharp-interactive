namespace CSharpInteractive.Core;

internal interface ICommandRunner
{
    CommandResult TryRun(ICommand command);
}