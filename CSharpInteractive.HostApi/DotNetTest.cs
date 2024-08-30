// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo
namespace HostApi;

using Internal;
using Internal.DotNet;

/// <summary>
/// The dotnet test command is used to execute unit tests in a given solution. The dotnet test command builds the solution and runs a test host application for each test project in the solution. The test host executes tests in the given project using a test framework, for example: MSTest, NUnit, or xUnit, and reports the success or failure of each test. If all tests are successful, the test runner returns 0 as an exit code; otherwise if any test fails, it returns 1.
/// <example>
/// <code>
/// new DotNetNew("mstest", "-n", "MyTests", "--force")
///     .Build().EnsureSuccess();
///
/// 
/// new DotNetTest().WithWorkingDirectory("MyTests")
///     .Build().EnsureSuccess();
/// </code>
/// </example>
/// </summary>
/// <param name="Props">MSBuild options for setting properties.</param>
/// <param name="Args">Specifies the set of command line arguments to use when starting the tool.</param>
/// <param name="Vars">Specifies the set of environment variables that apply to this process and its child processes.</param>
/// <param name="RunSettings">Specifies RunSettings passed to tests.</param>
/// <param name="Loggers">Specifies a logger for test results. Use "console;verbosity=detailed". Specify the parameter multiple times to enable multiple loggers.</param>
/// <param name="ExecutablePath">Overrides the tool executable path.</param>
/// <param name="WorkingDirectory">Specifies the working directory for the tool to be started.</param>
/// <param name="Project">Path to the test project, to the solution, to a directory that contains a project or a solution or to a test project .dll file.</param>
/// <param name="Settings">The .runsettings file to use for running the tests. The TargetPlatform element (x86|x64) has no effect for dotnet test. To run tests that target x86, install the x86 version of .NET Core. The bitness of the dotnet.exe that is on the path is what will be used for running tests.</param>
/// <param name="ListTests">List the discovered tests instead of running the tests.</param>
/// <param name="Filter">Filters out tests in the current project using the given expression. For more information, see the Filter option details section. For more information and examples on how to use selective unit test filtering, see Running selective unit tests.</param>
/// <param name="TestAdapterPath">Path to a directory to be searched for additional test adapters. Only .dll files with suffix .TestAdapter.dll are inspected. If not specified, the directory of the test .dll is searched.</param>
/// <param name="Configuration">Defines the build configuration. The default for most projects is Debug, but you can override the build configuration settings in your project.</param>
/// <param name="Framework">Forces the use of dotnet or .NET Framework test host for the test binaries. This option only determines which type of host to use. The actual framework version to be used is determined by the runtimeconfig.json of the test project. When not specified, the TargetFramework assembly attribute is used to determine the type of host. When that attribute is stripped from the .dll, the .NET Framework host is used.</param>
/// <param name="Runtime">The target runtime to test for.</param>
/// <param name="Output">Directory in which to find the binaries to run. If not specified, the default path is ./bin/&lt;configuration&gt;/&lt;framework&gt;/. For projects with multiple target frameworks (via the TargetFrameworks property), you also need to define --framework when you specify this option. dotnet test always runs tests from the output directory. You can use AppDomain.BaseDirectory to consume test assets in the output directory.</param>
/// <param name="Diag">Enables diagnostic mode for the test platform and writes diagnostic messages to the specified file and to files next to it. The process that is logging the messages determines which files are created, such as *.host_&lt;date&gt;.txt for test host log, and *.datacollector_&lt;date&gt;.txt for data collector log.</param>
/// <param name="NoBuild">Doesn't build the test project before running it. It also implicitly sets the - --no-restore flag.</param>
/// <param name="ResultsDirectory">The directory where the test results are going to be placed. If the specified directory doesn't exist, it's created. The default is TestResults in the directory that contains the project file.</param>
/// <param name="Collect">Enables data collector for the test run. For more information, see Monitor and analyze test run.</param>
/// <param name="Blame">Runs the tests in blame mode. This option is helpful in isolating problematic tests that cause the test host to crash. When a crash is detected, it creates a sequence file in TestResults/&lt;Guid&gt;/&lt;Guid&gt;_Sequence.xml that captures the order of tests that were run before the crash.</param>
/// <param name="BlameCrash">Runs the tests in blame mode and collects a crash dump when the test host exits unexpectedly. This option depends on the version of .NET used, the type of error, and the operating system.</param>
/// <param name="BlameCrashDumpType">The type of crash dump to be collected. Implies BlameCrash.</param>
/// <param name="BlameCrashCollectAlways">Collects a crash dump on expected as well as unexpected test host exit.</param>
/// <param name="BlameHang">Run the tests in blame mode and collects a hang dump when a test exceeds the given timeout.</param>
/// <param name="BlameHangDumpType">The type of crash dump to be collected. It should be full, mini, or none. When none is specified, test host is terminated on timeout, but no dump is collected. Implies BlameHang.</param>
/// <param name="BlameHangTimeout">Per-test timeout, after which a hang dump is triggered and the test host process and all of its child processes are dumped and terminated.</param>
/// <param name="NoLogo">Run tests without displaying the Microsoft TestPlatform banner. Available since .NET Core 3.0 SDK.</param>
/// <param name="NoRestore">Doesn't execute an implicit restore when running the command.</param>
/// <param name="Arch">Specifies the target architecture. This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --arch x86 sets the RID to win-x86. If you use this option, don't use the -r|--runtime option. Available since .NET 6 Preview 7.</param>
/// <param name="OS">Specifies the target operating system (OS). This is a shorthand syntax for setting the Runtime Identifier (RID), where the provided value is combined with the default RID. For example, on a win-x64 machine, specifying --os linux sets the RID to linux-x64. If you use this option, don't use the -r|--runtime option. Available since .NET 6.</param>
/// <param name="Verbosity">Sets the verbosity level of the command. Allowed values are Quiet, Minimal, Normal, Detailed, and Diagnostic. The default is Minimal. For more information, see LoggerVerbosity.</param>
/// <param name="ShortName">Specifies a short name for this operation.</param>
[Target]
public partial record DotNetTest(
    IEnumerable<(string name, string value)> Props,
    IEnumerable<string> Args,
    IEnumerable<(string name, string value)> Vars,
    IEnumerable<(string name, string value)> RunSettings,
    IEnumerable<string> Loggers,
    string ExecutablePath = "",
    string WorkingDirectory = "",
    string Project = "",
    string Settings = "",
    bool? ListTests = default,
    string Filter = "",
    string TestAdapterPath = "",
    string Configuration = "",
    string Framework = "",
    string Runtime = "",
    string Output = "",
    string Diag = "",
    bool? NoBuild = default,
    string ResultsDirectory = "",
    string Collect = "",
    bool? Blame = default,
    bool? BlameCrash = default,
    string BlameCrashDumpType = "",
    bool? BlameCrashCollectAlways = default,
    bool? BlameHang = default,
    string BlameHangDumpType = "",
    TimeSpan? BlameHangTimeout = default,
    bool? NoLogo = default,
    bool? NoRestore = default,
    string Arch = "",
    string OS = "",
    DotNetVerbosity? Verbosity = default,
    string ShortName = "")
{
    /// <summary>
    /// Create a new instance of the command.
    /// </summary>
    /// <param name="args">Specifies the set of command line arguments to use when starting the tool.</param>
    public DotNetTest(params string[] args)
        : this([], args, [], [], [])
    { }

    /// <inheritdoc/>
    public IStartInfo GetStartInfo(IHost host)
    {
        if (host == null) throw new ArgumentNullException(nameof(host));
        var blameHangTimeout = BlameHangTimeout?.TotalMilliseconds;
        // ReSharper disable once UseDeconstruction
        var components = host.GetService<HostComponents>();
        var virtualContext = components.VirtualContext;
        var settings = components.DotNetSettings;
        var cmd = host.CreateCommandLine(ExecutablePath)
            .WithShortName(ToString())
            .WithArgs("test")
            .AddNotEmptyArgs(Project)
            .WithWorkingDirectory(WorkingDirectory)
            .WithVars(Vars.ToArray())
            .AddTestLoggers(host, Loggers)
            .AddArgs(
                ("--settings", Settings),
                ("--filter", Filter),
                ("--test-adapter-path", $"{string.Join(";", new[]{TestAdapterPath, virtualContext.Resolve(settings.DotNetVSTestLoggerDirectory)}.Where(i => !string.IsNullOrWhiteSpace(i)))}"),
                ("--configuration", Configuration),
                ("--framework", Framework),
                ("--runtime", Runtime),
                ("--output", Output),
                ("--diag", Diag),
                ("--results-directory", ResultsDirectory),
                ("--collect", Collect),
                ("--verbosity", Verbosity?.ToString().ToLowerInvariant()),
                ("--arch", Arch),
                ("--os", OS),
                ("--blame-crash-dump-type", BlameCrashDumpType),
                ("--blame-hang-dump-type", BlameHangDumpType),
                ("--blame-hang-timeout", blameHangTimeout.HasValue ? $"{(int)blameHangTimeout}milliseconds" : string.Empty)
            )
            .AddBooleanArgs(
                ("--list-tests", ListTests),
                ("--no-build", NoBuild),
                ("--blame", Blame),
                ("--blame-crash", BlameCrash),
                ("--blame-crash-collect-always", BlameCrashCollectAlways),
                ("--blame-hang", BlameHang),
                ("--nologo", NoLogo),
                ("--no-restore", NoRestore)
            )
            .AddProps("-p", Props.ToArray())
            .AddArgs(Args.ToArray());

        if (string.IsNullOrWhiteSpace(Project) || Path.GetExtension(Project).ToLowerInvariant() != ".dll")
        {
            cmd = cmd.AddMSBuildLoggers(host, Verbosity);
        }
        
        var runSettings = RunSettings.Select(i => $"{i.name}={i.value}").ToArray();
        if (runSettings.Any())
        {
            cmd = cmd.AddArgs("--").AddArgs(runSettings);
        }

        return cmd;
    }

    /// <inheritdoc/>
    public override string ToString() => "dotnet test".GetShortName(ShortName, Project);
}