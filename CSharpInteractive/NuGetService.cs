// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi;

internal class NuGetService(
    ILog<NuGetService> log,
    IFileSystem fileSystem,
    IEnvironment environment,
    INuGetEnvironment nugetEnvironment,
    INuGetRestoreService nugetRestoreService,
    INuGetAssetsReader nugetAssetsReader,
    ICleaner cleaner)
    : INuGet
{
    public IEnumerable<NuGetPackage> Restore(NuGetRestoreSettings settings)
    {
        var packagesPath = settings.PackagesPath;
        if (string.IsNullOrWhiteSpace(packagesPath))
        {
            packagesPath = nugetEnvironment.PackagesPath;
        }

        if (!fileSystem.IsPathRooted(packagesPath))
        {
            packagesPath = Path.Combine(environment.GetPath(SpecialFolder.Working), packagesPath);
        }

        settings = settings.WithPackagesPath(packagesPath);
        if (!settings.Sources.Any())
        {
            settings = settings.WithSources(nugetEnvironment.Sources);
        }

        if (!settings.FallbackFolders.Any())
        {
            settings = settings.WithFallbackFolders(nugetEnvironment.FallbackFolders);
        }

        var restoreResult = nugetRestoreService.TryRestore(settings, out var projectAssetsJson);
        if (restoreResult == false)
        {
            log.Warning($"Cannot restore the NuGet package {settings.PackageId} {settings.VersionRange}".Trim() + '.');
            return Enumerable.Empty<NuGetPackage>();
        }

        var output = Path.GetDirectoryName(projectAssetsJson);
        var outputPathToken = Disposable.Empty;
        if (!string.IsNullOrWhiteSpace(output))
        {
            outputPathToken = cleaner.Track(output);
        }

        using (outputPathToken)
        {
            return nugetAssetsReader.ReadPackages(packagesPath, projectAssetsJson);
        }
    }
}