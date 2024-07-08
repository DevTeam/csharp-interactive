// ReSharper disable ClassNeverInstantiated.Global
namespace HostApi.Internal;

using HostApi.Internal.Cmd;
using HostApi.Internal.Docker;
using HostApi.Internal.DotNet;

internal record HostComponents(
    // Settings
    IDockerSettings DockerSettings,
    IDotNetSettings DotNetSettings,
    // Command line context to register paths resolver
    IPathResolverContext PathResolverContext,
    // Command line context to resolver a path
    IVirtualContext VirtualContext);