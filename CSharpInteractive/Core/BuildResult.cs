// ReSharper disable ReturnTypeCanBeEnumerable.Local

namespace CSharpInteractive.Core;

using System.Diagnostics;
using System.Text;
using HostApi;

[DebuggerTypeProxy(typeof(BuildResultDebugView))]
internal record BuildResult(
    ICommandLineResult CommandLineResult,
    IReadOnlyList<BuildMessage> Errors,
    IReadOnlyList<BuildMessage> Warnings,
    IReadOnlyList<TestResult> Tests) : IBuildResult, ISuccessDeterminant
{
#if NET9_0_OR_GREATER
    private readonly Lock _lockObject = new();
#else
    private readonly object _lockObject = new();
#endif

    // ReSharper disable once UnusedMember.Global
    public BuildResult(ICommandLineResult commandLineResult)
        : this(commandLineResult, [], [], [])
    { }

    public IStartInfo StartInfo => CommandLineResult.StartInfo;

    public ProcessState State => CommandLineResult.State;

    public long ElapsedMilliseconds => CommandLineResult.ElapsedMilliseconds;

    public int? ExitCode => CommandLineResult.ExitCode;

    public Exception? Error => CommandLineResult.Error;

    public BuildStatistics Summary
    {
        get
        {
            lock (_lockObject)
            {
                return field ??= CalculateSummary();
            }
        }
    }

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