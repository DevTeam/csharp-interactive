// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using Pure.DI;

internal class RuntimeExplorer(
    [Tag(Tag.RuntimePath)] string runtimePath,
    IFileSystem fileSystem) : IRuntimeExplorer
{
    public bool TryFindRuntimeAssembly(string assemblyPath, [MaybeNullWhen(false)] out string runtimeAssemblyPath)
    {
        if (string.IsNullOrWhiteSpace(runtimePath))
        {
            runtimeAssemblyPath = default;
            return false;
        }

        runtimeAssemblyPath = fileSystem.EnumerateFileSystemEntries(runtimePath, Path.GetFileName(assemblyPath), SearchOption.TopDirectoryOnly).FirstOrDefault();
        return runtimeAssemblyPath != default;
    }
}