namespace CSharpInteractive.Core;

internal interface IFilePathResolver
{
    bool TryResolve(string? fileOrDirectoryPath, out string fullScriptPath);
}