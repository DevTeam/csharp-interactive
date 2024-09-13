// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Buffers;

internal class MessageIndicesReader(
    ILog<MessageIndicesReader> log,
    MemoryPool<byte> memoryPool,
    IFileSystem fileSystem) : IMessageIndicesReader
{
    public IEnumerable<ulong> Read(string indicesFile)
    {
        using var reader = fileSystem.OpenReader(indicesFile);
        using var bufferOwner = memoryPool.Rent(sizeof(ulong));
        var buffer = bufferOwner.Memory[..sizeof(ulong)];
        int size;
        var prevIndex = 0UL;
        var number = 0UL;
        while ((size = reader.Read(buffer)) == sizeof(ulong))
        {
            buffer.Span.Reverse();
            var index = BitConverter.ToUInt64(buffer.Span);
            if (index <= prevIndex)
            {
                log.Warning($"Corrupted file \"{indicesFile}\", invalid index {index} at offset {number * sizeof(ulong)}.");
                break;
            }

            prevIndex = index;
            number++;
            yield return index;
        }

        if (size != 0)
        {
            log.Warning($"Corrupted file \"{indicesFile}\", invalid size.");
        }
    }
}