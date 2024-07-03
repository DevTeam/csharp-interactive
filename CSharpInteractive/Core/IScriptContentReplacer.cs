namespace CSharpInteractive.Core;

internal interface IScriptContentReplacer
{
    IEnumerable<string> Replace(IEnumerable<string> lines);
}