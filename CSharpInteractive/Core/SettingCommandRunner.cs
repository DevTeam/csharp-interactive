// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

internal class SettingCommandRunner<TOption>(
    ILog<SettingCommandRunner<TOption>> log,
    ISettingSetter<TOption> settingSetter) : ICommandRunner
    where TOption: struct, Enum
{
    public CommandResult TryRun(ICommand command)
    {
        if (command is not SettingCommand<TOption> settingCommand)
        {
            return new CommandResult(command, default);
        }

        var previousValue = settingSetter.SetSetting(settingCommand.Value);
        log.Trace(() => [new Text($"Change the {typeof(TOption).Name} from {previousValue} to {settingCommand.Value}.")]);

        return new CommandResult(command, true);
    }
}