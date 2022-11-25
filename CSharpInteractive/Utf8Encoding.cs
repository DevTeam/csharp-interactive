// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using System.Text;

[ExcludeFromCodeCoverage]
internal class Utf8Encoding : IEncoding
{
    public string GetString(Memory<byte> buffer) =>
        Encoding.UTF8.GetString(buffer.Span);
}