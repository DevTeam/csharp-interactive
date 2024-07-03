// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive.Core;

using System.Text.RegularExpressions;
using NuGet.Versioning;

[SuppressMessage("Performance", "SYSLIB1045:Convert to \'GeneratedRegexAttribute\'.")]
internal class AddNuGetReferenceCommandFactory(ILog<AddNuGetReferenceCommandFactory> log) : ICommandFactory<string>
{
    private static readonly Regex NuGetReferenceRegex = new("""^\s*#r\s+"nuget:\s*([^,\s]+?)(,(.+?)|\s*)"\s*$""", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    public int Order => 0;

    public IEnumerable<ICommand> Create(string replCommand)
    {
        var match = NuGetReferenceRegex.Match(replCommand);
        if (!match.Success)
        {
            yield break;
        }

        var packageIdStr = match.Groups[1].Value;
        var versionRangeStr = match.Groups[3].Value.Trim();

        VersionRange? versionRange = null;
        if (!string.IsNullOrWhiteSpace(versionRangeStr))
        {
            if (VersionRange.TryParse(versionRangeStr, out var curVersionRange))
            {
                versionRange = curVersionRange;
            }
            else
            {
                log.Error(ErrorId.CannotParsePackageVersion, $"Cannot parse the package version range \"{versionRangeStr}\".");
                yield break;
            }
        }

        log.Trace(() => [new Text($"REPL #r \"nuget:{packageIdStr}, {versionRange}\"")], string.Empty);
        yield return new AddNuGetReferenceCommand(packageIdStr, versionRange);
    }
}