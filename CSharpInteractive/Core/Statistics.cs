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
    private readonly List<CommandLineInfo> _info = [];
    
    public bool IsEmpty => Errors.Count == 0 && Warnings.Count == 0 && CommandLines.Count == 0;

    public IReadOnlyCollection<Text[]> Errors
    {
        get
        {
            lock (_lockObject)
            {
                return new ReadOnlyCollection<Text[]>(_errors);   
            }
        }
    }

    public IReadOnlyCollection<Text[]> Warnings
    {
        get
        {
            lock (_lockObject)
            {
                return new ReadOnlyCollection<Text[]>(_warnings);   
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