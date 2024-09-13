// ReSharper disable UnusedMember.Global

namespace HostApi;

/// <summary>
/// Represents the NuGet package type.
/// </summary>
public enum NuGetPackageType
{
    /// <summary>
    /// Package type packages add build- or run-time assets to libraries and applications, and can be installed in any project type (assuming they are compatible).
    /// </summary>
    Package,

    /// <summary>
    /// Tool type packages are .NET tools that can be installed by the dotnet CLI.
    /// </summary>
    Tool
}