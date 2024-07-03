// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
namespace CSharpInteractive.Core;

using System.Runtime.InteropServices;

internal class DockerEnvironment(
    IEnvironment environment,
    IFileExplorer fileExplorer) : ITraceSource, IDockerEnvironment
{
    public string Path
    {
        get
        {
            var executable = environment.OperatingSystemPlatform == OSPlatform.Windows ? "docker.exe" : "docker";
            try
            {
                return fileExplorer.FindFiles(executable, "DOCKER_HOME").FirstOrDefault() ?? executable;
            }
            catch
            {
                // ignored
            }

            return executable;
        }
    }

    [ExcludeFromCodeCoverage]
    public IEnumerable<Text> Trace
    {
        get
        {
            yield return new Text($"DockerPath: {Path}");
            yield return Text.NewLine;
        }
    }
}