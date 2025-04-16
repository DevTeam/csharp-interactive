// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Core;

using Pure.DI;
using static Pure.DI.Tag;

internal class CISpecific<T>(
    ICISettings settings,
    [Tag(BaseTag)] Func<T> defaultFactory,
    [Tag(TeamCityTag)] Func<T> teamcityFactory,
    [Tag(AnsiTag)] Func<T> ansiFactory)
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