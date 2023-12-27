// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;
using NuGet.Versioning;

internal class NuGetReferenceResolver(
    ILog<NuGetReferenceResolver> log,
    INuGetEnvironment nugetEnvironment,
    INuGetRestoreService nugetRestoreService,
    INuGetAssetsReader nugetAssetsReader,
    ICleaner cleaner)
    : INuGetReferenceResolver
{

    public bool TryResolveAssemblies(string packageId, VersionRange? versionRange, out IReadOnlyCollection<ReferencingAssembly> assemblies)
    {
        var result = new List<ReferencingAssembly>();
        assemblies = result;
        var packageName = $"{packageId} {versionRange}".Trim();
        log.Info([new Text($"Restoring package {packageName}.", Color.Highlighted)]);
        var restoreResult = nugetRestoreService.TryRestore(
            new NuGetRestoreSettings(
                packageId,
                nugetEnvironment.Sources,
                nugetEnvironment.FallbackFolders,
                versionRange,
                default,
                nugetEnvironment.PackagesPath
            ),
            out var projectAssetsJson);

        if (!restoreResult)
        {
            return false;
        }

        var output = Path.GetDirectoryName(projectAssetsJson);
        var outputPathToken = Disposable.Empty;
        if (!string.IsNullOrWhiteSpace(output))
        {
            outputPathToken = cleaner.Track(output);
        }

        using (outputPathToken)
        {
            log.Trace(() => new Text("Assemblies referenced:"));
            foreach (var assembly in nugetAssetsReader.ReadReferencingAssemblies(projectAssetsJson))
            {
                log.Trace(() => [Text.Tab, new Text(assembly.Name)]);
                result.Add(assembly);
            }
        }

        return true;
    }
}