// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using HostApi.Internal.DotNet;

internal class TeamCityContext(
    IEnvironment environment,
    IDotNetEnvironment dotnetEnvironment,
    ICISettings ciSettings) :
    ITeamCityContext,
    IDotNetSettings
{
    [ThreadStatic] private static bool _teamCityIntegration;

    public bool TeamCityIntegration
    {
        set => _teamCityIntegration = value;
    }

    public bool LoggersAreRequired => _teamCityIntegration;

    public string DotNetExecutablePath => dotnetEnvironment.Path;

    public string DotNetMSBuildLoggerDirectory => Path.Combine(environment.GetPath(SpecialFolder.Bin), "msbuild");

    public string DotNetVSTestLoggerDirectory => Path.Combine(environment.GetPath(SpecialFolder.Bin), "vstest");

    public string TeamCityMessagesPath => ciSettings.ServiceMessagesPath;
}