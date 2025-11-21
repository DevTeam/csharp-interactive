// ReSharper disable NotDisposedResourceIsReturned

namespace CSharpInteractive.Core;

using System.Collections;

internal class LineCodeSource(string line) : ICodeSource
{
    public IEnumerator<string?> GetEnumerator() => Enumerable.Repeat(line, 1).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public string Name => line;

    public bool Internal => true;
}