// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable SuggestBaseTypeForParameterInConstructor
// ReSharper disable NotAccessedPositionalProperty.Global

namespace HostApi;

using NuGet.Versioning;

/// <summary>
/// Represents information about the NuGet package.
/// </summary>
/// <param name="Name">Package name.</param>
/// <param name="Version">Package version without metadata or release labels.</param>
/// <param name="NuGetVersion">NuGet package version.</param>
/// <param name="Type">Package type.</param>
/// <param name="Path">Full path to the directory where the package was restored.</param>
/// <param name="Sha512">SHA 512 hash of the packet.</param>
/// <param name="Files">List of files related to the package.</param>
/// <param name="HasTools">Indicates that the package contains a tool.</param>
/// <param name="IsServiceable">Indicates that the package is serviceable.</param>
[Target]
public readonly record struct NuGetPackage(
    string Name,
    Version Version,
    NuGetVersion NuGetVersion,
    string Type,
    string Path,
    string Sha512,
    IReadOnlyList<string> Files,
    bool HasTools,
    bool IsServiceable);