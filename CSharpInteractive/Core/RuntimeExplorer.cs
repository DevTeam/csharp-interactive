// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using Pure.DI;
using static Pure.DI.Tag;

internal class RuntimeExplorer(
    [Tag("RuntimePathTag")] string runtimePath,
    IFileSystem fileSystem) : IRuntimeExplorer
{
    public bool TryFindRuntimeAssembly(string assemblyPath, [MaybeNullWhen(false)] out string runtimeAssemblyPath)
    {
        if (string.IsNullOrWhiteSpace(runtimePath))
        {
            runtimeAssemblyPath = null;
            return false;
        }

        runtimeAssemblyPath = fileSystem.EnumerateFileSystemEntries(runtimePath, Path.GetFileName(assemblyPath), SearchOption.TopDirectoryOnly).FirstOrDefault();
        return runtimeAssemblyPath != null;
    }
}