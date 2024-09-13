// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

[ExcludeFromCodeCoverage]
internal class UniqueNameGenerator : IUniqueNameGenerator
{
    public string Generate() => Guid.NewGuid().ToString().Replace("-", string.Empty)[..8];
}