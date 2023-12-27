namespace CSharpInteractive;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;

internal class MetadataResolverOptionsFactory(Func<MetadataReferenceResolver> metadataResolverFactory) : IScriptOptionsFactory
{

    public ScriptOptions Create(ScriptOptions baseOptions) =>
        baseOptions.WithMetadataResolver(metadataResolverFactory());
}