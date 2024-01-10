namespace CSharpInteractive;

[ExcludeFromCodeCoverage]
internal class ScriptCommand(string originName, string script, bool isInternal = false) : ICommand
{
    public readonly string Script = script;

    public string Name { get; } = originName;

    public bool Internal { get; } = isInternal;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        var other = (ScriptCommand)obj;
        return Name == other.Name && Script == other.Script;
    }

    public override int GetHashCode() => HashCode.Combine(Name, Script);

    public override string ToString() => Name;
}