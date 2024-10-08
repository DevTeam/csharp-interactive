// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Tests.Integration.Core;

internal class FileSystem : IFileSystem
{
    public string CreateTempFilePath() => Path.GetTempFileName();

    public void DeleteFile(string file) => File.Delete(file);

    public void AppendAllLines(string file, IEnumerable<string> lines)
    {
        var path = Path.GetDirectoryName(file);
        if (!string.IsNullOrWhiteSpace(path) && !Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.AppendAllLines(file, lines);
    }
}