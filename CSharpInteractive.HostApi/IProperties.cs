namespace HostApi;

/// <summary>
/// Represents a set of properties.
/// </summary>
public interface IProperties : IEnumerable<KeyValuePair<string, string>>
{
    /// <summary>
    /// Total number of properties in the set.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Allows to get and set the value of a property by its key.
    /// </summary>
    /// <param name="key">Property key.</param>
    string this[string key] { get; set; }

    /// <summary>
    /// Tries to get a property by its key.
    /// </summary>
    /// <param name="key">Property key.</param>
    /// <param name="value">The resulting property value.</param>
    /// <returns><c>True</c> if the property is obtained, otherwise <c>False</c>.</returns>
    bool TryGetValue(string key, out string value);
}