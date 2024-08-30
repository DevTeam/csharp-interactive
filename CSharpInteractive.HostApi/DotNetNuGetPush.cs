// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
namespace HostApi;

using Internal.DotNet;

/// <summary>
/// The dotnet push command pushes a package to the server and publishes it.
/// <example>
/// <code>
/// var apiKey = Props.Get("apiKey", "");
/// 
/// 
/// new DotNetNuGetPush().WithApiKey(apiKey);
/// </code>
/// </example> 
/// </summary>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Sources">Specifies the server URL. NuGet identifies a UNC or local folder source and simply copies the file there instead of pushing it using HTTP.</param>
/// <param name="SymbolSources">Specifies the symbol server URL.</param>
/// <param name="Package">Specifies the file path to the package to be pushed.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="ForceEnglishOutput">Forces the application to run using an invariant, English-based culture.</param>
/// <param name="Timeout">Specifies the timeout for pushing to a server in seconds. Defaults to 300 seconds (5 minutes). Specifying 0 applies the default value.</param>
/// <param name="ApiKey">The API key for the server.</param>
/// <param name="SymbolApiKey">The API key for the symbol server.</param>
/// <param name="DisableBuffering">Disables buffering when pushing to an HTTP(S) server to reduce memory usage.</param>
/// <param name="NoSymbols">Doesn't push symbols (even if present).</param>
/// <param name="NoServiceEndpoint">Doesn't append "api/v2/package" to the source URL.</param>
/// <param name="SkipDuplicate">When pushing multiple packages to an HTTP(S) server, treats any 409 Conflict response as a warning so that the push can continue.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetNuGetPush(
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Sources,
    IEnumerable<string> SymbolSources,
    string Package = "",
    string ExecutablePath = "",
    string WorkingDirectory = "",
    bool? ForceEnglishOutput = default,
    int? Timeout = default,
    string ApiKey = "",
    string SymbolApiKey = "",
    bool? DisableBuffering = default,
    bool? NoSymbols = default,
    bool? NoServiceEndpoint = default,
    bool? SkipDuplicate = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetNuGetPush(params string[] args)
        : this(args, [], [], [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        return host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("nuget", "push")
            .AddNotEmptyArgs(Package)
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddArgs(Sources.Select(i => ("--source", (string?)i)).ToArray())
            .AddArgs(SymbolSources.Select(i => ("--symbol-source", (string?)i)).ToArray())
            .AddArgs(
                ("--timeout", Timeout?.ToString()),
                ("--api-key", ApiKey),
                ("--symbol-api-key", SymbolApiKey)
            )
            .AddBooleanArgs(
                ("--force-english-output", ForceEnglishOutput),
                ("--disable-buffering", DisableBuffering),
                ("--no-symbols", NoSymbols),
                ("--no-service-endpoint", NoServiceEndpoint),
                ("--skip-duplicate", SkipDuplicate)
            )
            .AddArgs(Args.ToArray());
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet nuget push".GetShortName(ShortName, Package);
}