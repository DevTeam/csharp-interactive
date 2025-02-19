﻿namespace CSharpInteractive;

using Core;

/// <summary>
/// Class entry point to the application.
/// </summary>
public class Program
{
    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// Method entry point to the application.
    /// </summary>
    /// <returns></returns>
    public static int Main()
    {
        using (Composition.Shared)
        {
            return Composition.Shared.Root.Program.Run();
        }
    }

    private readonly ILog<Program> _log;
    private readonly IEnumerable<IActive> _activeObjects;
    private readonly IInfo _info;
    private readonly ISettings _settings;
    private readonly IExitTracker _exitTracker;
    private readonly Func<IScriptRunner> _runner;
    private readonly IStatistics _statistics;

    internal Program(
        ILog<Program> log,
        IEnumerable<IActive> activeObjects,
        IInfo info,
        ISettings settings,
        IExitTracker exitTracker,
        Func<IScriptRunner> runner,
        IStatistics statistics)
    {
        _log = log;
        _activeObjects = activeObjects;
        _info = info;
        _settings = settings;
        _exitTracker = exitTracker;
        _runner = runner;
        _statistics = statistics;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal int Run()
    {
        if (_settings.ShowVersionAndExit)
        {
            _info.ShowVersion();
            return 0;
        }

        _info.ShowHeader();

        if (_settings.ShowHelpAndExit)
        {
            _info.ShowHelp();
            return 0;
        }

        using var exitToken = _exitTracker.Track();
        try
        {
            using (Disposable.Create(_activeObjects.Select(i => i.Activate()).ToArray()))
            {
                var result = _runner().Run();
                if (_statistics.Items.Any(i => i.Type == StatisticsType.Error))
                {
                    result = 1;
                }

                return result;
            }
        }
        catch (Exception error)
        {
            _log.Error(ErrorId.Unhandled, error);
            return 1;
        }
        finally
        {
            _info.ShowFooter();
        }
    }
}