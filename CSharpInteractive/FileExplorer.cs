// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

using System.Runtime.InteropServices;

internal class FileExplorer : IFileExplorer {
    private readonly IEnvironment _environment;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IFileSystem _fileSystem;

    public FileExplorer(
        IEnvironment environment,
        IHostEnvironment hostEnvironment,
        IFileSystem fileSystem)
    {
        _environment = environment;
        _hostEnvironment = hostEnvironment;
        _fileSystem = fileSystem;
    }
    
    private char PathSeparator => _environment.OperatingSystemPlatform == OSPlatform.Windows ? ';' : ':';

    public IEnumerable<string> FindFiles(string searchPattern, params string[] additionalVariables)
    {
        var additionalPaths = additionalVariables.Select(varName => _hostEnvironment.GetEnvironmentVariable(varName));
        var paths = _hostEnvironment.GetEnvironmentVariable("PATH")?.Split(PathSeparator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Enumerable.Empty<string>();
        var allPaths = additionalPaths.Concat(paths).Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i?.Trim()).Distinct();
        return (
                from path in allPaths
                where _fileSystem.IsDirectoryExist(path)
                from file in _fileSystem.EnumerateFileSystemEntries(path, searchPattern, SearchOption.TopDirectoryOnly)
                where _fileSystem.IsFileExist(file)
                select file)
            .Distinct();
    }
}