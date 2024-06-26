// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable InconsistentNaming
namespace HostApi;

using JetBrains.TeamCity.ServiceMessages;

[ExcludeFromCodeCoverage]
[Target]
public readonly record struct BuildMessage(
    BuildMessageState State,
    IServiceMessage? ServiceMessage = default,
    string Text = "",
    string ErrorDetails = "",
    string Code = "",
    string File = "",
    string Subcategory = "",
    string ProjectFile = "",
    string SenderName = "",
    int? ColumnNumber = default,
    int? EndColumnNumber = default,
    int? LineNumber = default,
    int? EndLineNumber = default,
    DotNetMessageImportance? Importance = default)
{
    public TestResult? TestResult => 
        ServiceMessage is { } message && TryGetTestState(message.Name, out var testState)
            ? CreateResult(CreateKey(message), message, testState)
            : default(TestResult?);

    public override string ToString() => Text;
    
    internal static TestResult CreateResult(TestKey key, IServiceMessage message, TestState state)
    {
        var testSource = message.GetValue("testSource") ?? string.Empty;
        var displayName = message.GetValue("displayName") ?? string.Empty; 
        var resultDisplayName = message.GetValue("resultDisplayName") ?? string.Empty;
        var codeFilePath = message.GetValue("codeFilePath") ?? string.Empty;
        var fullyQualifiedName = message.GetValue("fullyQualifiedName") ?? string.Empty;
        var (flowId, suiteName, testName) = key;
        var result = new TestResult(state, testName)
            .WithSuiteName(suiteName)
            .WithFlowId(flowId)
            .WithSource(testSource)
            .WithDisplayName(displayName)
            .WithResultDisplayName(resultDisplayName)
            .WithCodeFilePath(codeFilePath)
            .WithFullyQualifiedName(fullyQualifiedName);
        
        if (Guid.TryParse(message.GetValue("id"), out var id))
        {
            result = result.WithId(id);
        }

        if (Uri.TryCreate(message.GetValue("executorUri"), UriKind.RelativeOrAbsolute, out var executorUri))
        {
            result = result.WithExecutorUri(executorUri);
        }

        if (int.TryParse(message.GetValue("lineNumber"), out var lineNumber))
        {
            result = result.WithLineNumber(lineNumber);
        }
        
        return result;
    }

    internal static TestKey CreateKey(IServiceMessage message)
    {
        var flowId = message.GetValue("flowId") ?? string.Empty;
        var suiteName = message.GetValue("suiteName") ?? string.Empty; 
        var name = message.GetValue("name") ?? string.Empty;
        return new TestKey(flowId, suiteName, name);
    }

    private static bool TryGetTestState(string? name, out TestState state)
    {
        switch(name?.ToLowerInvariant()) 
        {
            case "testfinished":
                state = TestState.Finished;
                return true;
                
            case "testignored":
                state = TestState.Ignored;
                return true;
            
            case "testfailed":
                state = TestState.Failed;
                return true;
        };

        state = default;
        return false;
    }
    
    // ReSharper disable once NotAccessedPositionalProperty.Local
    internal readonly record struct TestKey(string FlowId, string SuiteName, string TestName);
}