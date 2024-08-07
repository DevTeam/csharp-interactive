// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable CommentTypo
namespace HostApi;

using Internal;
using Internal.DotNet;

/// <summary>
/// The dotnet vstest command runs the VSTest.Console command-line application to run automated unit tests.
/// <example>
/// <code>
/// new DotNetPublish()
///     .WithFramework("net8.0").AddProps(("PublishDir", ".bin"))
///     .Build().EnsureSuccess();
///
/// 
/// new VSTest()
///     .WithWorkingDirectory(".bin")
///     .WithTestFileNames("*.Tests.dll");
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <seealso cref="DotNetPublish"/>
[Target]
public partial record VSTest(
    IEnumerable<string> TestFileNames,
    // Specifies the set of command line arguments to use when starting the tool.
    IEnumerable<string> Args,
    // Specifies RunSettings passed to tests.
    IEnumerable<(string name, string value)> RunSettings,
    // Specifies the set of environment variables that apply to this process and its child processes.
    IEnumerable<(string name, string value)> Vars,
    // Specifies a logger for test results. Use "console;verbosity=detailed". Specify the parameter multiple times to enable multiple loggers.
    IEnumerable<string> Loggers,
    // Overrides the tool executable path.
    string ExecutablePath = "",
    // Specifies the working directory for the tool to be started.
    string WorkingDirectory = "",
    // Run tests with names that match the provided values. Separate multiple values with commas.
    string Tests = "",
    // Run tests that match the given expression. &lt;EXPRESSION&gt; is of the format &lt;property&gt;Operator&lt;value&gt;[|&amp;&lt;EXPRESSION&gt;], where Operator is one of =, !=, or ~. Operator ~ has &apos;contains&apos; semantics and is applicable for string properties like DisplayName. Parentheses () are used to group subexpressions.
    string TestCaseFilter = "",
    // Target .NET Framework version used for test execution. Examples of valid values are .NETFramework,Version=v4.6 or .NETCoreApp,Version=v1.0. Other supported values are Framework40, Framework45, FrameworkCore10, and FrameworkUap10.
    string Framework = "",
    // Target platform architecture used for test execution. Valid values are x86, x64, and ARM.
    VSTestPlatform? Platform = default,
    // Settings to use when running tests.
    string Settings = "",
    // Lists all discovered tests from the given test container.
    bool? ListTests = default,
    // Run tests in parallel. By default, all available cores on the machine are available for use. Specify an explicit number of cores by setting the MaxCpuCount property under the RunConfiguration node in the runsettings file.
    bool? Parallel = default,
    // Use custom test adapters from a given path (if any) in the test run.
    string TestAdapterPath = "",
    // Runs the tests in blame mode. This option is helpful in isolating the problematic tests causing test host to crash. It creates an output file in the current directory as Sequence.xml that captures the order of tests execution before the crash.
    bool? Blame = default,
    // Enables verbose logs for the test platform. Logs are written to the provided file.
    string Diag = "",
    // Test results directory will be created in specified path if not exists.
    string ResultsDirectory = "",
    // Process ID of the parent process responsible for launching the current process.
    int? ParentProcessId = default,
    // Specifies the port for the socket connection and receiving the event messages.
    int? Port = default,
    string Collect = "",
    // Runs the tests in an isolated process. This makes vstest.console.exe process less likely to be stopped on an error in the tests, but tests may run slower.
    bool? InIsolation = default,
    // Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.
    DotNetVerbosity? Verbosity = default,
    // Specifies a short name for this operation.
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public VSTest(params string[] args)
        : this([], args, [], [], [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        // ReSharper disable once UseDeconstruction
        var components = host.GetService<HostComponents>();
        var virtualContext = components.VirtualContext;
        var settings = components.DotNetSettings;
        var cmd = host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("vstest")
            .AddArgs(TestFileNames.Where(i => !string.IsNullOrWhiteSpace(i)).ToArray())
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddVSTestLoggers(host)
            .AddMSBuildArgs(
                ("--Logger", $"console;verbosity={(Verbosity.HasValue ? Verbosity.Value >= DotNetVerbosity.Normal ? Verbosity.Value : DotNetVerbosity.Normal : DotNetVerbosity.Normal).ToString().ToLowerInvariant()}"),
                ("--TestAdapterPath", $"{string.Join(";", new[]{TestAdapterPath, virtualContext.Resolve(settings.DotNetVSTestLoggerDirectory)}.Where(i => !string.IsNullOrWhiteSpace(i)))}"),
                ("--Tests", Tests),
                ("--TestCaseFilter", TestCaseFilter),
                ("--Framework", Framework),
                ("--Platform", Platform?.ToString()),
                ("--Settings", Settings),
                ("--Diag", Diag),
                ("--ParentProcessId", ParentProcessId?.ToString()),
                ("--Port", Port?.ToString()),
                ("--Collect", Collect))
            .AddMSBuildArgs(Loggers.Select(i => ("--logger", (string?)i)).ToArray())
            .AddVars(("TEAMCITY_SERVICE_MESSAGES_PATH", virtualContext.Resolve(settings.TeamCityMessagesPath)))
            .AddBooleanArgs(
                ("--ListTests", ListTests),
                ("--Parallel", Parallel),
                ("--Blame", Blame),
                ("--InIsolation", InIsolation)
            )
            .AddArgs(Args.ToArray());

        if (!string.IsNullOrWhiteSpace(ResultsDirectory))
        {
            cmd = cmd.AddArgs($"--ResultsDirectory:{ResultsDirectory}");
        }

        var runSettings = RunSettings.Select(i => $"{i.name}={i.value}").ToArray();
        if (runSettings.Length != 0)
        {
            cmd = cmd.AddArgs("--").AddArgs(runSettings);
        }

        return cmd;
    }
    
    /// <inheritdoc/>
    public override string ToString() => 
        (string.IsNullOrWhiteSpace(ShortName) ? "dotnet vstest" : ShortName).GetShortName(ShortName, string.Empty);
}