namespace CSharpInteractive;

internal interface IEncoding
{
    string GetString(Memory<byte> buffer);
}