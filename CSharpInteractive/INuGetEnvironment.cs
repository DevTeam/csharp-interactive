// ReSharper disable UnusedMember.Global
namespace CSharpInteractive;

internal interface INuGetEnvironment
{
    IEnumerable<string> Sources { get; }

    IEnumerable<string> FallbackFolders { get; }

    string PackagesPath { get; }
}