namespace CSharpInteractive.Core;

internal interface ICommand
{
    string Name { get; }

    bool Internal { get; }
}