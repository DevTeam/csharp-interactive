namespace CSharpInteractive.Core;

[ExcludeFromCodeCoverage]
internal class CodeCommand(bool isInternal = false) : ICommand
{

    public string Name => "Code";

    public bool Internal { get; } = isInternal;

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        var other = (CodeCommand)obj;
        return Internal == other.Internal;
    }

    public override int GetHashCode() => Internal.GetHashCode();
}