namespace CSharpInteractive.Core;

internal interface ICommandFactory<in T>
{
    int Order { get; }

    IEnumerable<ICommand> Create(T data);
}