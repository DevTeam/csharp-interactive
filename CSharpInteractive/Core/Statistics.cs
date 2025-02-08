// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf

namespace CSharpInteractive.Core;

using System.Collections.ObjectModel;
using System.Diagnostics;
using HostApi;

internal class Statistics : IStatisticsRegistry, IStatistics, ICommandLineStatisticsRegistry
{
    private readonly object _lockObject = new();
    private readonly Stopwatch _stopwatch = new();
    private readonly List<StatisticsItem> _items = [];
    private readonly List<CommandLineInfo> _info = [];

    public bool IsEmpty
    {
        get
        {
            lock (_lockObject)
            {
                return _items.Count == 0 && CommandLines.Count == 0;
            }
        }
    }

    public IReadOnlyCollection<StatisticsItem> Items
    {
        get
        {
            lock (_lockObject)
            {
                return _items;
            }
        }
    }

    public TimeSpan TimeElapsed => _stopwatch.Elapsed;

    public IDisposable Start()
    {
        _stopwatch.Start();
        return Disposable.Create(() => _stopwatch.Stop());
    }

    public void Register(StatisticsType type, Text[] message)
    {
        message = message.Trim();
        if (!message.IsEmptyOrWhiteSpace())
        {
            lock (_lockObject)
            {
                _items.Add(new StatisticsItem(type, message));
            }
        }
    }

    public IReadOnlyCollection<CommandLineInfo> CommandLines
    {
        get
        {
            lock (_lockObject)
            {
                return new ReadOnlyCollection<CommandLineInfo>(_info);
            }
        }
    }

    public void Register(CommandLineInfo info)
    {
        lock (_lockObject)
        {
            _info.Add(info);
        }
    }
}