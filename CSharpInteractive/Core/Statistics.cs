// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InvertIf
namespace CSharpInteractive.Core;

using System.Collections.ObjectModel;
using System.Diagnostics;

internal class Statistics : IStatistics
{
    private readonly Stopwatch _stopwatch = new();
    private readonly List<Text[]> _errors = [];
    private readonly List<Text[]> _warnings = [];
    private readonly List<ProcessResult> _processResult = [];

    public IReadOnlyCollection<Text[]> Errors
    {
        get
        {
            lock (_errors)
            {
                return new ReadOnlyCollection<Text[]>(_errors);   
            }
        }
    }

    public IReadOnlyCollection<Text[]> Warnings
    {
        get
        {
            lock (_warnings)
            {
                return new ReadOnlyCollection<Text[]>(_warnings);   
            }
        }
    }

    public IReadOnlyCollection<ProcessResult> ProcessResults
    {
        get
        {
            lock (_processResult)
            {
                return new ReadOnlyCollection<ProcessResult>(_processResult);
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
            lock (_errors)
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
            lock (_warnings)
            {
                _warnings.Add(warning);   
            }
        }
    }

    public void RegisterProcessResult(ProcessResult result)
    {
        lock (_processResult)
        {
            _processResult.Add(result);
        }
    }
}