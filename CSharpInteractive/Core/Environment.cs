// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ConvertIfStatementToReturnStatement
// ReSharper disable InconsistentNaming

namespace CSharpInteractive.Core;

using System.Runtime.InteropServices;

internal class Environment :
    IEnvironment,
    ITestEnvironment,
    ITraceSource,
    IScriptContext,
    IErrorContext
{
    private static readonly OSPlatform UnknownOSPlatform = OSPlatform.Create("Unknown");
    private readonly LinkedList<ICodeSource> _sources = [];

    public bool IsTesting { get; set; }

    public int? ExitCode { get; set; }

    public OSPlatform OperatingSystemPlatform
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
            {
                return OSPlatform.FreeBSD;
            }

            return UnknownOSPlatform;
        }
    }

    public Architecture ProcessArchitecture => RuntimeInformation.ProcessArchitecture;

    public IEnumerable<string> GetCommandLineArgs() => System.Environment.GetCommandLineArgs();

    public string GetPath(SpecialFolder specialFolder)
    {
        if (OperatingSystemPlatform == OSPlatform.Windows)
        {
            return specialFolder switch
            {
                SpecialFolder.Bin => GetBinDirectory(),
                SpecialFolder.Temp => Path.GetFullPath(System.Environment.GetEnvironmentVariable("TMP") ?? Path.GetTempPath()),
                SpecialFolder.ProgramFiles => System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles),
                SpecialFolder.Script => GetScriptDirectory(),
                SpecialFolder.Working => GetWorkingDirectory(),
                _ => throw new ArgumentOutOfRangeException(nameof(specialFolder), specialFolder, null)
            };
        }

        return specialFolder switch
        {
            SpecialFolder.Bin => GetBinDirectory(),
            SpecialFolder.Temp => Path.GetFullPath(System.Environment.GetEnvironmentVariable("TMP") ?? Path.GetTempPath()),
            SpecialFolder.ProgramFiles => "usr/local/share",
            SpecialFolder.Script => GetScriptDirectory(),
            SpecialFolder.Working => GetWorkingDirectory(),
            _ => throw new ArgumentOutOfRangeException(nameof(specialFolder), specialFolder, null)
        };
    }

    public void Exit(int exitCode)
    {
        ExitCode = exitCode;
        if (IsTesting)
        {
            return;
        }

        System.Environment.Exit(exitCode);
    }

    public IEnumerable<Text> Trace
    {
        get
        {
            yield return new Text($"OperatingSystemPlatform: {OperatingSystemPlatform}");
            yield return Text.NewLine;
            yield return new Text($"ProcessArchitecture: {ProcessArchitecture}");
            yield return Text.NewLine;
            foreach (var specialFolder in Enum.GetValues(typeof(SpecialFolder)).OfType<SpecialFolder>())
            {
                yield return new Text($"Path({specialFolder}): {GetPath(specialFolder)}");
                yield return Text.NewLine;
            }

            yield return new Text("Command line arguments:");
            yield return Text.NewLine;
            foreach (var arg in System.Environment.GetCommandLineArgs())
            {
                yield return Text.Tab;
                yield return new Text(arg);
                yield return Text.NewLine;
            }
        }
    }

    public IDisposable CreateScope(ICodeSource source)
    {
        _sources.AddLast(source);
        return Disposable.Create(() => _sources.Remove(source));
    }

    public bool TryGetSourceName([NotNullWhen(true)] out string? name)
    {
        if (TryGetCurrentSource(out var source))
        {
            name = Path.GetFileName(source.Name);
            return !string.IsNullOrWhiteSpace(name);
        }

        name = default;
        return false;
    }

    private bool TryGetCurrentSource([NotNullWhen(true)] out ICodeSource? source)
    {
        source = _sources.LastOrDefault();
        return source != default;
    }

    private static string GetWorkingDirectory() => Directory.GetCurrentDirectory();

    private string GetBinDirectory() => Path.GetDirectoryName(typeof(Environment).Assembly.Location) ?? GetScriptDirectory();

    private string GetScriptDirectory()
    {
        var script = string.Empty;
        if (TryGetCurrentSource(out var source))
        {
            script = source.Name;
        }

        if (string.IsNullOrWhiteSpace(script))
        {
            return GetWorkingDirectory();
        }

        var scriptDirectory = Path.GetDirectoryName(script);
        return !string.IsNullOrWhiteSpace(scriptDirectory) ? scriptDirectory : script;
    }
}