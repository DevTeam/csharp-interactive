namespace CSharpInteractive;

using HostApi;

internal interface INuGetRestoreService
{
    bool TryRestore(NuGetRestoreSettings settings, out string projectAssetsJson);
}