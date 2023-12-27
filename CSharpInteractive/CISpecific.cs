// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
namespace CSharpInteractive;

using Pure.DI;

internal class CISpecific<T>(
    ICISettings settings,
    [Tag("Default")] Func<T> defaultFactory,
    [Tag("TeamCity")] Func<T> teamcityFactory,
    [Tag("Ansi")] Func<T> ansiFactory)
    : ICISpecific<T>
{

    public T Instance => settings.CIType switch
    {
        CIType.TeamCity => teamcityFactory(),
        CIType.GitLab => ansiFactory(),
        CIType.AzureDevOps => ansiFactory(),
        _ => defaultFactory()
    };
}