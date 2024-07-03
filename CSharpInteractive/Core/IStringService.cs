namespace CSharpInteractive.Core;

internal interface IStringService
{
    string TrimAndUnquote(string quotedString);
}