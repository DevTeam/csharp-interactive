// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable NotAccessedPositionalProperty.Local
namespace CSharpInteractive.Core;

using HostApi;
using NuGet.Common;
using NuGet.ProjectModel;
using NuGet.Versioning;

[ExcludeFromCodeCoverage]
internal class NuGetAssetsReader(
    ILog<NuGetAssetsReader> log,
    ILogger logger,
    IDotNetEnvironment dotnetEnvironment,
    IFileSystem fileSystem)
    : INuGetAssetsReader
{
    public IEnumerable<NuGetPackage> ReadPackages(string packagesPath, string projectAssetsJson)
    {
        var lockFile = LockFileUtilities.GetLockFile(projectAssetsJson, logger);
        // ReSharper disable once InvertIf
        if (lockFile == null)
        {
            log.Warning($"Cannot process the lock file \"{projectAssetsJson}\".");
            return [];
        }

        return lockFile.Libraries.Select(i =>
            new NuGetPackage(
                i.Name,
                i.Version.Version,
                i.Version,
                i.Type,
                Path.Combine(packagesPath, i.Path),
                i.Sha512,
                i.Files.ToList().AsReadOnly(),
                i.HasTools,
                i.IsServiceable));
    }

    public IEnumerable<ReferencingAssembly> ReadReferencingAssemblies(string projectAssetsJson)
    {
        var lockFile = LockFileUtilities.GetLockFile(projectAssetsJson, logger);
        if (lockFile == null)
        {
            log.Warning($"Cannot process the lock file \"{projectAssetsJson}\".");
            yield break;
        }

        var librariesDict = lockFile.Libraries.ToDictionary(i => new LibraryKey(i.Name, i.Version), i => i);

        var folders = lockFile.PackageFolders.Select(i => i.Path).ToHashSet();
        foreach (var target in lockFile.Targets)
        {
            log.Trace(() => [new Text($"Processing target \"{target.Name}\".")]);
            if (target.TargetFramework.DotNetFrameworkName != dotnetEnvironment.TargetFrameworkMoniker)
            {
                log.Trace(() => [new Text($"Skip processing of target \"{target.Name}\".")]);
                continue;
            }

            foreach (var library in target.Libraries)
            {
                log.Trace(() => [new Text($"Processing library \"{library.Name}\".")]);
                if (!librariesDict.TryGetValue(new LibraryKey(library.Name, library.Version), out var lockFileLibrary))
                {
                    log.Warning($"Cannot find the related library \"{library.Name}\", version {library.Version}.");
                    continue;
                }

                foreach (var assembly in library.RuntimeAssemblies)
                {
                    log.Trace(() => [new Text($"Processing assembly \"{assembly.Path}\".")]);
                    var baseAssemblyPath = Path.Combine(lockFileLibrary.Path, assembly.Path);
                    log.Trace(() => [new Text($"Base assembly path is \"{baseAssemblyPath}\".")]);
                    foreach (var folder in folders)
                    {
                        var fullAssemblyPath = Path.Combine(folder, baseAssemblyPath);
                        log.Trace(() => [new Text($"Full assembly path is \"{fullAssemblyPath}\".")]);
                        if (!fileSystem.IsFileExist(fullAssemblyPath))
                        {
                            log.Trace(() => [new Text($"File \"{baseAssemblyPath}\" does not exist.")]);
                            continue;
                        }

                        var ext = Path.GetExtension(fullAssemblyPath).ToLowerInvariant();
                        if (ext == ".dll")
                        {
                            log.Trace(() => [new Text($"Add reference to \"{fullAssemblyPath}\".")]);
                            yield return new ReferencingAssembly($"{Path.GetFileNameWithoutExtension(fullAssemblyPath)}", fullAssemblyPath);
                        }
                        else
                        {
                            log.Trace(() => [new Text($"Skip file \"{fullAssemblyPath}\".")]);
                        }

                        break;
                    }
                }
            }
        }
    }

    private readonly record struct LibraryKey(string? Name, NuGetVersion? Version);
}