namespace CSharpInteractive.Core;

internal interface ICommandSource
{
    IEnumerable<ICommand> GetCommands();
}