// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;

internal class ReferencesScriptOptionsFactory(
    ILog<ReferencesScriptOptionsFactory> log,
    IRuntimeExplorer runtimeExplorer) : IScriptOptionsFactory, IReferenceRegistry
{
    private readonly HashSet<PortableExecutableReference> _references = [];

    public ScriptOptions Create(ScriptOptions baseOptions) => baseOptions.AddReferences(_references);

    public bool TryRegisterAssembly(string assemblyPath, out string description)
    {
        try
        {
            assemblyPath = Path.GetFullPath(assemblyPath);
            if (runtimeExplorer.TryFindRuntimeAssembly(assemblyPath, out var runtimeAssemblyPath))
            {
                AddRef(runtimeAssemblyPath, out description);
            }

            AddRef(assemblyPath, out description);
            return true;
        }
        catch (Exception ex)
        {
            description = ex.Message;
        }

        return false;
    }

    private void AddRef(string fileName, out string description)
    {
        log.Trace(() => [new Text($"Try register the assembly \"{fileName}\".")]);
        var reference = MetadataReference.CreateFromFile(fileName);
        description = reference.Display ?? string.Empty;
        _references.Add(reference);
        log.Trace(() => [new Text($"New metadata reference added \"{reference.Display}\".")]);
    }
}