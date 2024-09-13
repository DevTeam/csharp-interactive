// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Scripting;

internal class SourceFileScriptOptionsFactory(Func<SourceReferenceResolver> sourceReferenceResolverFactory) : IScriptOptionsFactory
{
    public ScriptOptions Create(ScriptOptions baseOptions) =>
        baseOptions.WithSourceResolver(sourceReferenceResolverFactory());
}