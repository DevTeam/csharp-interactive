// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

internal class DockerEnvironment : ITraceSource, IDockerEnvironment
{
    private readonly IEnvironment _environment;
    private readonly IFileExplorer _fileExplorer;

    public DockerEnvironment(
        IEnvironment environment,
        IFileExplorer fileExplorer)
    {
        _environment = environment;
        _fileExplorer = fileExplorer;
    }

    public string Path
    {
        get
        {
            var executable = _environment.OperatingSystemPlatform == OSPlatform.Windows ? "docker.exe" : "docker";
            try
            {
                return _fileExplorer.FindFiles(executable, "DOCKER_HOME").FirstOrDefault() ?? executable;
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