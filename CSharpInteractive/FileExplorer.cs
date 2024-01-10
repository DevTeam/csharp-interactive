// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Runtime.InteropServices;

internal class FileExplorer(
    IEnvironment environment,
    IHostEnvironment hostEnvironment,
    IFileSystem fileSystem) : IFileExplorer
{
    private char PathSeparator => environment.OperatingSystemPlatform == OSPlatform.Windows ? ';' : ':';

    public IEnumerable<string> FindFiles(string searchPattern, params string[] additionalVariables)
    {
        var additionalPaths = additionalVariables.Select(hostEnvironment.GetEnvironmentVariable);
        var paths = hostEnvironment.GetEnvironmentVariable("PATH")?.Split(PathSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Enumerable.Empty<string>();
        var allPaths = additionalPaths.Concat(paths).Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i?.Trim()).Distinct();
        return (
                from path in allPaths
                where fileSystem.IsDirectoryExist(path)
                from file in fileSystem.EnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly)
                where fileSystem.IsFileExist(file)
                select file)
            .Distinct();
    }
}