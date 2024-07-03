namespace CSharpInteractive.Core;

internal interface IRuntimeExplorer
{
    bool TryFindRuntimeAssembly(string assemblyPath, [MaybeNullWhen(false)] out string runtimeAssemblyPath);
}