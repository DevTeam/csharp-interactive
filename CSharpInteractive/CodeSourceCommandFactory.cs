// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf
namespace CSharpInteractive;

using System.Text;

internal class CodeSourceCommandFactory : ICommandFactory<ICodeSource>
{
    private readonly ILog<CodeSourceCommandFactory> _log;
    private readonly ICommandFactory<string>[] _replCommandFactories;
    private readonly ICommandFactory<ScriptCommand> _scriptCommandFactory;

    public CodeSourceCommandFactory(
        ILog<CodeSourceCommandFactory> log,
        IEnumerable<ICommandFactory<string>> replCommandFactories,
        ICommandFactory<ScriptCommand> scriptCommandFactory)
    {
        _log = log;
        _replCommandFactories = replCommandFactories.OrderBy(i => i.Order).ToArray();
        _scriptCommandFactory = scriptCommandFactory;
    }

    public int Order => 0;

    public IEnumerable<ICommand> Create(ICodeSource codeSource)
    {
        var sb = new StringBuilder();
        using var codeEnumerator = codeSource.GetEnumerator();
        while (codeEnumerator.MoveNext())
        {
            var code = codeEnumerator.Current;
            if (code == null)
            {
                foreach (var command in CreateCommands(codeSource, sb))
                {
                    yield return command;
                }

                continue;
            }

            foreach (var line in code.Split(System.Environment.NewLine))
            {
                var trimmedLine = line.Trim();
                _log.Trace(() => [new Text($"Line: \"{trimmedLine}\".")]);
                if (trimmedLine.StartsWith("#"))
                {
                    var hasReplCommand = false;
                    foreach (var replCommandFactory in _replCommandFactories)
                    {
                        var commands = replCommandFactory.Create(trimmedLine).ToArray();
                        _log.Trace(() => [new Text($"REPL commands count: {commands.Length}.")]);
                        if (!commands.Any())
                        {
                            continue;
                        }

                        foreach (var command in CreateCommands(codeSource, sb))
                        {
                            yield return command;
                        }

                        hasReplCommand = true;
                        foreach (var command in commands)
                        {
                            yield return command;
                        }

                        break;
                    }

                    if (!hasReplCommand)
                    {
                        sb.AppendLine(line);
                    }
                }
                else
                {
                    sb.AppendLine(line);
                }
            }
        }

        foreach (var command in CreateCommands(codeSource, sb))
        {
            yield return command;
        }
    }

    private IEnumerable<ICommand> CreateCommands(ICodeSource codeSource, StringBuilder sb)
    {
        if (sb.Length <= 0)
        {
            yield break;
        }

        foreach (var command in _scriptCommandFactory.Create(new ScriptCommand(codeSource.Name, sb.ToString(), codeSource.Internal)))
        {
            yield return command;
        }

        sb.Clear();
    }
}