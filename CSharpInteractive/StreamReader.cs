namespace CSharpInteractive;

internal class StreamReader(Stream stream) : IStreamReader
{
    private readonly object _lockObject = new();

    public int Read(Memory<byte> buffer)
    {
        lock (_lockObject)
        {
            return stream.Read(buffer.Span);
        }
    }

    public int Read(Memory<byte> buffer, long offset)
    {
        lock (_lockObject)
        {
            stream.Seek(offset, SeekOrigin.Begin);
            return stream.Read(buffer.Span);
        }
    }

    public void Dispose()
    {
        lock (_lockObject)
        {
            stream.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}