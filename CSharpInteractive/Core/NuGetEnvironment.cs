// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;

[ExcludeFromCodeCoverage]
internal class NuGetEnvironment(
    IEnvironment environment,
    ISettings settings) : INuGetEnvironment, ITraceSource
{

    public IEnumerable<string> Sources =>
        settings.NuGetSources
            .Concat(NuGet.Configuration.SettingsUtility.GetEnabledSources(GetNuGetSettings()).Select(i => i.Source))
            .Distinct();

    public IEnumerable<string> FallbackFolders =>
        NuGet.Configuration.SettingsUtility.GetFallbackPackageFolders(GetNuGetSettings()).Distinct();

    public string PackagesPath =>
        NuGet.Configuration.SettingsUtility.GetGlobalPackagesFolder(GetNuGetSettings());

    public IEnumerable<Text> Trace
    {
        get
        {
            yield return new Text($"PackagesPath: {PackagesPath}");
            yield return Text.NewLine;
            yield return new Text($"NuGetSources: {string.Join(';', Sources)}");
            yield return Text.NewLine;
            yield return new Text($"NuGetFallbackFolders: {string.Join(';', FallbackFolders)}");
            yield return Text.NewLine;
        }
    }

    private NuGet.Configuration.ISettings GetNuGetSettings() =>
        NuGet.Configuration.Settings.LoadDefaultSettings(environment.GetPath(SpecialFolder.Script));
}