namespace CSharpInteractive;

internal interface IMessageIndicesReader
{
    IEnumerable<ulong> Read(string indicesFile);
}