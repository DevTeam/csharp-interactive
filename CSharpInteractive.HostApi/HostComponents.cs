// ReSharper disable ClassNeverInstantiated.Global
namespace HostApi;

using Internal.Cmd;
using Internal.Docker;
using Internal.DotNet;

internal record HostComponents(
    // Settings
    IDockerSettings DockerSettings,
    IDotNetSettings DotNetSettings,
    // Command line context to register paths resolver
    IPathResolverContext PathResolverContext,
    // Command line context to resolver a path
    IVirtualContext VirtualContext);