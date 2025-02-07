// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf

namespace CSharpInteractive.Core;

using System.Collections.ObjectModel;
using System.Diagnostics;

internal class Statistics : IStatisticsRegistry, IStatistics, ICommandLineStatisticsRegistry
{
    private readonly object _lockObject = new();
    private readonly Stopwatch _stopwatch = new();
    private readonly List<Text[]> _errors = [];
    private readonly List<Text[]> _warnings = [];
    private readonly List<Text[]> _summary = [];
    private readonly List<CommandLineInfo> _info = [];

    public bool IsEmpty => Errors.Count == 0 && Warnings.Count == 0 && CommandLines.Count == 0;

    public IReadOnlyCollection<Text[]> Errors
    {
        get
        {
            lock (_lockObject)
            {
                return _errors.AsReadOnly();
            }
        }
    }

    public IReadOnlyCollection<Text[]> Warnings
    {
        get
        {
            lock (_lockObject)
            {
                return _warnings.AsReadOnly();
            }
        }
    }

    public IReadOnlyCollection<Text[]> Summary
    {
        get
        {
            lock (_lockObject)
            {
                return _summary.AsReadOnly();
            }
        }
    }

    public TimeSpan TimeElapsed => _stopwatch.Elapsed;

    public IDisposable Start()
    {
        _stopwatch.Start();
        return Disposable.Create(() => _stopwatch.Stop());
    }

    public void RegisterError(Text[] error)
    {
        error = error.Trim();
        if (!error.IsEmptyOrWhiteSpace())
        {
            lock (_lockObject)
            {
                _errors.Add(error);
            }
        }
    }

    public void RegisterWarning(Text[] warning)
    {
        warning = warning.Trim();
        if (!warning.IsEmptyOrWhiteSpace())
        {
            lock (_lockObject)
            {
                _warnings.Add(warning);
            }
        }
    }

    public void RegisterSummary(Text[] summary)
    {
        summary = summary.Trim();
        if (!summary.IsEmptyOrWhiteSpace())
        {
            lock (_lockObject)
            {
                _summary.Add(summary);
            }
        }
    }

    public IReadOnlyCollection<CommandLineInfo> CommandLines
    {
        get
        {
            lock (_info)
            {
                return new ReadOnlyCollection<CommandLineInfo>(_info);
            }
        }
    }

    public void Register(CommandLineInfo info)
    {
        lock (_info)
        {
            _info.Add(info);
        }
    }
}