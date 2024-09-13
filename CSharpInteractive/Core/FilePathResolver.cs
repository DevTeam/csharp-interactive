// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

internal class FilePathResolver(
    ILog<FilePathResolver> log,
    IEnvironment environment,
    IFileSystem fileSystem) : IFilePathResolver
{
    public bool TryResolve(string? fileOrDirectoryPath, out string fullScriptPath)
    {
        var state = State.Unknown;
        fullScriptPath = string.Empty;
        if (!string.IsNullOrWhiteSpace(fileOrDirectoryPath))
        {
            if (fileSystem.IsPathRooted(fileOrDirectoryPath))
            {
                state = TryResolveFullPath(fileOrDirectoryPath, fileOrDirectoryPath, out fullScriptPath);
                if (state == State.NotFound)
                {
                    return false;
                }
            }
            else
            {
                foreach (var path in GetPaths().Distinct())
                {
                    fullScriptPath = Path.Combine(path, fileOrDirectoryPath);
                    state = TryResolveFullPath(path, fullScriptPath, out fullScriptPath);
                    if (state is State.Found or State.Error)
                    {
                        break;
                    }
                }
            }
        }

        if (state is State.NotFound)
        {
            log.Error(ErrorId.CannotFind, $"Cannot find \"{fileOrDirectoryPath}\".");
        }

        return state == State.Found;
    }

    private State TryResolveFullPath(string basePath, string fullPath, out string fullScriptPath)
    {
        fullScriptPath = string.Empty;
        var isFileExist = fileSystem.IsFileExist(fullPath);
        log.Trace(() => [new Text($"Try to find file \"{fullPath}\" in \"{basePath}\": {isFileExist}.")]);
        if (isFileExist)
        {
            fullScriptPath = fullPath;
            return State.Found;
        }

        var isDirectoryExist = fileSystem.IsDirectoryExist(fullPath);
        log.Trace(() => [new Text($"Try to find directory \"{fullPath}\" in \"{basePath}\": {isDirectoryExist}.")]);
        if (!isDirectoryExist)
        {
            return State.NotFound;
        }

        var scripts = fileSystem.EnumerateFileSystemEntries(fullPath, "*.csx", SearchOption.TopDirectoryOnly).ToList();
        switch (scripts.Count)
        {
            case 1:
                fullScriptPath = scripts[0];
                return State.Found;
            case > 1:
                log.Error(ErrorId.CannotFind, new Text($"Specify which script file to use because the folder \"{fullPath}\" contains more than one script file.", Color.Error));
                return State.Error;
            default:
                return State.Unknown;
        }
    }

    private enum State
    {
        Unknown,
        Found,
        Error,
        NotFound
    }

    private IEnumerable<string> GetPaths()
    {
        yield return environment.GetPath(SpecialFolder.Script);
        yield return environment.GetPath(SpecialFolder.Working);
    }
}