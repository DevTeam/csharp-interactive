namespace CSharpInteractive.Core;

using System.Collections.ObjectModel;

internal class CommandLineStatistics : ICommandLineStatisticsRegistry, ICommandLineStatistics
{
    private readonly List<CommandLineInfo> _info = [];
    private readonly Dictionary<int, List<Text[]>> _warnings = [];
    private readonly Dictionary<int, List<Text[]>> _errors = [];

    public bool IsEmpty => CommandLines.Count == 0;

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

    public void RegisterWarning(ProcessInfo processInfo, Text[] warning)
    {
        lock (_info)
        {
            if (!_warnings.TryGetValue(processInfo.RunId, out var warnings))
            {
                warnings = new List<Text[]>();
                _warnings.Add(processInfo.RunId, warnings);
            }
            
            warnings.Add(warning);
        }
    }

    public void RegisterError(ProcessInfo processInfo, Text[] error)
    {
        lock (_info)
        {
            if (!_errors.TryGetValue(processInfo.RunId, out var errors))
            {
                errors = new List<Text[]>();
                _errors.Add(processInfo.RunId, errors);
            }
            
            errors.Add(error);
        }
    }
}