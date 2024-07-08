// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotDisposedResourceIsReturned
namespace CSharpInteractive.Core;

using System.Collections;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using Pure.DI;

internal class TeamCityProperties(
    [Tag("Default")] IProperties properties,
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    ITeamCityWriter teamCityWriter) : IProperties
{
    public int Count => properties.Count;

    public string this[string key]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(key);
            return properties[key];
        }
        set
        {
            ArgumentNullException.ThrowIfNull(key);
            properties[key] = value;
            teamCityWriter.WriteBuildParameter($"system.{key}", value);
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => properties.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)properties).GetEnumerator();

    public bool TryGetValue(string key, out string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        return properties.TryGetValue(key, out value);
    }
}