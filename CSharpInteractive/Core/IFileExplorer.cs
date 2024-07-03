namespace CSharpInteractive.Core;

internal interface IFileExplorer
{
    IEnumerable<string> FindFiles(string searchPattern, params string[] additionalVariables);
}