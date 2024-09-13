// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Collections;

internal class LoadFileCodeSource(
    IFilePathResolver filePathResolver,
    IScriptContext scriptContext) : ICodeSource
{
    private string _fileName = "";

    public string Name
    {
        get => _fileName;
        set
        {
            if (!filePathResolver.TryResolve(value, out var fullFilePath))
            {
                fullFilePath = value;
            }

            _fileName = fullFilePath;
        }
    }

    public bool Internal => false;

    public IEnumerator<string> GetEnumerator()
    {
        var scope = scriptContext.CreateScope(this);
        return new LinesEnumerator(new List<string> {$"#load \"{_fileName}\""}, () => scope.Dispose());
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}