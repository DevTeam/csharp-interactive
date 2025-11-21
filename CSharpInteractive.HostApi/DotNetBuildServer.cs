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

// ReSharper disable once UnusedType.Global
internal static class DotNetBuildServerExtensions
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static string[] ToArgs(this IEnumerable<DotNetBuildServer> servers, string name, string collectionSeparator) =>
        servers.Select(server => server switch
        {
            DotNetBuildServer.MSBuild => "--msbuild",
            DotNetBuildServer.VbCsCompiler => "--vbcscompiler",
            DotNetBuildServer.Razor => "--razor",
            _ => throw new ArgumentOutOfRangeException()
        }).ToArray();
}