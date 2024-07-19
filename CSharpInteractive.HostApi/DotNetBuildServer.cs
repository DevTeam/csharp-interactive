// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace HostApi;

/// <summary>
/// Type of .NET service.
/// </summary>
/// <seealso cref="DotNetBuildServerShutdown"/>
public enum DotNetBuildServer
{
    /// <summary>
    /// MSBuild build server
    /// </summary>
    MSBuild,
    
    /// <summary>
    /// VB/C# compiler build server
    /// </summary>
    VbCsCompiler,
    
    /// <summary>
    /// Razor build server
    /// </summary>
    Razor
}