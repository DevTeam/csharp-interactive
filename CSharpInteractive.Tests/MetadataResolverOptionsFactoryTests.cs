namespace CSharpInteractive.Tests;

using Core;
using CSharpInteractive;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;

public class MetadataResolverOptionsFactoryTests
{
    private readonly Mock<Func<MetadataReferenceResolver>> _metadataResolverFactory = new();

    [Fact]
    public void ShouldSetupMetadataResolver()
    {
        // Given
        var factory = CreateInstance();
        var resolver = Mock.Of<MetadataReferenceResolver>();
        _metadataResolverFactory.Setup(i => i()).Returns(resolver);
        
        // When
        var options = factory.Create(ScriptOptions.Default);
        var actualResolver = options.MetadataResolver;

        // Then
        actualResolver.ShouldBe(resolver);
    }

    private MetadataResolverOptionsFactory CreateInstance() =>
        new(_metadataResolverFactory.Object);
}