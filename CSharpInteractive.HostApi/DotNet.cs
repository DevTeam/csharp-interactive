// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace HostApi;

using Internal.DotNet;

/// <summary>
/// Runs a dotnet application.
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the application.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="AdditionalProbingPaths">Paths containing probing policy and assemblies to probe.</param>
/// <param name="PathToApplication">Specifies the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp.dll runs the myapp application.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="AdditionalDeps">Path to an additional .deps.json file. A deps.json file contains a list of dependencies, compilation dependencies, and version information used to address assembly conflicts.</param>
/// <param name="FxVersion">Version of the .NET runtime to use to run the application.</param>
/// <param name="RollForward">Controls how roll forward is applied to the app. The SETTING can be one of the following values. If not specified, <see cref="HostApi.RollForward.Minor"/> is the default.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNet(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> AdditionalProbingPaths,
    string PathToApplication,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string AdditionalDeps = "",
    string FxVersion = "",
    RollForward? RollForward = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="pathToApplication">Specifies the path to an application .dll file to run the application. To run the application means to find and execute the entry point, which in the case of console apps is the Main method. For example, dotnet myapp.dll runs the myapp application.</param>
    public DotNet(string pathToApplication)
        : this([], [], [], pathToApplication)
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs(
                ("--additional-deps", AdditionalDeps),
                ("--fx-version", FxVersion),
                ("--roll-forward", RollForward?.ToString())
            )
            .AddArgs(AdditionalProbingPaths.Select(i => ("--additionalprobingpath", (string?)i)).ToArray())
            .AddArgs(PathToApplication)
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet".GetShortName(ShortName, PathToApplication);
}