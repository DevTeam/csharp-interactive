// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi.Internal.Docker;

internal class DockerSettings(IDockerEnvironment dockerEnvironment) : IDockerSettings
{
    public string DockerExecutablePath => dockerEnvironment.Path;
}