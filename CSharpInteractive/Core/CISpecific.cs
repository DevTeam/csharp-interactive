// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Core;

using Pure.DI;

internal class CISpecific<T>(
    ICISettings settings,
    [Tag(Tag.Base)] Func<T> defaultFactory,
    [Tag(Tag.TeamCity)] Func<T> teamcityFactory,
    [Tag(Tag.Ansi)] Func<T> ansiFactory)
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