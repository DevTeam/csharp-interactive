// ReSharper disable UnusedMember.Global

namespace CSharpInteractive.Core;

internal interface INuGetEnvironment
{
    IEnumerable<string> Sources { get; }

    IEnumerable<string> FallbackFolders { get; }

    string PackagesPath { get; }
}