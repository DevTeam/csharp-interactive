// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotDisposedResourceIsReturned

namespace CSharpInteractive.Core;

using System.Collections;
using System.Collections.ObjectModel;
using HostApi;

internal class Properties(
    ILog<Properties> log,
    ISettings settings) : IProperties
{
    private readonly Dictionary<string, string> _props = new(FilterPairs(settings.ScriptProperties));

    public int Count
    {
        get
        {
            lock (_props)
            {
                return _props.Count;
            }
        }
    }

    public string this[string key]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(key);
            return TryGetValue(key, out var value) ? value : string.Empty;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(key);
            lock (_props)
            {
                log.Trace(() => [new Text($"Props[\"{key}\"]=\"{value}\"")]);
                if (!string.IsNullOrEmpty(value))
                {
                    _props[key] = value;
                }
                else
                {
                    log.Trace(() => [new Text($"Props.Remove(\"{key}\")")]);
                    _props.Remove(key);
                }
            }
        }
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        ReadOnlyCollection<KeyValuePair<string, string>> props;
        lock (_props)
        {
            props = FilterPairs(_props).ToList().AsReadOnly();
        }

        return props.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool TryGetValue(string key, out string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        lock (_props)
        {
            _props.TryGetValue(key, out var curValue);
            value = curValue ?? string.Empty;
            log.Trace(() => [new Text($"Props[\"{key}\"] returns \"{curValue ?? "empty"}\"")]);
            return !string.IsNullOrEmpty(value);
        }
    }

    private static IEnumerable<KeyValuePair<string, string>> FilterPairs(IEnumerable<KeyValuePair<string, string>> pairs) =>
        pairs.Where(i => !string.IsNullOrWhiteSpace(i.Key) && !string.IsNullOrEmpty(i.Value));
}