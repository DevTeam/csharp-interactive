// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedType.Global
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// Builds a project and all of its dependencies. It allows access to a fully functional MSBuild. The command has the exact same capabilities as the existing MSBuild command-line client for SDK-style projects only. The options are all the same.
/// <example>
/// <code>
/// var buildProps = new[] {("version", "1.0.3")};
/// new MSBuild()
///     .WithProject("My.sln")
///     .WithRestore(true).WithTarget("Rebuild;VSTest;Pack")
///     .WithProps(buildProps)
///     .Build().EnsureSuccess();
/// </code>
/// </example> 
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Props">Set or override these project-level properties.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="RestoreProps">Set or override these project-level properties only during restore and do not use properties specified with the -property argument. name is the property name, and value is the property value.</param>
/// <param name="InputResultsCaches">List of input cache files that MSBuild will read build results from. Setting this also turns on isolated builds.</param>
/// <param name="IgnoreProjectExtensions">List of extensions to ignore when determining which project file to build. Use a semicolon or a comma to separate multiple extensions.</param>
/// <param name="WarnAsError">List of warning codes to treats as errors. Use a semicolon or a comma to separate multiple warning codes. To treat all warnings as errors use the switch with no values.</param>
/// <param name="WarnAsMessage">List of warning codes to treats as low importance messages. Use a semicolon or a comma to separate multiple warning codes.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">Builds the specified targets in the project file. If a project file is not specified, MSBuild searches the current working directory for a file that has a file extension that ends in "proj" and uses that file. If a directory is specified, MSBuild searches that directory for a project file.</param>
/// <param name="Target">Build these targets in this project. Use a semicolon or a comma to separate multiple targets, or specify each target separately.</param>
/// <param name="MaxCpuCount">Specifies the maximum number of concurrent processes to build with. If the switch is not used, the default value used is 1. If the switch is used without a value MSBuild will use up to the number of processors on the computer.</param>
/// <param name="ToolsVersion">The version of the MSBuild Toolset (tasks, targets, etc.) to use during build. This version will override the versions specified by individual projects.</param>
/// <param name="NodeReuse">Enables or Disables the reuse of MSBuild nodes.</param>
/// <param name="Preprocess">Creates a single, aggregated project file by inlining all the files that would be imported during a build, with their boundaries marked. This can be useful for figuring out what files are being imported and from where, and what they will contribute to the build. By default the output is written to the console window. If the path to an output file is provided that will be used instead.</param>
/// <param name="DetailedSummary">Shows detailed information at the end of the build about the configurations built and how they were scheduled to nodes.</param>
/// <param name="Restore">Runs a target named Restore prior to building other targets and ensures the build for these targets uses the latest restored build logic. This is useful when your project tree requires packages to be restored before it can be built.</param>
/// <param name="ProfileEvaluation">Profiles MSBuild evaluation and writes the result to the specified file. If the extension of the specified file is '.md', the result is generated in markdown format. Otherwise, a tab separated file is produced.</param>
/// <param name="IsolateProjects">Causes MSBuild to build each project in isolation. This is a more restrictive mode of MSBuild as it requires that the project graph be statically discoverable at evaluation time, but can improve scheduling and reduce memory overhead when building a large set of projects.</param>
/// <param name="OutputResultsCache">Output cache file where MSBuild will write the contents of its build result caches at the end of the build. Setting this also turns on isolated builds.</param>
/// <param name="GraphBuild">Causes MSBuild to construct and build a project graph. Constructing a graph involves identifying project references to form dependencies. Building that graph involves attempting to build project references prior to the projects that reference them, differing from traditional MSBuild scheduling.</param>
/// <param name="LowPriority">Causes MSBuild to run at low process priority.</param>
/// <param name="NoAutoResponse">Do not auto-include any MSBuild.rsp files.</param>
/// <param name="NoLogo">Do not display the startup banner and copyright message.</param>
/// <param name="DisplayVersion">Display version information only.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record MSBuild(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Props,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> RestoreProps,
    IEnumerable<string> InputResultsCaches,
    IEnumerable<string> IgnoreProjectExtensions,
    IEnumerable<string>  WarnAsError,
    IEnumerable<string> WarnAsMessage,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Project = "",
    string Target = "",
    int? MaxCpuCount = default,
    string ToolsVersion = "",
    bool? NodeReuse = default,
    string Preprocess = "",
    bool DetailedSummary = false,
    bool? Restore = default,
    string ProfileEvaluation = "",
    bool? IsolateProjects = default,
    string OutputResultsCache = "",
    bool? GraphBuild = default,
    bool? LowPriority = default,
    bool? NoAutoResponse = default,
    bool? NoLogo = default,
    bool? DisplayVersion = default,
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    public MSBuild()
        : this(
            [],
            [],
            [],
            [],
            [],
            [],
            [],
            [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs(ExecutablePath == string.Empty ? ["msbuild"] : [])
            .AddArgs(new[] {Project}.Where(i => !string.IsNullOrWhiteSpace(i)).ToArray())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddMSBuildLoggers(host, Verbosity)
            .AddMSBuildArgs(
                ("-target", Target),
                ("-maxCpuCount", MaxCpuCount?.ToString()),
                ("-toolsVersion", ToolsVersion),
                ("-verbosity", Verbosity?.ToString().ToLower()),
                ("-warnAsError", JoinWithSemicolons(WarnAsError)),
                ("-warnAsMessage", JoinWithSemicolons(WarnAsMessage)),
                ("-ignoreProjectExtensions", JoinWithSemicolons(IgnoreProjectExtensions)),
                ("-nodeReuse", NodeReuse?.ToString()),
                ("-preprocess", Preprocess),
                ("-restore", Restore?.ToString()),
                ("-profileEvaluation", ProfileEvaluation),
                ("-isolateProjects", IsolateProjects?.ToString()),
                ("-inputResultsCaches", JoinWithSemicolons(InputResultsCaches)),
                ("-outputResultsCache", OutputResultsCache),
                ("-graphBuild", GraphBuild?.ToString()),
                ("-lowPriority", LowPriority?.ToString())
            )
            .AddBooleanArgs(
                ("-detailedSummary", DetailedSummary),
                ("-noAutoResponse", NoAutoResponse),
                ("-noLogo", NoLogo),
                ("-version", DisplayVersion)
            )
            .AddProps("-restoreProperty", RestoreProps.ToArray())
            .AddProps("-p", Props.ToArray())
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => 
        (ExecutablePath == string.Empty ? "dotnet msbuild" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName(ShortName, Project);

    private static string JoinWithSemicolons(IEnumerable<string> arg) => 
        string.Join(";", arg.Where(i => !string.IsNullOrWhiteSpace(i)).Select(i => i.Trim()));
}