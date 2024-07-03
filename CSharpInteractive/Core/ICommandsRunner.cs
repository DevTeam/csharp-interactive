namespace CSharpInteractive.Core;

internal interface ICommandsRunner
{
    IEnumerable<CommandResult> Run(IEnumerable<ICommand> commands);
}