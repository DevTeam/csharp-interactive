namespace HostApi;

using NuGet.Versioning;

/// <summary>
/// Settings for restoring NuGet packages.
/// <example>
/// <code>
/// var settings = new NuGetRestoreSettings("MySampleLib")
///   .WithVersionRange(VersionRange.Parse("[1.0.14, 1.1)"))
///   .WithTargetFrameworkMoniker("net6.0")
///   .WithPackagesPath(".packages");
/// </code>
/// </example>
/// </summary>
/// <param name="PackageId">Identifier of the NuGet package to restore.</param>
/// <param name="Sources">Enumeration of package sources (as URLs) to use for the restore.</param>
/// <param name="FallbackFolders">Enumeration of package sources to use as fallbacks in case the package isn't found in the primary or default source.</param>
/// <param name="VersionRange">NuGet package version range that will be used to restore a package.</param>
/// <param name="TargetFrameworkMoniker">Specifies a target framework moniker (TFM) - standardized token format for specifying the target framework of a .NET app or library.</param>
/// <param name="PackagesPath">Specifies a path in which packages are installed. If no folder is specified, the current folder is used.</param>
/// <param name="PackageType">NuGet package type.</param>
/// <param name="DisableParallel">Disable parallel project restores and downloads.</param>
/// <param name="IgnoreFailedSources">Ignore errors from package sources.</param>
/// <param name="HideWarningsAndErrors">Do not display Errors and Warnings to the user.</param>
/// <param name="NoCache">Prevents NuGet from using cached packages.</param>
/// <seealso cref="INuGet.Restore"/>
[Target]
public record NuGetRestoreSettings(
    // Identifier of the NuGet package to restore.
    string PackageId,
    // Enumeration of package sources (as URLs) to use for the restore.
    IEnumerable<string> Sources,
    // Enumeration of package sources to use as fallbacks in case the package isn't found in the primary or default source.
    IEnumerable<string> FallbackFolders,
    // NuGet package version range that will be used to restore a package.
    VersionRange? VersionRange = default,
    // Specifies a target framework moniker (TFM) - standardized token format for specifying the target framework of a .NET app or library.
    string? TargetFrameworkMoniker = default,
    // Specifies a path in which packages are installed. If no folder is specified, the current folder is used.
    string? PackagesPath = default,
    // NuGet package type.
    NuGetPackageType? PackageType = default,
    // Disable parallel project restores and downloads.
    bool? DisableParallel = default,
    // Ignore errors from package sources.
    bool? IgnoreFailedSources = default,
    // Do not display Errors and Warnings to the user.
    bool? HideWarningsAndErrors = default,
    // Prevents NuGet from using cached packages.
    bool? NoCache = default)
{
    /// <summary>
    /// Creates a new instance of the settings.
    /// </summary> 
    /// <param name="packageId">Identifier of the NuGet package to restore.</param>
    public NuGetRestoreSettings(string packageId)
        : this(packageId, [], [])
    { }
}