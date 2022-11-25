namespace CSharpInteractive;

internal interface ICSharpScriptRunner
{
    CommandResult Run(ICommand sourceCommand, string script);

    void Reset();
}