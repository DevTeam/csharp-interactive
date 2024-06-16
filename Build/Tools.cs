using System.Xml;
using HostApi;
using Microsoft.Build.Tasks;
using NuGet.Packaging;
using NuGet.Versioning;
// ReSharper disable CheckNamespace

internal static class Tools
{
    public static bool UnderTeamCity => Environment.GetEnvironmentVariable("TEAMCITY_VERSION") != default;
    
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

        if (int.TryParse(string.Concat(curVersion.Release.Where(char.IsNumber)), out var num))
        {
            return $"{string.Concat(curVersion.Release.Where(i => !char.IsNumber(i)))}{num + 1}";
        }

        return release;
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

    public static bool Succeed(int? exitCode, string shortName)
    {
        if (exitCode == 0)
        {
            return true;
        }

        Error($"{shortName} failed.");
        Exit();
        return false;
    }

    public static async Task<bool> Succeed(Task<int?> exitCodeTask, string shortName) =>
        Succeed(await exitCodeTask, shortName);

    private static bool CheckBuildResult(IBuildResult result)
    {
        if (result.ExitCode == 0)
        {
            return true;
        }

        foreach (var failedTest in
                 from testResult in result.Tests
                 where testResult.State == TestState.Failed
                 select testResult.ToString())
        {
            Error(failedTest);
        }

        Error($"{result.StartInfo.ShortName} failed");
        return false;
    }

    public static void Succeed(IBuildResult result)
    {
        if (!CheckBuildResult(result))
        {
            Exit();
        }
    }

    public static async Task<bool> Succeed(Task<IBuildResult> resultTask)
    {
        if (CheckBuildResult(await resultTask))
        {
            return true;
        }

        Exit();
        return true;
    }

    public static async Task<bool> Succeed(Task<IBuildResult[]> resultsTask)
    {
        if ((await resultsTask).All(CheckBuildResult))
        {
            return true;
        }

        Exit();
        return true;
    }

    public static void Exit()
    {
        if (!Tools.UnderTeamCity)
        {
            var foregroundColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                var timeout = TimeSpan.FromSeconds(10);
                var period = TimeSpan.FromMilliseconds(10);
                while (timeout > period)
                {
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            return;
                        }
                        else
                        {
                            break;
                        }
                    }

                    Thread.Sleep(period);
                    timeout -= period;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"Continue this build? {(int)timeout.TotalSeconds:00} y/n");
                }
            }
            finally
            {
                Console.ForegroundColor = foregroundColor;
            }
        }
        
        Environment.Exit(1);
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