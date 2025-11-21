// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Text.RegularExpressions;
using HostApi;

[SuppressMessage("Performance", "SYSLIB1045:Convert to \'GeneratedRegexAttribute\'.")]
internal class HelpCommandFactory(ILog<HelpCommandFactory> log) : ICommandFactory<string>
{
    private static readonly Regex Regex = new(@"^#help\s*$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    public int Order => 0;

    public IEnumerable<ICommand> Create(string replCommand)
    {
        if (!Regex.IsMatch(replCommand))
        {
            yield break;
        }

        log.Trace(() => [new Text("REPL help")]);
        yield return HelpCommand.Shared;
    }
}