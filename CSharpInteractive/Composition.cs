// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantCast
// ReSharper disable UnusedMember.Local
// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedParameterInPartialMethod
namespace CSharpInteractive;

using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using Core;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Read;
using JetBrains.TeamCity.ServiceMessages.Write;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using JetBrains.TeamCity.ServiceMessages.Write.Special.Impl.Updater;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Pure.DI;
using static DateTime;
using static Pure.DI.Lifetime;
using static Pure.DI.Name;
using static Pure.DI.Tag;
using CommandLineParser = Core.CommandLineParser;
using Debugger = Core.Debugger;

internal partial class Composition
{
    public static readonly Composition Shared = new();

#if DEBUG
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    private partial T OnDependencyInjection<T>(in T value, object? tag, Lifetime lifetime)
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        if (Equals(value, null) && !System.Diagnostics.Debugger.IsAttached)
        {
            System.Diagnostics.Debugger.Launch();
        }

        return value;
    }
#endif

    private static void Setup()
    {
        DI.Setup()
            .Hint(Hint.Resolve, Off)
#if DEBUG
            .Hint(Hint.FormatCode, "On")
            .Hint(Hint.OnDependencyInjection, "On")
#endif
            .Root<Root>(nameof(Root))

#if TOOL
            // Transient
            .Transient(() => RunningMode.Tool)
            .Transient(() => Path.GetDirectoryName(typeof(object).Assembly.Location) ?? string.Empty, "RuntimePathTag")
            .Transient(ctx =>
            {
                ctx.Inject<ISettings>(out var settings);
                if (settings.InteractionMode == InteractionMode.Interactive)
                {
                    ctx.Inject<IScriptRunner>(InteractiveTag, out var scriptRunner);
                    return scriptRunner;
                }
                else
                {
                    ctx.Inject<IScriptRunner>(NonInteractiveTag, out var scriptRunner);
                    return scriptRunner;
                }
            })
            // Default settings
            .Transient(() => OptimizationLevel.Release)
            .Transient(() => (WarningLevel)ScriptOptions.Default.WarningLevel)
            .Transient(() => ScriptOptions.Default.CheckOverflow ? CheckOverflow.On : CheckOverflow.Off)
            .Transient(() => ScriptOptions.Default.AllowUnsafe ? AllowUnsafe.On : AllowUnsafe.Off)

            .Transient<LineCodeSource>("LineCodeTag")
            .Transient<ConfigurableScriptOptionsFactory, HelpCommandFactory, HelpCommandRunner>(Unique)
            .Transient<ScriptCommandFactory, SourceResolver, MetadataResolver, AssembliesProvider, ScriptSubmissionAnalyzer>()

            .Bind(Unique).Bind<IReferenceRegistry>().As(Singleton).To<ReferencesScriptOptionsFactory>()
            .Singleton<InteractiveRunner>(InteractiveTag)
            .Singleton<ScriptRunner>(NonInteractiveTag)
            .Singleton<ExitManager, Debugger, CSharpScriptCommandRunner, SettingCommandFactory<VerbosityLevel>
                , SettingCommandRunner<VerbosityLevel>, AddNuGetReferenceCommandFactory, AddNuGetReferenceCommandRunner
                , AssembliesScriptOptionsProvider, SourceFileScriptOptionsFactory, MetadataResolverOptionsFactory, ImportsOptionsFactory>(Unique)
            .Singleton<SettingCommandFactory<LanguageVersion>, SettingCommandRunner<LanguageVersion>, SettingCommandFactory<OptimizationLevel>
                , SettingCommandRunner<OptimizationLevel>, SettingCommandFactory<WarningLevel>, SettingCommandRunner<WarningLevel>
                , SettingCommandFactory<CheckOverflow>, SettingCommandRunner<CheckOverflow>, SettingCommandFactory<AllowUnsafe>
                , SettingCommandRunner<AllowUnsafe>, SettingCommandFactory<NuGetRestoreSetting>, SettingCommandRunner<NuGetRestoreSetting>>(Unique)
            .Singleton<CommandSource, Setting<TTE>, CommandsRunner, CodeSourceCommandFactory, CSharpScriptRunner, NuGetReferenceResolver, ScriptContentReplacer>()
            .PerBlock<TextReplacer, RuntimeExplorer, ExitCodeParser, StringService, DiagnosticsPresenter, ScriptStatePresenter>()
#endif
#if APPLICATION
            .Transient(_ => RunningMode.Application)
#endif
            .Transient(() => Now)
            .Transient(() => typeof(Composition).Assembly)
            .Transient(() => new CSharpParseOptions().LanguageVersion)
            .Transient((CancellationTokenSource cancellationTokenSource) => cancellationTokenSource.Token)
            .Transient(() => MemoryPool<TT>.Shared)
            .Transient(Process.GetCurrentProcess)
            .Transient((Process process) => process.MainModule?.FileName ?? string.Empty, ModuleFileTag)
            .Transient((Assembly assembly) => assembly.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>()?.FrameworkName ?? string.Empty, TargetFrameworkMonikerTag)
            .Transient<BuildContext>(BaseTag)
            .Transient<LoadFileCodeSource>(LoadFileCodeTag)
            .Transient<FilePathResolver, StartInfoFactory, ReliableBuildContext, ProcessMonitor, ProcessManager>()
            .Bind<IDotNetEnvironment>().Bind<ITraceSource>(Unique).As(PerBlock).To<DotNetEnvironment>()
            .Bind<IDockerEnvironment>().Bind<ITraceSource>(Unique).As(PerBlock).To<DockerEnvironment>()
            .Bind<INuGetEnvironment>().Bind<ITraceSource>(Unique).As(PerBlock).To<NuGetEnvironment>()
            .PerBlock<VerbosityLevelSettingDescription>(typeof(VerbosityLevel))
            .PerBlock<CustomMessagesProcessor>(CustomTag)
            .PerBlock<DefaultBuildMessagesProcessor, ProcessRunner>(BaseTag)
            .PerBlock<LanguageVersionSettingDescription, OptimizationLevelSettingDescription, WarningLevelSettingDescription
                , CheckOverflowSettingDescription, AllowUnsafeSettingDescription, NuGetRestoreSettingDescription>(Unique)
            .PerBlock<StartInfoDescription, HostEnvironment, ColorTheme, TeamCityLineFormatter, FileSystem, MSBuildArgumentsTool
                , CommandLineParser, TracePresenter, StatisticsPresenter, NuGetRestoreService, NuGetLogger
                , UniqueNameGenerator, NuGetAssetsReader, Cleaner, TextToColorStrings, FileExplorer, Utf8Encoding
                , BuildOutputProcessor, TeamCityContext, SummaryPresenter, ProcessResultHandler>()
            .Singleton(() => new CancellationTokenSource())
            .Singleton((ICISpecific<IStdOut> stdOut) => stdOut.Instance)
            .Singleton((ICISpecific<IStdErr> stdErr) => stdErr.Instance)
            .Singleton((ICISpecific<ILog<TT>> log) => log.Instance)
            .Singleton((ICISpecific<IProperties> properties) => properties.Instance)
            .Bind().Bind<ITraceSource>(Unique).As(Singleton).To<Environment>()
            .Bind<IEnvironmentVariables>().Bind<ITraceSource>(Unique).As(Singleton).To<EnvironmentVariables>()
            .Singleton<ConsoleInOut>(BaseTag)
            .Singleton<AnsiInOut>(AnsiTag)
            .Singleton<Log<TT>, Properties>(BaseTag, AnsiTag)
            .Singleton<TeamCityLog<TT>, TeamCityProperties, TeamCityInOut>(TeamCityTag)
            .Singleton<Root, Console, CISettings, ExitTracker, CISpecific<TT>, Settings, Info, ConsoleSource
                , Statistics, TargetFrameworkMonikerParser, DockerSettings, ProcessOutputWriter, BuildMessageLogWriter
                , MessageIndicesReader, MessagesReader, PathResolverContext, ProcessInFlowRunner>()
            // Public services
            .Singleton((ITeamCityServiceMessages teamCityServiceMessages, IConsole console)
                => teamCityServiceMessages.CreateWriter(str => console.WriteToOut((null, str + "\n"))), BaseTag)
            .Singleton<HostService, NuGetService, CommandLineRunner, BuildRunner, TeamCityServiceMessages, ServiceMessageFormatter
                , FlowIdGenerator, TimestampUpdater, SafeTeamCityWriter, ServiceMessageParser>();
    }
}