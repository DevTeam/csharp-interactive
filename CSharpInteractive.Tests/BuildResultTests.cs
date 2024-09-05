namespace CSharpInteractive.Tests;

using Core;
using HostApi;

public class BuildResultTests
{
    private static readonly Output Output = new(Mock.Of<IStartInfo>(), false, "", 99);
    private readonly Mock<ICommandLineResult> _commandLineResult = new();
    
    [Theory]
    [InlineData(2, 3, 2, 3, 4, " with 2 errors, 3 warnings and 9 finished tests: 2 failed, 3 ignored, 4 passed")]
    [InlineData( 1, 1, 1, 1, 1, " with 1 error, 1 warning and 3 finished tests: 1 failed, 1 ignored, 1 passed")]
    [InlineData( 0, 3, 2, 3, 4, " with 3 warnings and 9 finished tests: 2 failed, 3 ignored, 4 passed")]
    [InlineData( 2, 0, 2, 3, 4, " with 2 errors and 9 finished tests: 2 failed, 3 ignored, 4 passed")]
    [InlineData( 0, 0, 2, 3, 4, " with 9 finished tests: 2 failed, 3 ignored, 4 passed")]
    [InlineData( 0, 0, 1, 0, 0, " with 1 finished test: 1 failed")]
    [InlineData( 0, 0, 0, 0, 0, "")]
    public void ShouldSupportToString(int errors, int warnings, int failedTests, int ignoredTests, int passedTests, string expected)
    {
        // Given
        var result = new BuildResult(_commandLineResult.Object)
        {
            Errors = Enumerable.Repeat(new BuildMessage(Output, BuildMessageState.StdError), errors).ToArray(),
            Warnings = Enumerable.Repeat(new BuildMessage(Output, BuildMessageState.Warning), warnings).ToArray(),
            Tests = GetTests(TestState.Failed, failedTests)
                .Concat(GetTests(TestState.Ignored, ignoredTests))
                .Concat(GetTests(TestState.Finished, passedTests))
                .ToArray()
        };
        
        // When
        var actual = result.ToString();

        // Then
        actual.ShouldBe($"{_commandLineResult.Object}" + expected);
    }

    private static IEnumerable<TestResult> GetTests(TestState state, int count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new TestResult(state, $"Test_{state}_{i}").WithSuiteName("aaa");
        }
    }
}