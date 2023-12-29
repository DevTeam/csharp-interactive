// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Local
namespace HostApi;

using System.Diagnostics;
using System.Text;
using Immutype;

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
            default,
            default)
    { }

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