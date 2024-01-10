// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Buffers;
using JetBrains.TeamCity.ServiceMessages;
using JetBrains.TeamCity.ServiceMessages.Read;

internal class MessagesReader(
    ILog<MessagesReader> log,
    MemoryPool<byte> memoryPool,
    IMessageIndicesReader indicesReader,
    IFileSystem fileSystem,
    IEncoding encoding,
    IServiceMessageParser serviceMessageParser)
    : IMessagesReader
{
    public IEnumerable<IServiceMessage> Read(string indicesFile, string messagesFile)
    {
        using var reader = fileSystem.OpenReader(messagesFile);
        var position = 0UL;
        foreach (var index in indicesReader.Read(indicesFile))
        {
            var size = (int)(index - position);
            if (size <= 0)
            {
                log.Warning($"Corrupted file \"{indicesFile}\", invalid index {index}.");
                break;
            }

            using var owner = memoryPool.Rent(size);
            var buffer = owner.Memory[..size];
            if (reader.Read(buffer, (long)position) != size)
            {
                log.Warning($"Corrupted file \"{messagesFile}\", invalid size.");
                break;
            }

            position = index;
            var line = encoding.GetString(buffer);
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            foreach (var message in serviceMessageParser.ParseServiceMessages(line).Where(message => message != default))
            {
                yield return message;
            }
        }
    }
}