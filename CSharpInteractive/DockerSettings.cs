// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using HostApi.Docker;

internal class DockerSettings(IDockerEnvironment dockerEnvironment) : IDockerSettings
{

    public string DockerExecutablePath => dockerEnvironment.Path;
}