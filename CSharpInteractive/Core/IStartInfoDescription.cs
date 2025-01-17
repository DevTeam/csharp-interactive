namespace CSharpInteractive.Core;

using HostApi;

internal interface IStartInfoDescription
{
    string GetDescription(IStartInfo? startInfo, int? processId = null);

    IEnumerable<Text> GetDescriptionText(IStartInfo? startInfo, int? processId = null);
}