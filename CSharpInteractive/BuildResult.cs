// ReSharper disable ReturnTypeCanBeEnumerable.Local
namespace CSharpInteractive;

using System.Diagnostics;
using System.Text;
using HostApi;
using Immutype;

[Target]
[DebuggerTypeProxy(typeof(BuildResultDebugView))]
internal class BuildResult : IBuildResult
{
    private readonly Lazy<BuildStatistics> _summary;

    // ReSharper disable once UnusedMember.Global
    public BuildResult(IStartInfo startInfo)
        : this(startInfo, Array.Empty<BuildMessage>(), Array.Empty<BuildMessage>(), Array.Empty<TestResult>(), default)
    { }

    public BuildResult(
        IStartInfo startInfo,
        IReadOnlyList<BuildMessage> errors,
        IReadOnlyList<BuildMessage> warnings,
        IReadOnlyList<TestResult> tests,
        int? exitCode)
    {
        StartInfo = startInfo;
        Errors = errors;
        Warnings = warnings;
        Tests = tests;
        ExitCode = exitCode;
        _summary = new Lazy<BuildStatistics>(CalculateSummary, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public BuildStatistics Summary => _summary.Value;

    public IStartInfo StartInfo { get; }

    public IReadOnlyList<BuildMessage> Errors { get; }

    public IReadOnlyList<BuildMessage> Warnings { get; }

    public IReadOnlyList<TestResult> Tests { get; }

    public int? ExitCode { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(string.IsNullOrWhiteSpace(StartInfo.ShortName) ? "Build" : $"\"{StartInfo.ShortName}\"");
        sb.Append(" is ");
        sb.Append(ExitCode.HasValue ? "finished" : "not finished");
        if (Summary.IsEmpty != true)
        {
            sb.Append(" with ");
            sb.Append(Summary);
        }

        sb.Append('.');
        return sb.ToString();
    }

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
                case TestState.Passed:
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

    private class BuildResultDebugView(IBuildResult buildResult)
    {
        public BuildStatistics Summary => buildResult.Summary;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<BuildMessage> Errors => buildResult.Errors;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<BuildMessage> Warnings => buildResult.Warnings;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<TestResult> Tests => buildResult.Tests;

        public IStartInfo StartInfo => buildResult.StartInfo;

        public int? ExitCode => buildResult.ExitCode;
    }
}