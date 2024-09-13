// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedType.Global

namespace CSharpInteractive.Core;

internal class CommandSource(
    ISettings settings,
    ICommandFactory<ICodeSource> codeSourceCommandFactory) : ICommandSource
{
    public IEnumerable<ICommand> GetCommands() =>
        settings.CodeSources.SelectMany(codeSourceCommandFactory.Create);
}