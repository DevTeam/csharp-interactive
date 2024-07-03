namespace CSharpInteractive.Core;

internal interface IMessageIndicesReader
{
    IEnumerable<ulong> Read(string indicesFile);
}