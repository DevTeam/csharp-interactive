// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Tests.Integration.Core;

internal interface IFileSystem
{
    string CreateTempFilePath();

    void DeleteFile(string file);

    void AppendAllLines(string file, IEnumerable<string> lines);
}