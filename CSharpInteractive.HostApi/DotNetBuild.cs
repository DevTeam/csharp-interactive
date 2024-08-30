// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet build command builds the project and its dependencies into a set of binaries. The binaries include the project's code in Intermediate Language (IL) files with a .dll extension. Depending on the project type and settings, other files may be included.
/// <example>
/// <code>
/// var configuration = Props.Get("configuration", "Release");
///
/// 
/// new DotNetBuild().WithConfiguration(configuration)
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">The URI of the NuGet package source to use during the restore operation.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to build. If a project or solution file isn't specified, MSBuild searches the current working directory for a file that has a file extension that ends in either proj or sln and uses that file.</param>
/// <param name="Output">Directory in which to place the built binaries. If not specified, the default path is ./bin/&lt;configuration&gt;/&lt;framework&gt;/. For projects with multiple target frameworks (via the TargetFrameworks property), you also need to define --framework when you specify this option.</param>
/// <param name="Framework">Compiles for a specific framework. The framework must be defined in the project file.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Runtime">Specifies the target runtime. For a list of Runtime Identifiers (RIDs), see the RID catalog. If you use this option with .NET 6 SDK, use --self-contained or --no-self-contained also.</param>
/// <param name="VersionSuffix">Sets the value of the $(VersionSuffix) property to use when building the project. This only works if the $(Version) property isn't set. Then, $(Version) is set to the $(VersionPrefix) combined with the $(VersionSuffix), separated by a dash.</param>
/// <param name="NoIncremental">Marks the build as unsafe for incremental build. This flag turns off incremental compilation and forces a clean rebuild of the project's dependency graph.</param>
/// <param name="NoDependencies">Ignores project-to-project (P2P) references and only builds the specified root project.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message. Available since .NET Core 3.0 SDK.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore during build.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="SelfContained">Publishes the .NET runtime with the application so the runtime doesn't need to be installed on the target machine. The default is true if a runtime identifier is specified. Available since .NET 6 SDK.</param>
/// <param name="NoSelfContained">Publishes the application as a framework dependent application. A compatible .NET runtime must be installed on the target machine to run the application. Available since .NET 6 SDK.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don&amp;apos;t use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don&apos;t use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetBuild(
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Project = "",
    string Output = "",
    string Framework = "",
    string Configuration = "",
    string Runtime = "",
    string VersionSuffix = "",
    bool? NoIncremental = default,
    bool? NoDependencies = default,
    bool? NoLogo = default,
    bool? NoRestore = default,
    bool? Force = default,
    bool? SelfContained = default,
    bool? NoSelfContained = default,
    string Arch = "",
    string OS = "",
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetBuild(params string[] args)
        : this([], args, [], [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("build")
            .AddNotEmptyArgs(Project)
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(Sources.Select(i => ("--source", (string?)i)).ToArray())
            .AddArgs(
                ("--output", Output),
                ("--framework", Framework),
                ("--configuration", Configuration),
                ("--runtime", Runtime),
                ("--version-suffix", VersionSuffix),
                ("--verbosity", Verbosity?.ToString().ToLowerInvariant()),
                ("--arch", Arch),
                ("--os", OS)
            )
            .AddBooleanArgs(
                ("--no-incremental", NoIncremental),
                ("--no-dependencies", NoDependencies),
                ("--nologo", NoLogo),
                ("--no-restore", NoRestore),
                ("--self-contained", SelfContained),
                ("--no-self-contained", NoSelfContained),
                ("--force", Force)
            )
            .AddProps("-p", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet build".GetShortName(ShortName, Project);
}