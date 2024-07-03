namespace CSharpInteractive.Core;

internal interface IErrorContext
{
    bool TryGetSourceName([NotNullWhen(true)] out string? name);
}