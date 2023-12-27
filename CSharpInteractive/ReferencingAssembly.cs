namespace CSharpInteractive;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class ReferencingAssembly(string name, string filePath)
{
    public readonly string Name = name;
    public readonly string FilePath = filePath;

    public override string ToString() => FilePath;
}