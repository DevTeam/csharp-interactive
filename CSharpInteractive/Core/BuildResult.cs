// ReSharper disable ReturnTypeCanBeEnumerable.Local
namespace CSharpInteractive.Core;

using System.Diagnostics;
using System.Text;
using HostApi;
using Immutype;

[DebuggerTypeProxy(typeof(BuildResultDebugView))]
[Target]
internal class BuildResult : IBuildResult, ISuccessDeterminant
{
    private readonly Lazy<BuildStatistics> _summary;

    // ReSharper disable once UnusedMember.Global
    public BuildResult(ICommandLineResult commandLineResult)
        : this(commandLineResult, Array.Empty<BuildMessage>(), Array.Empty<BuildMessage>(), Array.Empty<TestResult>())
    { }
    
    public BuildResult(
        ICommandLineResult commandLineResult,
        IReadOnlyList<BuildMessage> errors,
        IReadOnlyList<BuildMessage> warnings,
        IReadOnlyList<TestResult> tests)
    {
        CommandLineResult = commandLineResult;
        Errors = errors;
        Warnings = warnings;
        Tests = tests;
        _summary = new Lazy<BuildStatistics>(CalculateSummary, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public ICommandLineResult CommandLineResult { get; }

    public IStartInfo StartInfo => CommandLineResult.StartInfo;

    public ProcessState State => CommandLineResult.State;

    public long ElapsedMilliseconds => CommandLineResult.ElapsedMilliseconds;
    
    public int? ExitCode => CommandLineResult.ExitCode;

    public Exception? Error => CommandLineResult.Error;

    public BuildStatistics Summary => _summary.Value;
    
    public IReadOnlyList<BuildMessage> Errors { get; }

    public IReadOnlyList<BuildMessage> Warnings { get; }

    public IReadOnlyList<TestResult> Tests { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(CommandLineResult);
        // ReSharper disable once InvertIf
        if (!Summary.IsEmpty)
        {
            sb.Append(" with ");
            sb.Append(Summary);
        }
        
        return sb.ToString();
    }

    public bool? IsSuccess => 
        ExitCode == 0
        && Error is null
        && State == ProcessState.Finished
        && Errors.Count == 0
        && Summary is {FailedTests: 0, Errors: 0};

    private BuildStatistics CalculateSummary()
    {
        var testItems =
            from testGroup in
                from testResult in Tests
                group testResult by (AssemblyName: testResult.SuiteName, testResult.Name)
            select testGroup.MaxBy(i => i.State);

        var totalTests = 0;
        var failedTests = 0;
        var ignoredTests = 0;
        var passedTests = 0;
        foreach (var test in testItems)
        {
            totalTests++;
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (test.State)
            {
                case TestState.Finished:
                    passedTests++;
                    break;

                case TestState.Failed:
                    failedTests++;
                    break;

                case TestState.Ignored:
                    ignoredTests++;
                    break;
            }
        }

        return new BuildStatistics(
            Errors.Count,
            Warnings.Count,
            totalTests,
            failedTests,
            ignoredTests,
            passedTests);
    }

    private class BuildResultDebugView(BuildResult buildResult)
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public ICommandLineResult CommandLineResult => buildResult.CommandLineResult;
        
        public BuildStatistics Summary => buildResult.Summary;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<BuildMessage> Errors => buildResult.Errors;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<BuildMessage> Warnings => buildResult.Warnings;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<TestResult> Tests => buildResult.Tests;
    }
}