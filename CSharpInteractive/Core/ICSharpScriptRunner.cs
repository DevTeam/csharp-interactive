namespace CSharpInteractive.Core;

internal interface ICSharpScriptRunner
{
    CommandResult Run(ICommand sourceCommand, string script);

    void Reset();
}