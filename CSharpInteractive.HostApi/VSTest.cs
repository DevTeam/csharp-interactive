// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable CommentTypo

namespace HostApi;

using Internal;
using Internal.DotNet;

/// <summary>
/// Runs tests from the specified assemblies.
/// <para>
/// This command runs the VSTest.Console command-line application to run automated unit tests.
/// </para>
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
/// <param name="TestFileNames">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Args">Specifies RunSettings passed to tests.</param>
/// <param name="RunSettings">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="Vars">Specifies a logger for test results. Use "console;verbosity=detailed". Specify the parameter multiple times to enable multiple loggers.</param>
/// <param name="Loggers">Overrides the tool executable path.</param>
/// <param name="ExecutablePath">Specifies the working directory for the tool to be started.</param>
/// <param name="WorkingDirectory">Run tests with names that match the provided values. Separate multiple values with commas.</param>
/// <param name="Tests">Run tests that match the given expression. &lt;EXPRESSION&gt; is of the format &lt;property&gt;Operator&lt;value&gt;[|&amp;&lt;EXPRESSION&gt;], where Operator is one of =, !=, or ~. Operator ~ has &apos;contains&apos; semantics and is applicable for string properties like DisplayName. Parentheses () are used to group subexpressions.</param>
/// <param name="TestCaseFilter">Target .NET Framework version used for test execution. Examples of valid values are .NETFramework,Version=v4.6 or .NETCoreApp,Version=v1.0. Other supported values are Framework40, Framework45, FrameworkCore10, and FrameworkUap10.</param>
/// <param name="Framework">Target .NET Framework version used for test execution. Examples of valid values are .NETFramework,Version=v4.6 or .NETCoreApp,Version=v1.0. Other supported values are Framework40, Framework45, FrameworkCore10, and FrameworkUap10.</param>
/// <param name="Platform">Target platform architecture used for test execution. Valid values are x86, x64, and ARM.</param>
/// <param name="Settings">Settings to use when running tests.</param>
/// <param name="ListTests">Lists all discovered tests from the given test container.</param>
/// <param name="Parallel">Run tests in parallel. By default, all available cores on the machine are available for use. Specify an explicit number of cores by setting the MaxCpuCount property under the RunConfiguration node in the runsettings file.</param>
/// <param name="TestAdapterPath">Use custom test adapters from a given path (if any) in the test run.</param>
/// <param name="Blame">Runs the tests in blame mode. This option is helpful in isolating the problematic tests causing test host to crash. It creates an output file in the current directory as Sequence.xml that captures the order of tests execution before the crash.</param>
/// <param name="Diag">Enables verbose logs for the test platform. Logs are written to the provided file.</param>
/// <param name="ResultsDirectory">Test results directory will be created in specified path if not exists.</param>
/// <param name="ParentProcessId">Process ID of the parent process responsible for launching the current process.</param>
/// <param name="Port">Specifies the port for the socket connection and receiving the event messages.</param>
/// <param name="Collect">Enables data collector for the test run.</param>
/// <param name="InIsolation">Runs the tests in an isolated process. This makes vstest.console.exe process less likely to be stopped on an error in the tests, but tests may run slower.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are <see cref="DotNetVerbosity.Quiet"/>, <see cref="DotNetVerbosity.Minimal"/>, <see cref="DotNetVerbosity.Normal"/>, <see cref="DotNetVerbosity.Detailed"/>, and <see cref="DotNetVerbosity.Diagnostic"/>. The default is <see cref="DotNetVerbosity.Minimal"/>. For more information, see <see cref="DotNetVerbosity"/>.</param>
/// <param name="Diagnostics">Enables diagnostic output.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
/// <seealso cref="DotNetPublish"/>
[Target]
public partial record VSTest(
    IEnumerable<string> TestFileNames,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> RunSettings,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<string> Loggers,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Tests = "",
    string TestCaseFilter = "",
    string Framework = "",
    VSTestPlatform? Platform = default,
    string Settings = "",
    bool? ListTests = default,
    bool? Parallel = default,
    string TestAdapterPath = "",
    bool? Blame = default,
    string Diag = "",
    string ResultsDirectory = "",
    int? ParentProcessId = default,
    int? Port = default,
    string Collect = "",
    bool? InIsolation = default,
    DotNetVerbosity? Verbosity = default,
    bool? Diagnostics = default,
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
                ("--TestAdapterPath", $"{string.Join(";", new[] {TestAdapterPath, virtualContext.Resolve(settings.DotNetVSTestLoggerDirectory)}.Where(i => !string.IsNullOrWhiteSpace(i)))}"),
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
                ("--InIsolation", InIsolation),
                ("--diagnostics", Diagnostics)
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
        (ExecutablePath == string.Empty ? "dotnet" : Path.GetFileNameWithoutExtension(ExecutablePath)).GetShortName("Runs tests from the specified assemblies.", ShortName, new [] {"vstest" }.Concat(TestFileNames).ToArg());
}