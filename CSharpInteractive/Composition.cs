// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantCast
// ReSharper disable UnusedMember.Local
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
            .DefaultLifetime(Lifetime.Transient)
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
            
            .DefaultLifetime(Lifetime.Singleton)
                .Bind(Tag.Type).To<ExitManager>()
                .Bind(Tag.Type).To<Debugger>()
                .Bind(InteractionMode.Interactive).To<InteractiveRunner>()
                .Bind(InteractionMode.NonInteractive).To<ScriptRunner>()
                .Bind().To<CommandSource>()
                .Bind().To<Setting<TTE>>()
#endif
#if APPLICATION
            .Bind().As(Lifetime.Transient).To(_ => RunningMode.Application)
#endif
            .DefaultLifetime(Lifetime.Transient)
                .Bind().To(_ => DateTime.Now)
                .Bind().To(_ => typeof(Composition).Assembly)
                .Bind().To(_ => new CSharpParseOptions().LanguageVersion)
                .Bind("RuntimePath").To(_ => Path.GetDirectoryName(typeof(object).Assembly.Location) ?? string.Empty)
                .Bind().To((CancellationTokenSource cancellationTokenSource) => cancellationTokenSource.Token)
                .Bind("TargetFrameworkMoniker").To((Assembly assembly) => assembly.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>()?.FrameworkName ?? string.Empty)
                .Bind().To(_ => Process.GetCurrentProcess())
                .Bind("ModuleFile").To((Process process) => process.MainModule?.FileName ?? string.Empty)
                .Bind().To<ScriptCommandFactory>()
                .Bind().To<ReliableBuildContext>()
                .Bind().To<ProcessMonitor>()
                .Bind().To<ProcessManager>()
                .Bind("base").To<BuildContext>()
                .Bind().To(_ => MemoryPool<TT>.Shared)
                .Bind().To<SourceResolver>()
                .Bind().To<MetadataResolver>()
                .Bind().To<AssembliesProvider>()
                .Bind(Tag.Type).To<ConfigurableScriptOptionsFactory>()
                .Bind().To<ScriptSubmissionAnalyzer>()
                .Bind(Tag.Unique).To<HelpCommandFactory>()
                .Bind(Tag.Unique).To<HelpCommandRunner>()
                .Bind().To<FilePathResolver>()
                .Bind().To<StartInfoFactory>()
            
            .DefaultLifetime(Lifetime.PerBlock)
                .Bind().To<StartInfoDescription>()
                .Bind().To<HostEnvironment>()
                .Bind().To<ColorTheme>()
                .Bind().To<TeamCityLineFormatter>()
                .Bind().To<FileSystem>()
                .Bind<IDotNetEnvironment>().Bind<ITraceSource>(Tag.Type).To<DotNetEnvironment>()
                .Bind<IDockerEnvironment>().Bind<ITraceSource>(Tag.Type).To<DockerEnvironment>()
                .Bind<INuGetEnvironment>().Bind<ITraceSource>(Tag.Type).To<NuGetEnvironment>()
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
                .Bind("default").To<DefaultBuildMessagesProcessor>()
                .Bind("custom").To<CustomMessagesProcessor>()
                .Bind().To<TeamCityContext>()
                .Bind().To<SummaryPresenter>()
                .Bind().To<ExitCodeParser>()
                .Bind("base").To<ProcessRunner>()
                .Bind().To<ProcessResultHandler>()
                .Bind().To<TextReplacer>()
                .Bind().To<RuntimeExplorer>()

            .DefaultLifetime(Lifetime.Singleton)
                .Bind().To<Root>()
                .Bind().To(_ => new CancellationTokenSource())
                .Bind().To<CISpecific<TT>>()
                .Bind("Default").To<ConsoleInOut>()
                .Bind("TeamCity").To<TeamCityInOut>()
                .Bind("Ansi").To<AnsiInOut>()
                .Bind().To<Console>()
                .Bind().To((ICISpecific<IStdOut> stdOut) => stdOut.Instance)
                .Bind().To((ICISpecific<IStdErr> stdErr) => stdErr.Instance)
                .Bind("Default", "Ansi").To<Log<TT>>()
                .Bind("TeamCity").To<TeamCityLog<TT>>()
                .Bind().To((ICISpecific<ILog<TT>> log) => log.Instance)
                .Bind().To<CISettings>()
                .Bind().To<ExitTracker>()
                .Bind().Bind<ITraceSource>(Tag.Type).To<Environment>()
                .Bind<IEnvironmentVariables>().Bind<ITraceSource>(Tag.Type).To<EnvironmentVariables>()
                .Bind().To<Settings>()
                .Bind().To<Info>()
                .Bind().To<ConsoleSource>()
                .Bind(typeof(LoadFileCodeSource)).To(ctx => new Func<string, ICodeSource>(name =>
                {
                    ctx.Inject<LoadFileCodeSource>(out var loadFileCodeSource);
                    loadFileCodeSource.Name = name;
                    return loadFileCodeSource;
                }))
                .Bind(typeof(LineCodeSource)).To(ctx => new Func<string, ICodeSource>(line =>
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
                .Bind(Tag.Type).To<AssembliesScriptOptionsProvider>()
                .Bind(Tag.Type).Bind<IReferenceRegistry>().To<ReferencesScriptOptionsFactory>()
                .Bind(Tag.Type).To<SourceFileScriptOptionsFactory>()
                .Bind(Tag.Type).To<MetadataResolverOptionsFactory>()
                .Bind(Tag.Type).To<ImportsOptionsFactory>()
                .Bind(Tag.Unique).To<SettingCommandFactory<LanguageVersion>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<LanguageVersion>>()
                .Bind(typeof(LanguageVersion)).As(Lifetime.PerBlock).To<LanguageVersionSettingDescription>()
                .Bind(Tag.Unique).To<SettingCommandFactory<OptimizationLevel>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<OptimizationLevel>>()
                .Bind(typeof(OptimizationLevel)).As(Lifetime.PerBlock).To<OptimizationLevelSettingDescription>()
                .Bind(Tag.Unique).To<SettingCommandFactory<WarningLevel>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<WarningLevel>>()
                .Bind(typeof(WarningLevel)).As(Lifetime.PerBlock).To<WarningLevelSettingDescription>()
                .Bind(Tag.Unique).To<SettingCommandFactory<CheckOverflow>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<CheckOverflow>>()
                .Bind(typeof(CheckOverflow)).As(Lifetime.PerBlock).To<CheckOverflowSettingDescription>()
                .Bind(Tag.Unique).To<SettingCommandFactory<AllowUnsafe>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<AllowUnsafe>>()
                .Bind(typeof(AllowUnsafe)).As(Lifetime.PerBlock).To<AllowUnsafeSettingDescription>()
                .Bind(Tag.Unique).To<SettingCommandFactory<NuGetRestoreSetting>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<NuGetRestoreSetting>>()
                .Bind(typeof(NuGetRestoreSetting)).As(Lifetime.PerBlock).To<NuGetRestoreSettingDescription>()
                .Bind(Tag.Unique).To<CSharpScriptCommandRunner>()
                .Bind(Tag.Unique).To<SettingCommandFactory<VerbosityLevel>>()
                .Bind(Tag.Unique).To<SettingCommandRunner<VerbosityLevel>>()
                .Bind(Tag.Unique).To<AddNuGetReferenceCommandFactory>()
                .Bind(Tag.Unique).To<AddNuGetReferenceCommandRunner>()
                .Bind("Default", "Ansi").To<Properties>()
                .Bind("TeamCity").To<TeamCityProperties>()
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
                .Bind().To((ITeamCityServiceMessages teamCityServiceMessages, IConsole console) => teamCityServiceMessages.CreateWriter(str => console.WriteToOut((default, str + "\n"))))
                .Bind().To<ServiceMessageParser>();
    }
}