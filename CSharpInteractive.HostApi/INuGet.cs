// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace HostApi;

/// <summary>
/// Service for working with NuGet.
/// <example>
/// <code>
/// var nuGet = GetService&lt;INuGet&gt;();
/// </code>
/// </example>
/// </summary>
public interface INuGet
{
    /// <summary>
    /// Restores the NuGet package and all its dependencies according to the specified settings.
    /// <example>
    /// <code>
    /// var nuget = GetService&lt;INuGet&gt;();
    ///
    /// 
    /// var settings = new NuGetRestoreSettings("MySampleLib")
    ///   .WithVersionRange(VersionRange.Parse("[1.0.14, 1.1)"))
    ///   .WithTargetFrameworkMoniker("net10.0")
    ///   .WithPackagesPath(".packages");
    ///
    /// 
    /// var packages = nuget.Restore(settings);
    /// foreach (var package in packages)
    /// {
    ///   Info(package.Path);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="settings">Package restore settings.</param>
    /// <returns>Information about the package and all its dependencies that have been restored.</returns>
    /// <seealso cref="IHost.GetService{T}"/>
    IEnumerable<NuGetPackage> Restore(NuGetRestoreSettings settings);
}