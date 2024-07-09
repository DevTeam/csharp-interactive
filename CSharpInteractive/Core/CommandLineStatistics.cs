namespace CSharpInteractive.Core;

using System.Collections.ObjectModel;

internal class CommandLineStatistics : ICommandLineStatistics
{
    private readonly List<CommandLineInfo> _info = [];

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
}