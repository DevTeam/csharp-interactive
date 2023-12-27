// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeProtected.Global
namespace CSharpInteractive;

using Microsoft.CodeAnalysis;

internal class SourceResolver(
    IEnvironment environment,
    IScriptContentReplacer scriptContentReplacer,
    ITextReplacer textReplacer) : SourceReferenceResolver
{
    private readonly Lazy<SourceFileResolver> _baseResolver = new(() => CreateResolver(environment));

    public override bool Equals(object? other) => _baseResolver.Equals(other);

    public override int GetHashCode() => _baseResolver.GetHashCode();

    public override string? NormalizePath(string path, string? baseFilePath) =>
        _baseResolver.Value.NormalizePath(path, baseFilePath);

    public override string? ResolveReference(string path, string? baseFilePath) =>
        _baseResolver.Value.ResolveReference(path, baseFilePath);

    public override Stream OpenRead(string resolvedPath) =>
        textReplacer.Replace(OpenReadInternal(resolvedPath), scriptContentReplacer.Replace);

    protected virtual Stream OpenReadInternal(string resolvedPath) => _baseResolver.Value.OpenRead(resolvedPath);

    internal IEnumerable<string> SearchPaths => _baseResolver.Value.SearchPaths;

    internal string? BaseDirectory => _baseResolver.Value.BaseDirectory;

    private static SourceFileResolver CreateResolver(IEnvironment environment)
    {
        var scriptDirectory = Path.GetFullPath(environment.GetPath(SpecialFolder.Script));
        var workingDirectory = Path.GetFullPath(environment.GetPath(SpecialFolder.Working));
        return new SourceFileResolver(new []{scriptDirectory, workingDirectory}, scriptDirectory);
    }
}