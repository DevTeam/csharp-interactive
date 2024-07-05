// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
namespace HostApi.Internal.DotNet;

[ExcludeFromCodeCoverage]
internal static class DotNetCommandLineExtensions
{
    private const string TeamcityLoggerName = "logger://teamcity";

    internal static CommandLine CreateCommandLine(this IHost host, string executablePath) =>
        new(host.GetExecutablePath(executablePath));

    private static string GetExecutablePath(this IHost host, string executablePath)
    {
        if (!string.IsNullOrWhiteSpace(executablePath))
        {
            return executablePath;
        }

        executablePath = host.GetService<HostComponents>().DotNetSettings.DotNetExecutablePath;
        return host.GetService<HostComponents>().VirtualContext.IsActive
            ? Path.GetFileNameWithoutExtension(executablePath)
            : executablePath;
    }
    
    public static string GetShortName(this string baseName, string shortName, string path = "")
    {
        if (!string.IsNullOrWhiteSpace(shortName))
        {
            return shortName;
        }

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (string.IsNullOrWhiteSpace(path))
        {
            return baseName;
        }

        return $"{baseName} {Path.GetFileName(path)}";
    }

    public static CommandLine AddMSBuildLoggers(this CommandLine cmd, IHost host, DotNetVerbosity? verbosity = default)
    {
        // ReSharper disable once UseDeconstruction
        var components = host.GetService<HostComponents>();
        var virtualContext = components.VirtualContext;
        var settings = components.DotNetSettings;
        return settings.LoggersAreRequired
            ? cmd
                .AddArgs("/noconsolelogger")
                .AddMSBuildArgs(("/l", $"TeamCity.MSBuild.Logger.TeamCityMSBuildLogger,{virtualContext.Resolve(settings.DotNetMSBuildLoggerDirectory)}/TeamCity.MSBuild.Logger.dll;TeamCity;plain"))
                .AddProps("-p",
                    ("VSTestLogger", TeamcityLoggerName),
                    ("VSTestTestAdapterPath", virtualContext.Resolve(settings.DotNetVSTestLoggerDirectory)),
                    ("VSTestVerbosity", (verbosity.HasValue ? (verbosity.Value >= DotNetVerbosity.Normal ? verbosity.Value : DotNetVerbosity.Normal) : DotNetVerbosity.Normal).ToString().ToLowerInvariant()))
                .AddVars(("TEAMCITY_SERVICE_MESSAGES_PATH", virtualContext.Resolve(settings.TeamCityMessagesPath)))
            : cmd;
    }
    
    public static CommandLine AddTestLoggers(this CommandLine cmd, IHost host, IEnumerable<string> loggers)
    {
        // ReSharper disable once UseDeconstruction
        var settings = host.GetService<HostComponents>().DotNetSettings;
        if (settings.LoggersAreRequired)
        {
            loggers = loggers.Concat([TeamcityLoggerName]);
        }
        
        return cmd.AddArgs(loggers.Select(i => ("--logger", (string?)i)).ToArray());
    }
    
    public static CommandLine AddVSTestLoggers(this CommandLine cmd, IHost host) => 
        host.GetService<HostComponents>().DotNetSettings.LoggersAreRequired
            ? cmd.AddMSBuildArgs(("--Logger", TeamcityLoggerName))
            : cmd;

    public static CommandLine AddNotEmptyArgs(this CommandLine cmd, params string[] args) =>
        cmd.AddArgs(args.Where(i => !string.IsNullOrWhiteSpace(i)).ToArray());

    public static CommandLine AddArgs(this CommandLine cmd, params (string name, string? value)[] args) =>
        cmd.AddArgs((
                from arg in args
                where !string.IsNullOrWhiteSpace(arg.value)
                select new[] {arg.name, arg.value})
            .SelectMany(i => i)
            .ToArray());

    public static CommandLine AddMSBuildArgs(this CommandLine cmd, params (string name, string? value)[] args) =>
        cmd.AddArgs((
                from arg in args
                where !string.IsNullOrWhiteSpace(arg.value)
                select $"{arg.name}:{arg.value}")
            .ToArray());

    public static CommandLine AddBooleanArgs(this CommandLine cmd, params (string name, bool? value)[] args) =>
        cmd.AddArgs((
                from arg in args
                where arg.value ?? false
                select arg.name)
            .ToArray());

    public static CommandLine AddProps(this CommandLine cmd, string propertyName, params (string name, string value)[] props) =>
        cmd.AddArgs(props.Select(i => $"{propertyName}:{i.name}={i.value}")
            .ToArray());
}