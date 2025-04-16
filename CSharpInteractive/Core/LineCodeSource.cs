// ReSharper disable NotDisposedResourceIsReturned

namespace CSharpInteractive.Core;

using System.Collections;

internal class LineCodeSource(string line) : ICodeSource
{
    public string Line { get; set; } = line;

    public IEnumerator<string?> GetEnumerator() => Enumerable.Repeat(Line, 1).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public string Name => Line;

    public bool Internal => true;
}