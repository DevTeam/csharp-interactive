namespace CSharpInteractive;

internal interface ICommandsRunner
{
    IEnumerable<CommandResult> Run(IEnumerable<ICommand> commands);
}