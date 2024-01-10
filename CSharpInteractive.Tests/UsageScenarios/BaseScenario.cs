// ReSharper disable MemberCanBeProtected.Global
namespace CSharpInteractive.Tests.UsageScenarios;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using CSharpInteractive;
using HostApi;
using Environment = System.Environment;

[SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
public class BaseScenario : IHost, IDisposable
{
    private readonly string _tempDir;
    private readonly string _prevCurDir;

    public BaseScenario()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()[..4]);
        Directory.CreateDirectory(_tempDir);
        _prevCurDir = Environment.CurrentDirectory;
        Environment.CurrentDirectory = _tempDir;
    }

    // ReSharper disable once MemberCanBeProtected.Global
    public IHost Host => this;

    public IReadOnlyList<string> Args { get; } = new List<string>
    {
        "Arg1",
        "Arg2"
    };

    public IProperties Props { get; } = new Properties();

    public T GetService<T>() => Composition.Shared.Resolve<T>();

    public void WriteLine() => Composition.Shared.Resolve<IHost>().WriteLine();

    public void WriteLine<T>(T line, Color color = Color.Default) => Composition.Shared.Resolve<IHost>().WriteLine(line, color);

    public void Error(string? error, string? errorId = default) => Composition.Shared.Resolve<IHost>().Error(error, errorId);

    public void Warning(string? warning) => Composition.Shared.Resolve<IHost>().Warning(warning);

    public void Info(string? text) => Composition.Shared.Resolve<IHost>().Info(text);

    public void Trace(string? trace, string? origin = default) => Composition.Shared.Resolve<IHost>().Trace(trace, origin);

    private class Properties : IProperties
    {
        private readonly Dictionary<string, string> _dict = new()
        {
            {"TEAMCITY_VERSION", "2021.2"},
            {"TEAMCITY_PROJECT_NAME", "Samples"}
        };

        public int Count => _dict.Count;

        public string this[string key]
        {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _dict.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool TryGetValue(string key, out string value) => _dict.TryGetValue(key, out value!);
    }

    void IDisposable.Dispose()
    {
        try
        {
            Directory.Delete(_tempDir, true);
        }
        catch
        {
            // ignored
        }
        
        Environment.CurrentDirectory = _prevCurDir;
    }
}