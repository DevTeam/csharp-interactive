namespace CSharpInteractive.Core;

internal interface ICodeSource : IEnumerable<string?>
{
    string Name { get; }

    bool Internal { get; }
}