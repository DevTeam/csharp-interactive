namespace CSharpInteractive.Core;

using HostApi;

internal readonly record struct StatisticsItem(StatisticsType Type, Text[] Text)
{
    public bool Equals(StatisticsItem other) =>
        Type == other.Type && Text.SequenceEqual(other.Text);

    public override int GetHashCode() =>
        Text.Aggregate((int)Type, HashCode.Combine);
}