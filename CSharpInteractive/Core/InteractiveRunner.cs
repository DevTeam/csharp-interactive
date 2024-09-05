// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable UnusedType.Global
namespace CSharpInteractive.Core;

internal class InteractiveRunner(
    ICommandSource commandSource,
    ICommandsRunner commandsRunner,
    IStdOut stdOut) : IScriptRunner
{
    public int Run()
    {
        ShowCursor(true);
        // ReSharper disable once UseDeconstruction
        foreach (var result in commandsRunner.Run(commandSource.GetCommands()))
        {
            var exitCode = result.ExitCode;
            if (exitCode.HasValue)
            {
                return exitCode.Value;
            }

            if (!result.Command.Internal)
            {
                ShowCursor(result.Command is not CodeCommand);
            }
        }

        return 0;
    }

    private void ShowCursor(bool completed) =>
        stdOut.Write(new Text(completed ? "> " : ". "));
}