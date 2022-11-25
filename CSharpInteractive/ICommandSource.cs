namespace CSharpInteractive;

internal interface ICommandSource
{
    IEnumerable<ICommand> GetCommands();
}