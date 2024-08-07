namespace CSharpInteractive.Core;

[ExcludeFromCodeCoverage]
internal class SettingCommand<TOption>(TOption value) : ICommand
    where TOption: struct, Enum
{
    public readonly TOption Value = value;

    public string Name => $"Set verbosity level to {Value}";

    public bool Internal => false;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        var other = (SettingCommand<TOption>)obj;
        return Equals(Value, other.Value);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Name;
}