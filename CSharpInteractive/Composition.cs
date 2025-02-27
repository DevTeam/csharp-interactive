// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantCast
// ReSharper disable UnusedMember.Local
// ReSharper disable RedundantUsingDirective
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
using static Pure.DI.Lifetime;
using static Pure.DI.Tag;
using CommandLineParser = Core.CommandLineParser;
using Debugger = Core.Debugger;

internal partial class Composition
{
    public static readonly Composition Shared = new();
    
    private static void Setup()
    {
        DI.Setup()
            .Hint(Hint.Resolve, "Off")
            .Root<Root>("Root")

#if TOOL
            .DefaultLifetime(Transient)
                .Bind().To(_ => RunningMode.Tool)
                .Bind().To(ctx =>
                {
                    ctx.Inject<ISettings>(out var settings);
                    if (settings.InteractionMode == InteractionMode.Interactive)
                    {
                        ctx.Inject<IScriptRunner>(InteractionMode.Interactive, out var scriptRunner);
                        return scriptRunner;
                    }
                    else
                    {
                        ctx.Inject<IScriptRunner>(InteractionMode.NonInteractive, out var scriptRunner);
                        return scriptRunner;
                    }
                })
                // Default settings
                .Bind().To(_ => OptimizationLevel.Release)
                .Bind().To(_ => (WarningLevel)ScriptOptions.Default.WarningLevel)
                .Bind().To(_ => ScriptOptions.Default.CheckOverflow ? CheckOverflow.On : CheckOverflow.Off)
                .Bind().To(_ => ScriptOptions.Default.AllowUnsafe ? AllowUnsafe.On : AllowUnsafe.Off)
            
            .DefaultLifetime(Singleton)
                .Bind(Unique).To<ExitManager>()
                .Bind(Unique).To<Debugger>()
                .Bind(InteractionMode.Interactive).To<InteractiveRunner>()
                .Bind(InteractionMode.NonInteractive).To<ScriptRunner>()
                .Bind().To<CommandSource>()
                .Bind().To<Setting<TTE>>()
                .Bind(Unique).Bind<IReferenceRegistry>().To<ReferencesScriptOptionsFactory>()
#endif
#if APPLICATION
            .Bind().As(Transient).To(_ => RunningMode.Application)
#endif
            .DefaultLifetime(Transient)
                .Bind().To(_ => DateTime.Now)
                .Bind().To(_ => typeof(Composition).Assembly)
                .Bind().To(_ => new CSharpParseOptions().LanguageVersion)
                .Bind(RuntimePath).To(_ => Path.GetDirectoryName(typeof(object).Assembly.Location) ?? string.Empty)
                .Bind().To((CancellationTokenSource cancellationTokenSource) => cancellationTokenSource.Token)
                .Bind(TargetFrameworkMoniker).To((Assembly assembly) => assembly.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>()?.FrameworkName ?? string.Empty)
                .Bind().To(_ => Process.GetCurrentProcess())
                .Bind(ModuleFile).To((Process process) => process.MainModule?.FileName ?? string.Empty)
                .Bind().To<ScriptCommandFactory>()
                .Bind().To<ReliableBuildContext>()
                .Bind().To<ProcessMonitor>()
                .Bind().To<ProcessManager>()
                .Bind(Base).To<BuildContext>()
                .Bind().To(_ => MemoryPool<TT>.Shared)
                .Bind().To<SourceResolver>()
                .Bind().To<MetadataResolver>()
                .Bind().To<AssembliesProvider>()
                .Bind(Unique).To<ConfigurableScriptOptionsFactory>()
                .Bind().To<ScriptSubmissionAnalyzer>()
                .Bind(Unique).To<HelpCommandFactory>()
                .Bind(Unique).To<HelpCommandRunner>()
                .Bind().To<FilePathResolver>()
                .Bind().To<StartInfoFactory>()
            
            .DefaultLifetime(PerBlock)
                .Bind().To<StartInfoDescription>()
                .Bind().To<HostEnvironment>()
                .Bind().To<ColorTheme>()
                .Bind().To<TeamCityLineFormatter>()
                .Bind().To<FileSystem>()
                .Bind<IDotNetEnvironment>().Bind<ITraceSource>(Unique).To<DotNetEnvironment>()
                .Bind<IDockerEnvironment>().Bind<ITraceSource>(Unique).To<DockerEnvironment>()
                .Bind<INuGetEnvironment>().Bind<ITraceSource>(Unique).To<NuGetEnvironment>()
                .Bind(typeof(VerbosityLevel)).To<VerbosityLevelSettingDescription>()
                .Bind().To<MSBuildArgumentsTool>()
                .Bind().To<CommandLineParser>()
                .Bind().To<StringService>()
                .Bind().To<TracePresenter>()
                .Bind().To<StatisticsPresenter>()
                .Bind().To<DiagnosticsPresenter>()
                .Bind().To<ScriptStatePresenter>()
                .Bind().To<BuildEngine>()
                .Bind().To<NuGetRestoreService>()
                .Bind().To<NuGetLogger>()
                .Bind().To<UniqueNameGenerator>()
                .Bind().To<NuGetAssetsReader>()
                .Bind().To<Cleaner>()
                .Bind().To<TextToColorStrings>()
                .Bind().To<FileExplorer>()
                .Bind().To<Utf8Encoding>()
                .Bind().To<BuildOutputProcessor>()
                .Bind(Base).To<DefaultBuildMessagesProcessor>()
                .Bind(Custom).To<CustomMessagesProcessor>()
                .Bind().To<TeamCityContext>()
                .Bind().To<SummaryPresenter>()
                .Bind().To<ExitCodeParser>()
                .Bind(Base).To<ProcessRunner>()
                .Bind().To<ProcessResultHandler>()
                .Bind().To<TextReplacer>()
                .Bind().To<RuntimeExplorer>()

            .DefaultLifetime(Singleton)
                .Bind().To<Root>()
                .Bind().To(_ => new CancellationTokenSource())
                .Bind().To<CISpecific<TT>>()
                .Bind(Base).To<ConsoleInOut>()
                .Bind(TeamCity).To<TeamCityInOut>()
                .Bind(Ansi).To<AnsiInOut>()
                .Bind().To<Console>()
                .Bind().To((ICISpecific<IStdOut> stdOut) => stdOut.Instance)
                .Bind().To((ICISpecific<IStdErr> stdErr) => stdErr.Instance)
                .Bind(Base, Ansi).To<Log<TT>>()
                .Bind(TeamCity).To<TeamCityLog<TT>>()
                .Bind().To((ICISpecific<ILog<TT>> log) => log.Instance)
                .Bind().To<CISettings>()
                .Bind().To<ExitTracker>()
                .Bind().Bind<ITraceSource>(Unique).To<Environment>()
                .Bind<IEnvironmentVariables>().Bind<ITraceSource>(Unique).To<EnvironmentVariables>()
                .Bind().To<Settings>()
                .Bind().To<Info>()
                .Bind().To<ConsoleSource>()
                .Bind(LoadFileCode).To(ctx => new Func<string, ICodeSource>(name =>
                {
                    ctx.Inject<LoadFileCodeSource>(out var loadFileCodeSource);
                    loadFileCodeSource.Name = name;
                    return loadFileCodeSource;
                }))
                .Bind(LineCode).To(ctx => new Func<string, ICodeSource>(line =>
                {
                    ctx.Inject<LineCodeSource>(out var lineCodeSource);
                    lineCodeSource.Line = line;
                    return lineCodeSource;
                }))
                .Bind().To<Statistics>()
                .Bind().To<CommandsRunner>()
                .Bind().To<CodeSourceCommandFactory>()
                .Bind().To<CSharpScriptRunner>()
                .Bind().To<TargetFrameworkMonikerParser>()
                .Bind().To<DockerSettings>()
                .Bind().To<ProcessOutputWriter>()
                .Bind().To<BuildMessageLogWriter>()
                .Bind().To<MessageIndicesReader>()
                .Bind().To<MessagesReader>()
                .Bind().To<PathResolverContext>()
                .Bind().To<ProcessInFlowRunner>()
                .Bind().To<NuGetReferenceResolver>()
                .Bind().To<ScriptContentReplacer>()
                .Bind(Unique).To<AssembliesScriptOptionsProvider>()
                .Bind(Unique).To<SourceFileScriptOptionsFactory>()
                .Bind(Unique).To<MetadataResolverOptionsFactory>()
                .Bind(Unique).To<ImportsOptionsFactory>()
                .Bind(Unique).To<SettingCommandFactory<LanguageVersion>>()
                .Bind(Unique).To<SettingCommandRunner<LanguageVersion>>()
                .Bind(Unique).As(PerBlock).To<LanguageVersionSettingDescription>()
                .Bind(Unique).To<SettingCommandFactory<OptimizationLevel>>()
                .Bind(Unique).To<SettingCommandRunner<OptimizationLevel>>()
                .Bind(Unique).As(PerBlock).To<OptimizationLevelSettingDescription>()
                .Bind(Unique).To<SettingCommandFactory<WarningLevel>>()
                .Bind(Unique).To<SettingCommandRunner<WarningLevel>>()
                .Bind(Unique).As(PerBlock).To<WarningLevelSettingDescription>()
                .Bind(Unique).To<SettingCommandFactory<CheckOverflow>>()
                .Bind(Unique).To<SettingCommandRunner<CheckOverflow>>()
                .Bind(Unique).As(PerBlock).To<CheckOverflowSettingDescription>()
                .Bind(Unique).To<SettingCommandFactory<AllowUnsafe>>()
                .Bind(Unique).To<SettingCommandRunner<AllowUnsafe>>()
                .Bind(Unique).As(PerBlock).To<AllowUnsafeSettingDescription>()
                .Bind(Unique).To<SettingCommandFactory<NuGetRestoreSetting>>()
                .Bind(Unique).To<SettingCommandRunner<NuGetRestoreSetting>>()
                .Bind(Unique).As(PerBlock).To<NuGetRestoreSettingDescription>()
                .Bind(Unique).To<CSharpScriptCommandRunner>()
                .Bind(Unique).To<SettingCommandFactory<VerbosityLevel>>()
                .Bind(Unique).To<SettingCommandRunner<VerbosityLevel>>()
                .Bind(Unique).To<AddNuGetReferenceCommandFactory>()
                .Bind(Unique).To<AddNuGetReferenceCommandRunner>()
                .Bind(Base, Ansi).To<Properties>()
                .Bind(TeamCity).To<TeamCityProperties>()
                .Bind().To((ICISpecific<IProperties> properties) => properties.Instance)

                // Public services
                .Bind().To<HostService>()
                .Bind().To<NuGetService>()
                .Bind().To<CommandLineRunner>()
                .Bind().To<BuildRunner>()
                .Bind().To<TeamCityServiceMessages>()
                .Bind().To<ServiceMessageFormatter>()
                .Bind().To<FlowIdGenerator>()
                .Bind().To<TimestampUpdater>()
                .Bind().To((ITeamCityServiceMessages teamCityServiceMessages, IConsole console)
                    => new SafeTeamCityWriter(teamCityServiceMessages.CreateWriter(str => console.WriteToOut((null, str + "\n")))))
                .Bind().To<ServiceMessageParser>();
    }
}