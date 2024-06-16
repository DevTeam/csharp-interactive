// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantCast
// ReSharper disable UnusedMember.Local
namespace CSharpInteractive;

using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;
using HostApi;
using JetBrains.TeamCity.ServiceMessages.Read;
using JetBrains.TeamCity.ServiceMessages.Write;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using JetBrains.TeamCity.ServiceMessages.Write.Special.Impl.Updater;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Pure.DI;

internal partial class Composition
{
    public static readonly Composition Shared = new();
    
    private void Setup()
    {
        DI.Setup(nameof(Composition))
            .Hint(Hint.Resolve, "Off")
            .Root<Root>("Root")
            
            .DefaultLifetime(Lifetime.Singleton)
#if TOOL
            .Bind().To(_ => RunningMode.Tool)
#endif
#if APPLICATION
            .Bind().To(_ => RunningMode.Application)
#endif
            .Bind().To<Root>()
            .Bind().To(_ => typeof(Composition).Assembly)
            .Bind().To(_ => new CSharpParseOptions().LanguageVersion)
            .Bind("RuntimePath").To(_ => Path.GetDirectoryName(typeof(object).Assembly.Location) ?? string.Empty)
            .Bind("TargetFrameworkMoniker").To(ctx =>
            {
                ctx.Inject<Assembly>(out var assembly);
                return assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName ?? string.Empty;
            })
            .Bind().To(_ => Process.GetCurrentProcess())
            .Bind("ModuleFile").To(ctx =>
            {
                ctx.Inject<Process>(out var process);
                return process.MainModule?.FileName ?? string.Empty;
            })
            .Bind().To(_ => new CancellationTokenSource())
            .Bind().As(Lifetime.Transient).To(ctx =>
            {
                ctx.Inject<CancellationTokenSource>(out var cancellationTokenSource);
                return cancellationTokenSource.Token;
            })
            .Bind(Tag.Type).To<ExitManager>()
            .Bind().To<HostEnvironment>()
            .Bind().To<ColorTheme>()
            .Bind().To<TeamCityLineFormatter>()
            .Bind().To<CISpecific<TT>>()
            .Bind("Default").To<ConsoleInOut>()
            .Bind("TeamCity").To<TeamCityInOut>()
            .Bind("Ansi").To<AnsiInOut>()
            .Bind().To<Console>()
            .Bind().To(ctx =>
            {
                ctx.Inject<ICISpecific<IStdOut>>(out var stdOut);
                return stdOut.Instance;
            })
            .Bind().To(ctx =>
            {
                ctx.Inject<ICISpecific<IStdErr>>(out var stdErr);
                return stdErr.Instance;
            })
            .Bind("Default", "Ansi").To<Log<TT>>()
            .Bind("TeamCity").To<TeamCityLog<TT>>()
            .Bind().To(ctx =>
            {
                ctx.Inject<ICISpecific<ILog<TT>>>(out var log);
                return log.Instance;
            })
            .Bind().To<FileSystem>()
            .Bind().To<CISettings>()
            .Bind().To<ExitTracker>()
            .Bind<IEnvironment, IScriptContext, IErrorContext>().Bind<ITraceSource>(Tag.Type).To<Environment>()
            .Bind<IDotNetEnvironment>().Bind<ITraceSource>(Tag.Type).To<DotNetEnvironment>()
            .Bind<IDockerEnvironment>().Bind<ITraceSource>(Tag.Type).To<DockerEnvironment>()
            .Bind<INuGetEnvironment>().Bind<ITraceSource>(Tag.Type).To<NuGetEnvironment>()
            .Bind<IEnvironmentVariables>().Bind<ITraceSource>(Tag.Type).To<EnvironmentVariables>()
            .Bind().To<Settings>()
            .Bind(typeof(VerbosityLevel)).To<VerbosityLevelSettingDescription>()
            .Bind().To<MSBuildArgumentsTool>()
            .Bind().To<CommandLineParser>()
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
            .Bind(InteractionMode.Interactive).To<InteractiveRunner>()
            .Bind(InteractionMode.NonInteractive).To<ScriptRunner>()
            .Bind().As(Lifetime.Transient).To(ctx =>
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
            .Bind().To<CommandSource>()
            .Bind().To<StringService>()
            .Bind().To<Statistics>()
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
            .Bind().To<CommandsRunner>()
            .Bind().To<CodeSourceCommandFactory>()
            .Bind().As(Lifetime.Transient).To<ScriptCommandFactory>()
            .Bind().To<CSharpScriptRunner>()
            .Bind().To<TargetFrameworkMonikerParser>()
            .Bind(Tag.Type).To<Debugger>()
            .Bind().To<DockerSettings>()
            .Bind("base").As(Lifetime.Transient).To<BuildContext>()
            .Bind().As(Lifetime.Transient).To<ReliableBuildContext>()
            .Bind().To<TextToColorStrings>()
            .Bind().To<FileExplorer>()
            .Bind().To<ProcessOutputWriter>()
            .Bind().To<BuildMessageLogWriter>()
            .Bind().To(_ => MemoryPool<TT>.Shared)
            .Bind().To<MessageIndicesReader>()
            .Bind().To<MessagesReader>()
            .Bind().To<PathResolverContext>()
            .Bind().To<Utf8Encoding>()
            .Bind().As(Lifetime.Transient).To<ProcessMonitor>()
            .Bind().To<BuildOutputProcessor>()
            .Bind("default").To<DefaultBuildMessagesProcessor>()
            .Bind("custom").To<CustomMessagesProcessor>()
            .Bind().To<SummaryPresenter>()
            .Bind().To<ExitCodeParser>()
            .Bind("base").To<ProcessRunner>()
            .Bind().To<ProcessInFlowRunner>()
            .Bind().To<TeamCityContext>()
            .Bind().To<ProcessResultHandler>()
            .Bind().To<RuntimeExplorer>()
            .Bind().To<NuGetReferenceResolver>()
            .Bind().As(Lifetime.Transient).To<SourceResolver>()
            .Bind().As(Lifetime.Transient).To<MetadataResolver>()
            .Bind().To<ScriptContentReplacer>()
            .Bind().To<TextReplacer>();

        DI.Setup(nameof(Composition))
            .DefaultLifetime(Lifetime.Singleton)
            // Script options factory
            .Bind().To(_ => new Setting<LanguageVersion>(LanguageVersion.Default))
            .Bind().To(_ => new Setting<OptimizationLevel>(OptimizationLevel.Release))
            .Bind().To(_ => new Setting<WarningLevel>((WarningLevel)ScriptOptions.Default.WarningLevel))
            .Bind().To(_ => new Setting<CheckOverflow>(ScriptOptions.Default.CheckOverflow ? CheckOverflow.On : CheckOverflow.Off))
            .Bind().To(_ => new Setting<AllowUnsafe>(ScriptOptions.Default.AllowUnsafe ? AllowUnsafe.On : AllowUnsafe.Off))
            .Bind().To<AssembliesProvider>()
            .Bind(Tag.Type).To<AssembliesScriptOptionsProvider>()
            .Bind(Tag.Type).To<ConfigurableScriptOptionsFactory>()
            .Bind(Tag.Type).Bind<IReferenceRegistry>().To<ReferencesScriptOptionsFactory>()
            .Bind(Tag.Type).To<SourceFileScriptOptionsFactory>()
            .Bind(Tag.Type).To<MetadataResolverOptionsFactory>()
            .Bind(Tag.Type).To<ImportsOptionsFactory>()
            .Bind(Tag.Unique).To<SettingCommandFactory<LanguageVersion>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<LanguageVersion>>()
            .Bind(typeof(LanguageVersion)).To<LanguageVersionSettingDescription>()
            .Bind(Tag.Unique).To<SettingCommandFactory<OptimizationLevel>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<OptimizationLevel>>()
            .Bind(typeof(OptimizationLevel)).To<OptimizationLevelSettingDescription>()
            .Bind(Tag.Unique).To<SettingCommandFactory<WarningLevel>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<WarningLevel>>()
            .Bind(typeof(WarningLevel)).To<WarningLevelSettingDescription>()
            .Bind(Tag.Unique).To<SettingCommandFactory<CheckOverflow>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<CheckOverflow>>()
            .Bind(typeof(CheckOverflow)).To<CheckOverflowSettingDescription>()
            .Bind(Tag.Unique).To<SettingCommandFactory<AllowUnsafe>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<AllowUnsafe>>()
            .Bind(typeof(AllowUnsafe)).To<AllowUnsafeSettingDescription>()
            .Bind(Tag.Unique).To<SettingCommandFactory<NuGetRestoreSetting>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<NuGetRestoreSetting>>()
            .Bind(typeof(NuGetRestoreSetting)).To<NuGetRestoreSettingDescription>()
            .Bind().To<ScriptSubmissionAnalyzer>()
            .Bind(Tag.Unique).To<CSharpScriptCommandRunner>()
            .Bind(Tag.Unique).To<HelpCommandFactory>()
            .Bind(Tag.Unique).To<HelpCommandRunner>()
            .Bind(Tag.Unique).To<SettingCommandFactory<VerbosityLevel>>()
            .Bind(Tag.Unique).To<SettingCommandRunner<VerbosityLevel>>()
            .Bind(Tag.Unique).To<AddNuGetReferenceCommandFactory>()
            .Bind().To<FilePathResolver>()
            .Bind(Tag.Unique).To<AddNuGetReferenceCommandRunner>()
            .Bind().To<StartInfoFactory>()
            .Bind().As(Lifetime.Transient).To<ProcessManager>()
            .Bind("Default", "Ansi").To<Properties>()
            .Bind("TeamCity").To<TeamCityProperties>()

            // Public
            .Bind().To<HostService>()
            .Bind().To(ctx =>
            {
                ctx.Inject<ICISpecific<IProperties>>(out var properties);
                return properties.Instance;
            })
            .Bind().To<NuGetService>()
            .Bind().To<CommandLineRunner>()
            .Bind().To<BuildRunner>()

            // TeamCity Service messages
            .Bind().To<TeamCityServiceMessages>()
            .Bind().To<ServiceMessageFormatter>()
            .Bind().To<FlowIdGenerator>()
            .Bind().As(Lifetime.Transient).To(_ => DateTime.Now)
            .Bind().To<TimestampUpdater>()
            .Bind().To(
                ctx =>
                {
                    ctx.Inject<ITeamCityServiceMessages>(out var teamCityServiceMessages);
                    return teamCityServiceMessages.CreateWriter(
                        str =>
                        {
                            ctx.Inject<IConsole>(out var console);
                            console.WriteToOut((default, str + "\n"));
                        });
                })
            .Bind().To<ServiceMessageParser>();
    }
}