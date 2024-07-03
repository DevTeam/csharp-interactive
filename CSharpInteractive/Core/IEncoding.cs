namespace CSharpInteractive.Core;

internal interface IEncoding
{
    string GetString(Memory<byte> buffer);
}