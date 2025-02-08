// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf
// ReSharper disable UnusedType.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class ScriptRunner(
    ILog<ScriptRunner> log,
    ICommandSource commandSource,
    ICommandsRunner commandsRunner,
    IStatistics statistics,
    IPresenter<Summary> summaryPresenter)
    : IScriptRunner
{
    public int Run()
    {
        var summary = new Summary(true);
        try
        {
            int? exitCode = null;
            foreach (var (command, success, currentExitCode) in commandsRunner.Run(GetCommands()))
            {
                if (success.HasValue)
                {
                    if (!success.Value)
                    {
                        summary = summary.WithSuccess(false);
                        break;
                    }
                }
                else
                {
                    log.Error(ErrorId.NotSupported, $"{command} is not supported.");
                }

                exitCode = currentExitCode;
            }

            var actualExitCode = exitCode ?? (summary.Success == false || statistics.Items.Any(i => i.Type == StatisticsType.Error) ? 1 : 0);
            log.Trace(() => [new Text($"Exit code: {actualExitCode}.")]);
            return actualExitCode;
        }
        finally
        {
            summaryPresenter.Show(summary);
        }
    }

    private IEnumerable<ICommand> GetCommands()
    {
        CodeCommand? codeCommand = null;
        foreach (var command in commandSource.GetCommands())
        {
            codeCommand = command as CodeCommand;
            if (codeCommand == null)
            {
                yield return command;
            }
        }

        if (codeCommand != null)
        {
            log.Error(ErrorId.UncompletedScript, "Script is uncompleted.");
        }
    }
}