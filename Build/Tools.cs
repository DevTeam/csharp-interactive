using System.Xml;
using HostApi;
using NuGet.Versioning;
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming

internal static class Tools
{
    public static bool CI => 
        Environment.GetEnvironmentVariable("TEAMCITY_VERSION") is not null
        || Environment.GetEnvironmentVariable("CI") == "true";
    
    public static NuGetVersion GetNextNuGetVersion(NuGetRestoreSettings settings, NuGetVersion defaultVersion)
    {
        var floatRange = defaultVersion.Release != string.Empty
            ? new FloatRange(NuGetVersionFloatBehavior.Prerelease, defaultVersion)
            : new FloatRange(NuGetVersionFloatBehavior.Minor, defaultVersion);

        return GetService<INuGet>()
            .Restore(settings.WithHideWarningsAndErrors(true).WithVersionRange(new VersionRange(defaultVersion, floatRange)))
            .Where(i => i.Name == settings.PackageId)
            .Select(i => i.NuGetVersion)
            .Select(i => defaultVersion.Release != string.Empty
                ? new NuGetVersion(i.Major, i.Minor, i.Patch, GetNextRelease(i, defaultVersion.Release))
                : new NuGetVersion(i.Major, i.Minor, i.Patch + 1))
            .Max() ?? defaultVersion;
    }

    private static string GetNextRelease(SemanticVersion curVersion, string release)
    {
        if (!curVersion.Release.StartsWith(release, StringComparison.InvariantCultureIgnoreCase))
        {
            return release;
        }

        return int.TryParse(string.Concat(curVersion.Release.Where(char.IsNumber)), out var num) 
            ? $"{string.Concat(curVersion.Release.Where(i => !char.IsNumber(i)))}{num + 1}"
            : release;
    }

    public static string GetProperty(string name, string defaultProp, bool showWarning = false)
    {
        if (Props.TryGetValue(name, out var prop) && !string.IsNullOrWhiteSpace(prop))
        {
            WriteLine($"{name}: {prop}", Color.Highlighted);
            return prop;
        }

        var message = $"The property \"{name}\" was not defined, the default value \"{defaultProp}\" was used.";
        if (showWarning)
        {
            Warning(message);
        }
        else
        {
            Info(message);
        }

        return defaultProp;
    }
    
    public static bool TryGetCoverage(string dotCoverReportXml, out int coveragePercentage)
    {
        var dotCoverReportDoc = new XmlDocument();
        dotCoverReportDoc.Load(dotCoverReportXml);
        var coveragePercentageValue = dotCoverReportDoc.SelectNodes("Root")?.Item(0)?.Attributes?["CoveragePercent"]?.Value;
        return int.TryParse(coveragePercentageValue, out coveragePercentage);
    }

    public static bool HasLinuxDocker()
    {
        var hasLinuxDocker = false;
        new DockerCustom("info").WithShortName("Defining a docker container type")
            .Run(output =>
            {
                WriteLine("    " + output.Line, Color.Details);
                if (output.Line.Contains("OSType: linux"))
                {
                    hasLinuxDocker = true;
                }
            });
        
        return hasLinuxDocker;
    }
}