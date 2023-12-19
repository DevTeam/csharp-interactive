// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantCast
// ReSharper disable UnusedMember.Local
namespace CSharpInteractive;

using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;
using HostApi;
using HostApi.Cmd;
using HostApi.Docker;
using HostApi.DotNet;
using JetBrains.TeamCity.ServiceMessages.Read;
using JetBrains.TeamCity.ServiceMessages.Write;
using JetBrains.TeamCity.ServiceMessages.Write.Special;
using JetBrains.TeamCity.ServiceMessages.Write.Special.Impl.Updater;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using Pure.DI;
using Pure.DI.MS;
using ILogger = NuGet.Common.ILogger;

internal partial class Composition: ServiceProviderFactory<Composition>
{
    public static readonly Composition Shared = new();
    
    private void Setup()
    {
        DI.Setup(nameof(Composition))
            .DependsOn(Base)
            .DefaultLifetime(Lifetime.Singleton)
#if TOOL
            .Bind<Program>().To<Program>().Root<Program>("Program")
            .Bind<RunningMode>().To(_ => RunningMode.Tool)
#endif
#if APPLICATION
            .Bind<RunningMode>().To(_ => RunningMode.Application)
#endif
            .Bind<Assembly>().To(_ => typeof(Composition).Assembly)
            .Bind<LanguageVersion>().To(_ => new CSharpParseOptions().LanguageVersion)
            .Bind<string>("RuntimePath").To(_ => Path.GetDirectoryName(typeof(object).Assembly.Location) ?? string.Empty)
            .Bind<string>("TargetFrameworkMoniker").To(ctx =>
            {
                ctx.Inject<Assembly>(out var assembly);
                return assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName ?? string.Empty;
            })
            .Bind<Process>().To(_ => Process.GetCurrentProcess())
            .Bind<string>("ModuleFile").To(ctx =>
            {
                ctx.Inject<Process>(out var process);
                return process.MainModule?.FileName ?? string.Empty;
            })
            .Bind<CancellationTokenSource>().To(_ => new CancellationTokenSource())
            .Bind<CancellationToken>().As(Lifetime.Transient).To(ctx =>
            {
                ctx.Inject<CancellationTokenSource>(out var cancellationTokenSource);
                return cancellationTokenSource.Token;
            })
            .Bind<IActive>(typeof(ExitManager)).To<ExitManager>()
            .Bind<IHostEnvironment>().To<HostEnvironment>()
            .Bind<IColorTheme>().To<ColorTheme>()
            .Bind<ITeamCityLineFormatter>().To<TeamCityLineFormatter>()
            .Bind<ICISpecific<TT>>().To<CISpecific<TT>>()
            .Bind<IStdOut>().Bind<IStdErr>().Tags("Default").To<ConsoleInOut>()
            .Bind<IStdOut>().Bind<IStdErr>().Tags("TeamCity").To<TeamCityInOut>()
            .Bind<IStdOut>().Bind<IStdErr>().Tags("Ansi").To<AnsiInOut>()
            .Bind<IConsole>().To<Console>()
            .Bind<IStdOut>().To(ctx =>
            {
                ctx.Inject<ICISpecific<IStdOut>>(out var stdOut);
                return stdOut.Instance;
            })
            .Bind<IStdErr>().To(ctx =>
            {
                ctx.Inject<ICISpecific<IStdErr>>(out var stdErr);
                return stdErr.Instance;
            })
            .Bind<ILog<TT>>("Default", "Ansi").To<Log<TT>>()
            .Bind<ILog<TT>>("TeamCity").To<TeamCityLog<TT>>()
            .Bind<ILog<TT>>().To(ctx =>
            {
                ctx.Inject<ICISpecific<ILog<TT>>>(out var log);
                return log.Instance;
            })
            .Bind<IFileSystem>().To<FileSystem>()
            .Bind<IEnvironment>().Bind<IScriptContext>().Bind<IErrorContext>().Bind<ITraceSource>(typeof(Environment)).To<Environment>().Root<IEnvironment>()
            .Bind<ICISettings>().To<CISettings>()
            .Bind<IExitTracker>().To<ExitTracker>()
            .Bind<IDotNetEnvironment>().Bind<ITraceSource>(typeof(DotNetEnvironment)).To<DotNetEnvironment>().Root<IDotNetEnvironment>()
            .Bind<IDockerEnvironment>().Bind<ITraceSource>(typeof(DockerEnvironment)).To<DockerEnvironment>()
            .Bind<INuGetEnvironment>().Bind<ITraceSource>(typeof(NuGetEnvironment)).To<NuGetEnvironment>()
            .Bind<ISettings>().Bind<ISettingSetter<VerbosityLevel>>().Bind<Settings>().To<Settings>()
            .Bind<ISettingDescription>().Tags(typeof(VerbosityLevel)).To<VerbosityLevelSettingDescription>()
            .Bind<IMSBuildArgumentsTool>().To<MSBuildArgumentsTool>()
            .Bind<ICommandLineParser>().To<CommandLineParser>()
            .Bind<IInfo>().To<Info>()
            .Bind<ICodeSource>().To<ConsoleSource>()
            .Bind<Func<string, ICodeSource>>(typeof(LoadFileCodeSource)).To(ctx => new Func<string, ICodeSource>(name =>
            {
                ctx.Inject<LoadFileCodeSource>(out var loadFileCodeSource);
                loadFileCodeSource.Name = name;
                return loadFileCodeSource;
            }))
            .Bind<Func<string, ICodeSource>>(typeof(LineCodeSource)).To(ctx => new Func<string, ICodeSource>(line =>
            {
                ctx.Inject<LineCodeSource>(out var lineCodeSource);
                lineCodeSource.Line = line;
                return lineCodeSource;
            }))
            .Bind<IScriptRunner>().Tags(InteractionMode.Interactive).To<InteractiveRunner>()
            .Bind<IScriptRunner>().Tags(InteractionMode.NonInteractive).To<ScriptRunner>()
            .Bind<IScriptRunner>().As(Lifetime.Transient).To(ctx =>
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
            .Bind<ICommandSource>().To<CommandSource>()
            .Bind<IStringService>().To<StringService>()
            .Bind<IStatistics>().To<Statistics>()
            .Bind<IPresenter<IEnumerable<ITraceSource>>>().To<TracePresenter>()
            .Bind<IPresenter<IStatistics>>().To<StatisticsPresenter>()
            .Bind<IPresenter<CompilationDiagnostics>>().To<DiagnosticsPresenter>()
            .Bind<IPresenter<ScriptState<object>>>().To<ScriptStatePresenter>()
            .Bind<IBuildEngine>().To<BuildEngine>()
            .Bind<INuGetRestoreService>().Bind<ISettingSetter<NuGetRestoreSetting>>().To<NuGetRestoreService>()
            .Bind<ILogger>().To<NuGetLogger>()
            .Bind<IUniqueNameGenerator>().To<UniqueNameGenerator>().Root<IUniqueNameGenerator>()
            .Bind<INuGetAssetsReader>().To<NuGetAssetsReader>()
            .Bind<ICleaner>().To<Cleaner>()
            .Bind<ICommandsRunner>().To<CommandsRunner>()
            .Bind<ICommandFactory<ICodeSource>>().To<CodeSourceCommandFactory>()
            .Bind<ICommandFactory<ScriptCommand>>().As(Lifetime.Transient).To<ScriptCommandFactory>()
            .Bind<ICSharpScriptRunner>().To<CSharpScriptRunner>()
            .Bind<ITargetFrameworkMonikerParser>().To<TargetFrameworkMonikerParser>()
            .Bind<IEnvironmentVariables>().Bind<ITraceSource>(typeof(EnvironmentVariables)).To<EnvironmentVariables>()
            .Bind<IActive>(typeof(Debugger)).To<Debugger>()
            .Bind<IDockerSettings>().To<DockerSettings>().Root<IDockerSettings>()
            .Bind<IBuildContext>("base").As(Lifetime.Transient).To<BuildContext>()
            .Bind<IBuildContext>().As(Lifetime.Transient).To<ReliableBuildContext>()
            .Bind<ITextToColorStrings>().To<TextToColorStrings>()
            .Bind<IFileExplorer>().To<FileExplorer>()
            .Bind<IProcessOutputWriter>().To<ProcessOutputWriter>()
            .Bind<IBuildMessageLogWriter>().To<BuildMessageLogWriter>()
            .Bind<MemoryPool<TT>>().To(_ => MemoryPool<TT>.Shared)
            .Bind<IMessageIndicesReader>().To<MessageIndicesReader>()
            .Bind<IMessagesReader>().To<MessagesReader>()
            .Bind<IPathResolverContext>().Bind<IVirtualContext>().To<PathResolverContext>().Root<IPathResolverContext>().Root<IVirtualContext>()
            .Bind<IEncoding>().To<Utf8Encoding>()
            .Bind<IProcessMonitor>().As(Lifetime.Transient).To<ProcessMonitor>()
            .Bind<IBuildOutputProcessor>().To<BuildOutputProcessor>()
            .Bind<IBuildMessagesProcessor>("default").To<DefaultBuildMessagesProcessor>()
            .Bind<IBuildMessagesProcessor>("custom").To<CustomMessagesProcessor>().Root<ScriptHostComponents>("ScriptHostComponents")
            .Bind<IPresenter<Summary>>().To<SummaryPresenter>()
            .Bind<IExitCodeParser>().To<ExitCodeParser>()
            .Bind<IProcessRunner>("base").To<ProcessRunner>()
            .Bind<IProcessRunner>().To<ProcessInFlowRunner>()
            .Bind<IDotNetSettings>().Bind<ITeamCityContext>().To<TeamCityContext>().Root<IDotNetSettings>()
            .Bind<IProcessResultHandler>().To<ProcessResultHandler>()
            .Bind<IRuntimeExplorer>().To<RuntimeExplorer>()
            .Bind<INuGetReferenceResolver>().To<NuGetReferenceResolver>()
            .Bind<SourceReferenceResolver>().As(Lifetime.Transient).To<SourceResolver>()
            .Bind<MetadataReferenceResolver>().As(Lifetime.Transient).To<MetadataResolver>()
            .Bind<IScriptContentReplacer>().To<ScriptContentReplacer>()
            .Bind<ITextReplacer>().To<TextReplacer>()

            // Script options factory
            .Bind<ISettingGetter<LanguageVersion>>().Bind<ISettingSetter<LanguageVersion>>().To(_ => new Setting<LanguageVersion>(LanguageVersion.Default))
            .Bind<ISettingGetter<OptimizationLevel>>().Bind<ISettingSetter<OptimizationLevel>>().To(_ => new Setting<OptimizationLevel>(OptimizationLevel.Release))
            .Bind<ISettingGetter<WarningLevel>>().Bind<ISettingSetter<WarningLevel>>().To(_ => new Setting<WarningLevel>((WarningLevel)ScriptOptions.Default.WarningLevel))
            .Bind<ISettingGetter<CheckOverflow>>().Bind<ISettingSetter<CheckOverflow>>().To(_ => new Setting<CheckOverflow>(ScriptOptions.Default.CheckOverflow ? CheckOverflow.On : CheckOverflow.Off))
            .Bind<ISettingGetter<AllowUnsafe>>().Bind<ISettingSetter<AllowUnsafe>>().To(_ => new Setting<AllowUnsafe>(ScriptOptions.Default.AllowUnsafe ? AllowUnsafe.On : AllowUnsafe.Off))
            .Bind<IAssembliesProvider>().To<AssembliesProvider>()
            .Bind<IScriptOptionsFactory>().Bind<IActive>().Tags(typeof(AssembliesScriptOptionsProvider)).To<AssembliesScriptOptionsProvider>()
            .Bind<IScriptOptionsFactory>(typeof(ConfigurableScriptOptionsFactory)).To<ConfigurableScriptOptionsFactory>()
            .Bind<IScriptOptionsFactory>(typeof(ReferencesScriptOptionsFactory)).Bind<IReferenceRegistry>().To<ReferencesScriptOptionsFactory>()
            .Bind<IScriptOptionsFactory>(typeof(SourceFileScriptOptionsFactory)).To<SourceFileScriptOptionsFactory>()
            .Bind<IScriptOptionsFactory>(typeof(MetadataResolverOptionsFactory)).To<MetadataResolverOptionsFactory>()
            .Bind<IScriptOptionsFactory>(typeof(ImportsOptionsFactory)).To<ImportsOptionsFactory>()
            .Bind<ICommandFactory<string>>("REPL Set a C# language version parser").To<SettingCommandFactory<LanguageVersion>>()
            .Bind<ICommandRunner>("REPL Set a C# language version").To<SettingCommandRunner<LanguageVersion>>()
            .Bind<ISettingDescription>(typeof(LanguageVersion)).To<LanguageVersionSettingDescription>()
            .Bind<ICommandFactory<string>>("REPL Set an optimization level parser").To<SettingCommandFactory<OptimizationLevel>>()
            .Bind<ICommandRunner>("REPL Set an optimization level").To<SettingCommandRunner<OptimizationLevel>>()
            .Bind<ISettingDescription>(typeof(OptimizationLevel)).To<OptimizationLevelSettingDescription>()
            .Bind<ICommandFactory<string>>("REPL Set a warning level parser").To<SettingCommandFactory<WarningLevel>>()
            .Bind<ICommandRunner>("REPL Set a warning level").To<SettingCommandRunner<WarningLevel>>()
            .Bind<ISettingDescription>(typeof(WarningLevel)).To<WarningLevelSettingDescription>()
            .Bind<ICommandFactory<string>>("REPL Set an overflow check parser").To<SettingCommandFactory<CheckOverflow>>()
            .Bind<ICommandRunner>("REPL Set an overflow check").To<SettingCommandRunner<CheckOverflow>>()
            .Bind<ISettingDescription>(typeof(CheckOverflow)).To<CheckOverflowSettingDescription>()
            .Bind<ICommandFactory<string>>("REPL Set allow unsafe parser").To<SettingCommandFactory<AllowUnsafe>>()
            .Bind<ICommandRunner>("REPL Set allow unsafe").To<SettingCommandRunner<AllowUnsafe>>()
            .Bind<ISettingDescription>(typeof(AllowUnsafe)).To<AllowUnsafeSettingDescription>()
            .Bind<ICommandFactory<string>>("REPL Set NuGet restore setting parser").To<SettingCommandFactory<NuGetRestoreSetting>>()
            .Bind<ICommandRunner>("REPL Set NuGet restore setting").To<SettingCommandRunner<NuGetRestoreSetting>>()
            .Bind<ISettingDescription>(typeof(NuGetRestoreSetting)).To<NuGetRestoreSettingDescription>()
            .Bind<IScriptSubmissionAnalyzer>().To<ScriptSubmissionAnalyzer>()
            .Bind<ICommandRunner>("CSharp").To<CSharpScriptCommandRunner>()
            .Bind<ICommandFactory<string>>("REPL Help parser").To<HelpCommandFactory>()
            .Bind<ICommandRunner>("REPL Help runner").To<HelpCommandRunner>()
            .Bind<ICommandFactory<string>>("REPL Set verbosity level parser").To<SettingCommandFactory<VerbosityLevel>>()
            .Bind<ICommandRunner>("REPL Set verbosity level runner").To<SettingCommandRunner<VerbosityLevel>>()
            .Bind<ICommandFactory<string>>("REPL Add NuGet reference parser").To<AddNuGetReferenceCommandFactory>()
            .Bind<IFilePathResolver>().To<FilePathResolver>()
            .Bind<ICommandRunner>("REPL Add package reference runner").To<AddNuGetReferenceCommandRunner>()
            .Bind<IStartInfoFactory>().To<StartInfoFactory>()
            .Bind<IProcessManager>().As(Lifetime.Transient).To<ProcessManager>()
            .Bind<IProperties>("Default", "Ansi").To<Properties>()
            .Bind<IProperties>("TeamCity").To<TeamCityProperties>()

            // Public
            .Bind<IHost>().To<HostService>().Root<IHost>()
            .Bind<IServiceCollection>().To(_ => CreateServiceCollection(this)).Root<IServiceCollection>()
            .Bind<IServiceProvider>().To(ctx =>
            {
                ctx.Inject<IServiceCollection>(out var serviceCollection);
                return CreateServiceProvider(serviceCollection);
            }).Root<IServiceProvider>()
            .Bind<IProperties>().To(ctx =>
            {
                ctx.Inject<ICISpecific<IProperties>>(out var properties);
                return properties.Instance;
            })
            .Bind<INuGet>().To<NuGetService>().Root<INuGet>()
            .Bind<ICommandLineRunner>().To<CommandLineRunner>().Root<ICommandLineRunner>()
            .Bind<IBuildRunner>().To<BuildRunner>().Root<IBuildRunner>()

            // TeamCity Service messages
            .Bind<ITeamCityServiceMessages>().To<TeamCityServiceMessages>()
            .Bind<IServiceMessageFormatter>().To<ServiceMessageFormatter>()
            .Bind<IFlowIdGenerator>().Bind<IFlowContext>().To<FlowIdGenerator>()
            .Bind<DateTime>().As(Lifetime.Transient).To(_ => DateTime.Now)
            .Bind<IServiceMessageUpdater>().To<TimestampUpdater>()
            .Bind<ITeamCityWriter>().To(
                ctx =>
                {
                    ctx.Inject<ITeamCityServiceMessages>(out var teamCityServiceMessages);
                    return teamCityServiceMessages.CreateWriter(
                        str =>
                        {
                            ctx.Inject<IConsole>(out var console);
                            console.WriteToOut((default, str + "\n"));
                        });
                }).Root<ITeamCityWriter>()
            .Bind<IServiceMessageParser>().To<ServiceMessageParser>().Root<IServiceMessageParser>();
    }
}