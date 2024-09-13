// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;
using JetBrains.TeamCity.ServiceMessages;
using Pure.DI;

internal class ReliableBuildContext(
    ICISettings ciSettings,
    IFileSystem fileSystem,
    IMessagesReader messagesReader,
    [Tag("base")] IBuildContext baseBuildContext)
    : IBuildContext
{
    private readonly Dictionary<string, Output> _sources = new();

    public IReadOnlyList<BuildMessage> ProcessMessage(Output output, IServiceMessage message)
    {
        var source = message.GetValue("source");
        if (string.IsNullOrWhiteSpace(source))
        {
            return baseBuildContext.ProcessMessage(output, message);
        }

        _sources.TryAdd(source, output with {Line = string.Empty});
        return Array.Empty<BuildMessage>();
    }

    public IReadOnlyList<BuildMessage> ProcessOutput(Output output) =>
        baseBuildContext.ProcessOutput(output);

    public IBuildResult Create(ICommandLineResult commandLineResult)
    {
        var items =
            from source in _sources
            let indicesFile = Path.Combine(ciSettings.ServiceMessagesPath, source.Key)
            where fileSystem.IsFileExist(indicesFile)
            let messagesFile = indicesFile + ".msg"
            where fileSystem.IsFileExist(messagesFile)
            from message in messagesReader.Read(indicesFile, messagesFile)
            select (message, startInfoFactory: source.Value);

        // ReSharper disable once UseDeconstruction
        foreach (var (message, output) in items)
        {
            baseBuildContext.ProcessMessage(output, message);
        }

        return baseBuildContext.Create(commandLineResult);
    }
}