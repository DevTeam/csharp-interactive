// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Collections;

[ExcludeFromCodeCoverage]
internal class ConsoleSource(CancellationToken cancellationToken) : ICodeSource, IEnumerator<string?>
{
    public string Name => "Console";

    public bool Internal => false;

    public IEnumerator<string?> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public string? Current { get; private set; } = string.Empty;

    object? IEnumerator.Current => Current;

    public bool MoveNext()
    {
        if (Current == null)
        {
            Task.Run(() => { Current = System.Console.In.ReadLine() ?? string.Empty; }, cancellationToken).Wait(cancellationToken);
        }
        else
        {
            Current = null;
        }

        return true;
    }

    public void Reset()
    { }

    public void Dispose()
    { }
}