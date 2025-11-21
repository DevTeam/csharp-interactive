// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable ConvertToExtensionBlock
namespace HostApi.Internal.DotNet;

using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

[ExcludeFromCodeCoverage]
internal static class DotNetCommandLineExtensions
{
    private const string TeamcityLoggerName = "logger://teamcity";

    internal static CommandLine CreateCommandLine(this IHost host, string executablePath) =>
        new(host.GetExecutablePath(executablePath));

    [Pure]
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

    [Pure]
    public static string GetShortName(this string baseName, string description, string shortName, params string[] paths)
    {
        if (!string.IsNullOrWhiteSpace(shortName))
        {
            return shortName;
        }

        var name = new StringBuilder();
        name.Append(baseName);
        foreach (var path in paths)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                continue;
            }

            if (name.Length > 0)
            {
                name.Append(' ');
            }

            var fileName = Path.GetFileName(path);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                name.Append(path);
                continue;
            }
            
            name.Append(fileName);
        }
        
        name.Append(" - ");
        name.Append(description);
        return name.ToString();
    }

    [Pure]
    public static CommandLine AddMSBuildLoggers(this CommandLine cmd, IHost host, DotNetVerbosity? verbosity = null)
    {
        // ReSharper disable once UseDeconstruction
        var components = host.GetService<HostComponents>();
        var virtualContext = components.VirtualContext;
        var settings = components.DotNetSettings;
        return settings.LoggersAreRequired
            ? cmd
                .AddArgs("/noconsolelogger")
                .AddMSBuildArgs(("/l", $"TeamCity.MSBuild.Logger.TeamCityMSBuildLogger,{virtualContext.Resolve(settings.DotNetMSBuildLoggerDirectory)}/TeamCity.MSBuild.Logger.dll;TEAMCITY;PLAIN;NOSUMMARY"))
                .AddProps("-p",
                    ("VSTestLogger", TeamcityLoggerName),
                    ("VSTestTestAdapterPath", virtualContext.Resolve(settings.DotNetVSTestLoggerDirectory)),
                    ("VSTestVerbosity", (verbosity.HasValue ? (verbosity.Value >= DotNetVerbosity.Normal ? verbosity.Value : DotNetVerbosity.Normal) : DotNetVerbosity.Normal).ToString().ToLowerInvariant()))
                .AddVars(("TEAMCITY_SERVICE_MESSAGES_PATH", virtualContext.Resolve(settings.TeamCityMessagesPath)))
            : cmd;
    }

    [Pure]
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

    [Pure]
    public static CommandLine AddVSTestLoggers(this CommandLine cmd, IHost host) =>
        host.GetService<HostComponents>().DotNetSettings.LoggersAreRequired
            ? cmd.AddMSBuildArgs(("--Logger", TeamcityLoggerName))
            : cmd;

    [Pure]
    public static CommandLine AddNotEmptyArgs(this CommandLine cmd, params string[] args) =>
        cmd.AddArgs(args.Where(i => !string.IsNullOrWhiteSpace(i)).ToArray());

    [Pure]
    public static CommandLine AddArgs(this CommandLine cmd, params (string name, string? value)[] args) =>
        cmd.AddArgs((
                from arg in args
                where !string.IsNullOrWhiteSpace(arg.value)
                select new[] {arg.name, arg.value})
            .SelectMany(i => i)
            .ToArray());

    [Pure]
    public static CommandLine AddMSBuildArgs(this CommandLine cmd, params (string name, string? value)[] args) =>
        cmd.AddArgs((
                from arg in args
                where !string.IsNullOrWhiteSpace(arg.value)
                select $"{arg.name}:{arg.value}")
            .ToArray());

    [Pure]
    public static CommandLine AddBooleanArgs(this CommandLine cmd, params (string name, bool? value)[] args) =>
        cmd.AddArgs((
                from arg in args
                where arg.value ?? false
                select arg.name)
            .ToArray());

    [Pure]
    public static CommandLine AddProps(this CommandLine cmd, string propertyName, params (string name, string value)[] props) =>
        cmd.AddArgs(props.Select(i => $"{propertyName}:{i.name}={i.value}")
            .ToArray());
    
    // ReSharper disable once UnusedParameter.Global
    public static string[] ToArgs<T>(this T value, string name, string collectionSeparator)
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        var valueStr = value?.ToString();
        if (string.IsNullOrWhiteSpace(valueStr))
        {
            return [];
        }
        
        if (!string.IsNullOrWhiteSpace(collectionSeparator))
        {
            // ReSharper disable once HeapView.PossibleBoxingAllocation
            return [$"{name}{collectionSeparator}{value}"];
        }
        
        return [name, valueStr!];
    }
    
    public static string[] ToArgs<T>(this IEnumerable<(string name, T value)> values, string name, string separator)
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        return values.SelectMany(i => new [] { name, $"{i.name}{separator}{i.value}"}).ToArray();
    }
    
    public static string[] ToArgs(this IEnumerable<string> values, string name, string collectionSeparator)
    {
        if (string.IsNullOrEmpty(collectionSeparator))
        {
            return values.SelectMany(value => string.IsNullOrWhiteSpace(value) ? [] : new[] {name, value}).ToArray();
        }

        var str = string.Join(collectionSeparator, values);
        if (string.IsNullOrWhiteSpace(str))
        {
            return [];
        }

        return string.IsNullOrWhiteSpace(str) ? [] : [name, str];
    }
    
    // ReSharper disable once UnusedParameter.Global
    public static string[] ToArgs(this TimeSpan? value, string name, string collectionSeparator) => 
        value.HasValue ? [name, value.Value.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)] : [];

    // ReSharper disable once HeapView.PossibleBoxingAllocation
    public static string ToArg<T>(this T value) => value?.ToString() ?? "";
}