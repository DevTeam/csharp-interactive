// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using System.Collections.Immutable;
using Pure.DI;
using static Pure.DI.Tag;

internal class Settings(
    RunningMode runningMode,
    IEnvironment environment,
    ICommandLineParser commandLineParser,
    ICodeSource consoleCodeSource,
    [Tag(LoadFileCodeTag)] Func<string, ICodeSource> fileCodeSourceFactory)
    : ISettings, ISettingSetter<VerbosityLevel>
{
#if NET9_0_OR_GREATER
    private readonly Lock _lockObject = new();
#else
    private readonly object _lockObject = new();
#endif
    private bool _isLoaded;
    private VerbosityLevel _verbosityLevel = VerbosityLevel.Normal;
    private InteractionMode _interactionMode = InteractionMode.Interactive;
    private bool _showHelpAndExit;
    private bool _showVersionAndExit;
    private IEnumerable<ICodeSource> _codeSources = [];
    private IReadOnlyList<string> _scriptArguments = ImmutableArray<string>.Empty;
    private IReadOnlyDictionary<string, string> _scriptProperties = new Dictionary<string, string>();
    private IEnumerable<string> _nuGetSources = [];

    public VerbosityLevel VerbosityLevel
    {
        get
        {
            EnsureLoaded();
            return _verbosityLevel;
        }
    }

    public InteractionMode InteractionMode
    {
        get
        {
            EnsureLoaded();
            return _interactionMode;
        }
    }

    public bool ShowHelpAndExit
    {
        get
        {
            EnsureLoaded();
            return _showHelpAndExit;
        }
    }

    public bool ShowVersionAndExit
    {
        get
        {
            EnsureLoaded();
            return _showVersionAndExit;
        }
    }

    public IEnumerable<ICodeSource> CodeSources
    {
        get
        {
            EnsureLoaded();
            return _codeSources;
        }
    }

    public IReadOnlyList<string> ScriptArguments
    {
        get
        {
            EnsureLoaded();
            return _scriptArguments;
        }
    }

    public IReadOnlyDictionary<string, string> ScriptProperties
    {
        get
        {
            EnsureLoaded();
            return _scriptProperties;
        }
    }

    public IEnumerable<string> NuGetSources
    {
        get
        {
            EnsureLoaded();
            return _nuGetSources;
        }
    }

    private void EnsureLoaded()
    {
        lock (_lockObject)
        {
            if (_isLoaded)
            {
                return;
            }

            _isLoaded = true;
            var defaultArgType = runningMode switch
            {
                RunningMode.Tool => CommandLineArgumentType.ScriptFile,
                RunningMode.Application => CommandLineArgumentType.ScriptArgument,
                _ => CommandLineArgumentType.ScriptFile
            };

            var args = commandLineParser.Parse(
                    environment.GetCommandLineArgs().Skip(1),
                    defaultArgType)
                .ToImmutableArray();

            var props = new Dictionary<string, string>();
            _scriptProperties = props;
            foreach (var (_, value, key) in args.Where(i => i.ArgumentType == CommandLineArgumentType.ScriptProperty))
            {
                props[key] = value;
            }

            _showHelpAndExit = args.Any(i => i.ArgumentType == CommandLineArgumentType.Help);
            _showVersionAndExit = args.Any(i => i.ArgumentType == CommandLineArgumentType.Version);
            _nuGetSources = args.Where(i => i.ArgumentType == CommandLineArgumentType.NuGetSource).Select(i => i.Value);
            if (runningMode == RunningMode.Application
                || args.Any(i => i.ArgumentType == CommandLineArgumentType.ScriptFile)
                || args.Any(i => i.ArgumentType == CommandLineArgumentType.Help))
            {
                _interactionMode = InteractionMode.NonInteractive;
                _verbosityLevel = VerbosityLevel.Normal;
                _scriptArguments = [..args.Where(i => i.ArgumentType == CommandLineArgumentType.ScriptArgument).Select(i => i.Value)];
                _codeSources = args.Where(i => i.ArgumentType == CommandLineArgumentType.ScriptFile).Select(i => fileCodeSourceFactory(i.Value));
            }
            else
            {
                _interactionMode = InteractionMode.Interactive;
                _verbosityLevel = VerbosityLevel.Quiet;
                _codeSources = [consoleCodeSource];
            }
        }
    }

    VerbosityLevel ISettingSetter<VerbosityLevel>.SetSetting(VerbosityLevel value)
    {
        var prevVerbosityLevel = VerbosityLevel;
        _verbosityLevel = value;
        return prevVerbosityLevel;
    }
}