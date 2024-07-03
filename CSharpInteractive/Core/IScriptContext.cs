namespace CSharpInteractive.Core;

internal interface IScriptContext
{
    IDisposable CreateScope(ICodeSource source);
}