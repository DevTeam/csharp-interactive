// ReSharper disable ClassNeverInstantiated.Global
namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class UniqueNameGenerator : IUniqueNameGenerator
{
    public string Generate() => Guid.NewGuid().ToString().Replace("-", string.Empty)[..8];
}