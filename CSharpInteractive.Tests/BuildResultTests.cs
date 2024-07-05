namespace CSharpInteractive.Tests;

using Core;
using HostApi;
using BuildResult = Core.BuildResult;

public class BuildResultTests
{
    [Theory]
    [InlineData("Abc", 0, 2, 3, 2, 3, 4, "Abc completed with exit code 0, with 2 errors, 3 warnings and 9 finished tests: 2 failed, 3 ignored, 4 passed.")]
    [InlineData("Abc", null, 2, 3, 2, 3, 4, "Abc not completed with 2 errors, 3 warnings and 9 finished tests: 2 failed, 3 ignored, 4 passed.")]
    [InlineData("Abc", 1, 1, 1, 1, 1, 1, "Abc completed with exit code 1, with 1 error, 1 warning and 3 finished tests: 1 failed, 1 ignored, 1 passed.")]
    [InlineData("Abc", 2, 0, 3, 2, 3, 4, "Abc completed with exit code 2, with 3 warnings and 9 finished tests: 2 failed, 3 ignored, 4 passed.")]
    [InlineData("Abc", 3, 2, 0, 2, 3, 4, "Abc completed with exit code 3, with 2 errors and 9 finished tests: 2 failed, 3 ignored, 4 passed.")]
    [InlineData("Abc", 4, 0, 0, 2, 3, 4, "Abc completed with exit code 4, with 9 finished tests: 2 failed, 3 ignored, 4 passed.")]
    [InlineData("Abc", 5, 0, 0, 1, 0, 0, "Abc completed with exit code 5, with 1 finished test: 1 failed.")]
    [InlineData("", 6, 0, 0, 1, 0, 0, "Build completed with exit code 6, with 1 finished test: 1 failed.")]
    [InlineData("", null, 0, 0, 0, 0, 0, "Build not completed.")]
    [InlineData("Abc", null, 0, 0, 0, 0, 0, "Abc not completed.")]
    public void ShouldSupportToString(string name, int? exitCode, int errors, int warnings, int failedTests, int ignoredTests, int passedTests, string expected)
    {
        // Given
        var startInfo = new Mock<IStartInfo>();
        var startInfoDescription = new Mock<IStartInfoDescription>();
        startInfoDescription.Setup(i => i.GetDescription(startInfo.Object, default)).Returns(name);
        var result = new BuildResult(startInfo.Object, startInfoDescription.Object)
            .WithExitCode(exitCode)
            .WithErrors(Enumerable.Repeat(new BuildMessage(BuildMessageState.StdError), errors).ToArray())
            .WithWarnings(Enumerable.Repeat(new BuildMessage(BuildMessageState.Warning), warnings).ToArray())
            .WithTests(
                GetTests(TestState.Failed, failedTests)
                    .Concat(GetTests(TestState.Ignored, ignoredTests))
                    .Concat(GetTests(TestState.Finished, passedTests))
                    .ToArray());

        // When
        var actual = result.ToString();

        // Then
        actual.ShouldBe(expected);
    }

    private static IEnumerable<TestResult> GetTests(TestState state, int count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new TestResult(state, $"Test_{state}_{i}").WithSuiteName("aaa");
        }
    }
}