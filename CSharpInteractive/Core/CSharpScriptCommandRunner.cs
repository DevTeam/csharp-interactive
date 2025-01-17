// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf

namespace CSharpInteractive.Core;

internal class CSharpScriptCommandRunner(ICSharpScriptRunner scriptRunner) : ICommandRunner
{
    public CommandResult TryRun(ICommand command)
    {
        switch (command)
        {
            case ScriptCommand scriptCommand:
                return scriptRunner.Run(command, scriptCommand.Script);

            case ResetCommand:
                scriptRunner.Reset();
                return new CommandResult(command, true);

            default:
                return new CommandResult(command, null);
        }
    }
}