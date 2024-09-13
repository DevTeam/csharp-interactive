namespace CSharpInteractive.Tests;

using System.Collections.Immutable;
using Core;
using Microsoft.CodeAnalysis;

public class MetadataResolverTests
{
    private readonly Mock<IEnvironment> _environment = new();
    private readonly PortableExecutableReference _reference1 = MetadataReference.CreateFromFile(typeof(MetadataResolver).Assembly.Location);
    private readonly PortableExecutableReference _reference2 = MetadataReference.CreateFromFile(typeof(TestMetadataResolver).Assembly.Location);

    public MetadataResolverTests()
    {
        _environment.Setup(i => i.GetPath(SpecialFolder.Script)).Returns("ScriptDir");
        _environment.Setup(i => i.GetPath(SpecialFolder.Working)).Returns("WorkingDir");
    }

    [Fact]
    public void ShouldSetupBaseAndSearchDirectories()
    {
        // Given
        var resolver = CreateInstance();

        // When

        // Then
        resolver.BaseDirectory.ShouldBe(Path.GetFullPath("ScriptDir"));
        resolver.SearchPaths.ShouldBe([Path.GetFullPath("ScriptDir"), Path.GetFullPath("WorkingDir")]);
    }

    [Theory]
    [InlineData("Abc")]
    [InlineData("nuget")]
    [InlineData("NuGet")]
    [InlineData("nugetAbc")]
    public void ShouldResolveOrdinaryReferences(string reference)
    {
        // Given
        var resolver = CreateInstance(_reference1, _reference2);

        // When
        var resolvedReferences = resolver.ResolveReference(reference, "Dir", new MetadataReferenceProperties());

        // Then
        resolvedReferences.ShouldBe(new[] {_reference1, _reference2});
    }

    [Theory]
    [InlineData("nuget: abc")]
    [InlineData("NuGet: abc")]
    [InlineData("NuGet:abc")]
    [InlineData("NuGet: abc, 1.2.3")]
    public void ShouldResolveReferencesWhenNuGet(string reference)
    {
        // Given
        var resolver = CreateInstance(_reference1, _reference2);

        // When
        var resolvedReferences = resolver.ResolveReference(reference, "Dir", new MetadataReferenceProperties());

        // Then
        resolvedReferences.Length.ShouldBe(1);
        resolvedReferences.ShouldNotContain(_reference1);
        resolvedReferences.ShouldNotContain(_reference2);
    }

    private TestMetadataResolver CreateInstance(params PortableExecutableReference[] references) =>
        // ReSharper disable once UseCollectionExpression
        new(_environment.Object, references.ToImmutableArray());

    private class TestMetadataResolver(IEnvironment environment, ImmutableArray<PortableExecutableReference> references) : MetadataResolver(environment)
    {
        protected override ImmutableArray<PortableExecutableReference> ShouldResolveReferenceInternal(string reference, string? baseFilePath, MetadataReferenceProperties properties) =>
            references;
    }
}