// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Collections;

internal class LoadFileCodeSource : ICodeSource
{
    private readonly IScriptContext _scriptContext;

    public LoadFileCodeSource(IFilePathResolver filePathResolver,
        IScriptContext scriptContext,
        string name)
    {
        _scriptContext = scriptContext;
        if (!filePathResolver.TryResolve(name, out var fullFilePath))
        {
            Name = name;
        }

        Name = fullFilePath;
    }

    public string Name { get; }

    public bool Internal => false;

    public IEnumerator<string> GetEnumerator()
    {
        var scope = _scriptContext.CreateScope(this);
        return new LinesEnumerator(new List<string> {$"#load \"{Name}\""}, () => scope.Dispose());
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}