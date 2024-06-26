// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotDisposedResourceIsReturned
namespace CSharpInteractive;

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
        get => properties[key];
        set
        {
            properties[key] = value;
            teamCityWriter.WriteBuildParameter($"system.{key}", value);
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => properties.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)properties).GetEnumerator();

    public bool TryGetValue(string key, out string value) => properties.TryGetValue(key, out value);
}