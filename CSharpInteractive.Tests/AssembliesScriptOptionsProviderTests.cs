// ReSharper disable RedundantNameQualifier

namespace CSharpInteractive.Tests;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Core;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;

public class AssembliesScriptOptionsProviderTests
{
    private readonly Mock<IAssembliesProvider> _assembliesProvider = new();

    [Fact]
    [SuppressMessage("ReSharper", "BuiltInTypeReferenceStyle")]
    public void ShouldAddReferencesForAssembliesWithLocation()
    {
        // Given
        var assembly1 = new Mock<Assembly>();
        var assembly2 = new Mock<Assembly>();
        assembly2.SetupGet(i => i.Location).Returns(Assembly.GetCallingAssembly().Location);
        _assembliesProvider.Setup(i => i.GetAssemblies(It.IsAny<IEnumerable<Type>>())).Returns([assembly1.Object, assembly2.Object]);
        var provider = CreateInstance();

        // When
        var options = provider.Create(ScriptOptions.Default);

        // Then
        _assembliesProvider.Verify(i => i.GetAssemblies(new[]
        {
            typeof(String),
            typeof(List<string>),
            typeof(Path),
            typeof(Enumerable),
            typeof(HttpRequestMessage),
            typeof(Thread),
            typeof(Task),
            typeof(IServiceCollection),
            typeof(ServiceCollectionContainerBuilderExtensions)
        }));

        options.MetadataReferences.Length.ShouldBe(ScriptOptions.Default.MetadataReferences.Length + 1);
    }

    [Fact]
    [SuppressMessage("ReSharper", "BuiltInTypeReferenceStyle")]
    public void ShouldAddImports()
    {
        // Given
        _assembliesProvider.Setup(i => i.GetAssemblies(It.IsAny<IEnumerable<Type>>())).Returns([]);
        var provider = CreateInstance();

        // When
        var options = provider.Create(ScriptOptions.Default);

        // Then
        options.Imports.ShouldBe(AssembliesScriptOptionsProvider.Refs.Select(i => i.ns).Where(i => !string.IsNullOrWhiteSpace(i)));
    }

    private AssembliesScriptOptionsProvider CreateInstance() =>
        new(Mock.Of<ILog<AssembliesScriptOptionsProvider>>(), _assembliesProvider.Object, CancellationToken.None);
}