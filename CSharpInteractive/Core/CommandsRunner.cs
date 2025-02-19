// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf

namespace CSharpInteractive.Core;

internal class CommandsRunner(
    // ReSharper disable once ParameterTypeCanBeEnumerable.Local
    IReadOnlyCollection<ICommandRunner> commandRunners,
    IStatisticsRegistry statisticsRegistry) : ICommandsRunner
{
    public IEnumerable<CommandResult> Run(IEnumerable<ICommand> commands)
    {
        using (statisticsRegistry.Start())
        {
            foreach (var command in commands)
            {
                var processed = false;
                foreach (var runner in commandRunners)
                {
                    var result = runner.TryRun(command);
                    if (result.Success.HasValue)
                    {
                        processed = true;
                        yield return result;
                        break;
                    }
                }

                if (!processed)
                {
                    yield return new CommandResult(command, null);
                }
            }
        }
    }
}