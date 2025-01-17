// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Local

namespace HostApi;

using System.Diagnostics;
using System.Text;

/// <summary>
/// Represents a test result. 
/// </summary>
/// <param name="State">State of the test.</param>
/// <param name="Name">User-friendly name for the test.</param>
/// <param name="FlowId">Identifier of the flow in which the test was running.</param>
/// <param name="SuiteName">Suite name that includes the test.</param>
/// <param name="FullyQualifiedName">Fully qualified name of the test.</param>
/// <param name="DisplayName">Displayed test name.</param>
/// <param name="ResultDisplayName">Displayed test name with some details, such as test parameters.</param>
/// <param name="Message">Message displayed while the test is running.</param>
/// <param name="Details">Details of errors during test running.</param>
/// <param name="Duration">Duration of the test.</param>
/// <param name="Output">Messages displayed on the console during test running.</param>
/// <param name="Source">Container source from which the test is discovered.</param>
/// <param name="CodeFilePath">Source code file path of the test.</param>
/// <param name="Id">Test identifier.</param>
/// <param name="ExecutorUri">Uri of the Executor to use for running this test.</param>
/// <param name="LineNumber">Line number of the test in the source code file.</param>
[Target]
[DebuggerTypeProxy(typeof(TestResultDebugView))]
public readonly record struct TestResult(
    TestState State,
    string Name,
    string FlowId,
    string SuiteName,
    string FullyQualifiedName,
    string DisplayName,
    string ResultDisplayName,
    string Message,
    string Details,
    TimeSpan Duration,
    IReadOnlyList<Output> Output,
    string Source,
    string CodeFilePath,
    Guid Id,
    Uri? ExecutorUri,
    int? LineNumber)
{
    /// <summary>
    /// Creates a new instance of the test result.
    /// </summary>
    /// <param name="state">State of the test.</param>
    /// <param name="name">User-friendly name for the test.</param>
    public TestResult(TestState state, string name)
        : this(
            state,
            name,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            TimeSpan.Zero,
            Array.Empty<Output>(),
            string.Empty,
            string.Empty,
            Guid.Empty,
            null,
            null)
    { }

    /// <inheritdoc/>
    public override string ToString()
    {
        var sb = new StringBuilder();
        if (!string.IsNullOrWhiteSpace(SuiteName))
        {
            sb.Append(SuiteName);
            sb.Append(": ");
        }

        sb.Append(DisplayName);
        sb.Append(" is ");
        sb.Append(State.ToString().ToLowerInvariant());
        sb.Append('.');
        return sb.ToString();
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private class TestResultDebugView(TestResult testResult)
    {
        public TestState State => testResult.State;

        public string Name => testResult.Name;

        public string SuiteName => testResult.SuiteName;

        public string FullyQualifiedName => testResult.FullyQualifiedName;

        public string DisplayName => testResult.DisplayName;

        public string ResultDisplayName => testResult.ResultDisplayName;

        public string Message => testResult.Message;

        public string Details => testResult.Details;

        public TimeSpan Duration => testResult.Duration;

        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public IReadOnlyList<Output> Output => testResult.Output;

        public string Source => testResult.Source;

        public string CodeFilePath => testResult.CodeFilePath;

        public Guid Id => testResult.Id;

        public Uri? ExecutorUri => testResult.ExecutorUri;

        public int? LineNumber => testResult.LineNumber;
    }
}