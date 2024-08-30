// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet pack command builds the project and creates NuGet packages. The result of this command is a NuGet package (that is, a .nupkg file).
/// <example>
/// <code>
/// new DotNetPack()
///     .Build()
/// .EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">The project or solution file to pack. If a project or solution file isn't specified, MSBuild searches the current working directory for a file that has a file extension that ends in either proj or sln and uses that file.</param>
/// <param name="Output">Places the built packages in the directory specified.</param>
/// <param name="Runtime">Specifies the target runtime to restore packages for. For a list of Runtime Identifiers (RIDs), see the RID catalog.</param>
/// <param name="NoBuild">Doesn't build the project before packing. It also implicitly sets the --no-restore flag.</param>
/// <param name="NoDependencies">Ignores project-to-project references and only restores the root project.</param>
/// <param name="IncludeSymbols">Includes the debug symbols NuGet packages in addition to the regular NuGet packages in the output directory.</param>
/// <param name="IncludeSource">Includes the debug symbols NuGet packages in addition to the regular NuGet packages in the output directory. The sources files are included in the src folder within the symbols package.</param>
/// <param name="Serviceable">Sets the serviceable flag in the package. For more information, see .NET Blog: .NET Framework 4.5.1 Supports Microsoft Security Updates for .NET NuGet Libraries.</param>
/// <param name="NoLogo">Doesn't display the startup banner or the copyright message. Available since .NET Core 3.0 SDK.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="VersionSuffix">Defines the value for the VersionSuffix MSBuild property. The effect of this property on the package version depends on the values of the Version and VersionPrefix properties.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="UseCurrentRuntime">Sets the RuntimeIdentifier to a platform portable RuntimeIdentifier based on the one of your machine. This happens implicitly with properties that require a RuntimeIdentifier, such as SelfContained, PublishAot, PublishSelfContained, PublishSingleFile, and PublishReadyToRun. If the property is set to false, that implicit resolution will no longer occur.</param>
/// <param name="Force">Forces all dependencies to be resolved even if the last restore was successful. Specifying this flag is the same as deleting the project.assets.json file.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
/// <seealso cref="DotNetBuild"/>
[Target]
public partial record DotNetPack(
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Project = "",
    string Output = "", 
    string Runtime = "",
    bool? NoBuild = default,
    bool? NoDependencies = default,
    bool? IncludeSymbols = default,
    bool? IncludeSource = default,
    bool? Serviceable = default,
    bool? NoLogo = default,
    bool? NoRestore = default,
    string VersionSuffix = "",
    string Configuration = "",
    bool? UseCurrentRuntime = default,
    bool? Force = default,
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetPack(params string[] args)
        : this([], args, [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("pack")
            .AddNotEmptyArgs(Project)
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddMSBuildLoggers(host, Verbosity)
            .AddArgs(
                ("--output", Output),
                ("--version-suffix", VersionSuffix),
                ("--configuration", Configuration),
                ("--runtime", Runtime)
            )
            .AddBooleanArgs(
                ("--no-build", NoBuild),
                ("--no-dependencies", NoDependencies),
                ("--include-symbols", IncludeSymbols),
                ("--include-source", IncludeSource),
                ("--serviceable", Serviceable),
                ("--nologo", NoLogo),
                ("--no-restore", NoRestore),
                ("--use-current-runtime", UseCurrentRuntime),
                ("--force", Force)
            )
            .AddProps("-p", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet pack".GetShortName(ShortName, Project);
}