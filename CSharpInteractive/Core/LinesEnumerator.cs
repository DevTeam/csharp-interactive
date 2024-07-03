namespace CSharpInteractive.Core;

using System.Collections;

internal class LinesEnumerator(IEnumerable<string> enumerable, Action onDispose) : IEnumerator<string>
{
    private readonly IEnumerator<string> _baseEnumerator = enumerable.GetEnumerator();

    public bool MoveNext() => _baseEnumerator.MoveNext();

    public void Reset() => _baseEnumerator.Reset();

    public string Current => _baseEnumerator.Current;

    object? IEnumerator.Current => ((IEnumerator)_baseEnumerator).Current;

    public void Dispose()
    {
        try
        {
            _baseEnumerator.Dispose();
        }
        finally
        {
            onDispose();
        }
    }
}