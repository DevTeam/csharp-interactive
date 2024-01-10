namespace CSharpInteractive;

internal interface IErrorContext
{
    bool TryGetSourceName([NotNullWhen(true)] out string? name);
}