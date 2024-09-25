namespace CSharpInteractive.Tests;

using JetBrains.TeamCity.ServiceMessages.Write;

public class BuildContextTests
{
    private readonly Mock<ICommandLineResult> _commandLineResult = new();
    private readonly Mock<IStartInfo> _startInfo = new();

    [Fact]
    public void ShouldProcessStdOutput()
    {
        // Given
        var result = CreateInstance();
        var output = new Output(Mock.Of<IStartInfo>(), false, "Abc", 33);
        var msg = new BuildMessage(output, BuildMessageState.StdOut, default, "Abc");

        // When
        var messages = result.ProcessOutput(output);
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        messages.ShouldBe([msg]);
        buildResult.Errors.ShouldNotContain(msg);
    }

    [Fact]
    public void ShouldProcessErrOutput()
    {
        // Given
        var result = CreateInstance();
        var output = new Output(Mock.Of<IStartInfo>(), true, "Abc", 33);
        var msg = new BuildMessage(output, BuildMessageState.StdError, default, "Abc");

        // When
        var messages = result.ProcessOutput(output);
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        messages.ShouldBe([msg]);
        buildResult.Errors.ShouldContain(msg);
    }

    [Fact]
    public void ShouldProcessTestFinished()
    {
        // Given
        var result = CreateInstance();
        var testStdout = new ServiceMessage("testStdout")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"out", "Some output"}
        };

        var testStderr = new ServiceMessage("testStderr")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"out", "Some error"}
        };

        var testFinished = new ServiceMessage("testFinished")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"duration", "123"},
            {"displayName", "Test1"},
            {"fullyQualifiedName", "Full Test1"},
            {"executorUri", "executor://mstestadapter/v2"},
            {"lineNumber", "23"}
        };

        var stdOutput = new Output(_startInfo.Object, false, "Some output", 11);
        var errOutput = new Output(_startInfo.Object, true, "Some error", 11);
        var finishedOutput = new Output(Mock.Of<IStartInfo>(), false, "Abc", 33);

        // When
        result.ProcessMessage(stdOutput, testStdout).ToArray().ShouldBe([new BuildMessage(stdOutput, BuildMessageState.StdOut).WithText("Some output")]);
        result.ProcessMessage(errOutput, testStderr).ToArray().ShouldBe([new BuildMessage(errOutput, BuildMessageState.StdError).WithText("Some error")]);
        result.ProcessMessage(finishedOutput, testFinished).ShouldBeEmpty();
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        buildResult.Tests.Count.ShouldBe(1);
        var test = buildResult.Tests[0];
        test.SuiteName.ShouldBe("Assembly1");
        test.DisplayName.ShouldBe("Test1");
        test.FullyQualifiedName.ShouldBe("Full Test1");
        test.Duration.ShouldBe(TimeSpan.FromMilliseconds(123));
        test.State.ShouldBe(TestState.Finished);
        test.Message.ShouldBeEmpty();
        test.Details.ShouldBeEmpty();
        test.Output.ShouldBe([stdOutput, errOutput]);
        test.ExecutorUri.ShouldBe(new Uri("executor://mstestadapter/v2", UriKind.RelativeOrAbsolute));
        test.LineNumber.ShouldBe(23);
    }

    [Fact]
    public void ShouldProcessTestFailed()
    {
        // Given
        var result = CreateInstance();
        var testStdout = new ServiceMessage("testStdout")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"out", "Some output"}
        };

        var testFailed = new ServiceMessage("testFailed")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"message", "Some message"},
            {"details", "Error details"},
            {"displayName", "Test1"},
            {"resultDisplayName", "Test1 Abc"},
            {"fullyQualifiedName", "Full Test1"}
        };

        var output = new Output(_startInfo.Object, false, string.Empty, 11);

        // When
        result.ProcessMessage(output, testStdout).ToArray().ShouldBe([new BuildMessage(output, BuildMessageState.StdOut).WithText("Some output")]);
        result.ProcessMessage(output, testFailed).ShouldBeEmpty();
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        buildResult.Tests.Count.ShouldBe(1);
        var test = buildResult.Tests[0];
        test.SuiteName.ShouldBe("Assembly1");
        test.DisplayName.ShouldBe("Test1");
        test.ResultDisplayName.ShouldBe("Test1 Abc");
        test.FullyQualifiedName.ShouldBe("Full Test1");
        test.Duration.ShouldBe(TimeSpan.Zero);
        test.State.ShouldBe(TestState.Failed);
        test.Message.ShouldBe("Some message");
        test.Details.ShouldBe("Error details");
        test.Output.ShouldBe([new Output(_startInfo.Object, false, "Some output", 11)]);
    }

    [Fact]
    public void ShouldProcessTestIgnored()
    {
        // Given
        var result = CreateInstance();
        var testStdout = new ServiceMessage("testStdout")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"out", "Some output"}
        };

        var testIgnored = new ServiceMessage("testIgnored")
        {
            {"suiteName", "Assembly1"},
            {"name", "Test1"},
            {"flowId", "11"},
            {"message", "Some message"},
            {"displayName", "Test1"},
            {"fullyQualifiedName", "Full Test1"}
        };

        var output = new Output(_startInfo.Object, false, string.Empty, 11);

        // When
        result.ProcessMessage(output, testStdout).ToArray().ShouldBe([new BuildMessage(output, BuildMessageState.StdOut).WithText("Some output")]);
        result.ProcessMessage(output, testIgnored).ShouldBeEmpty();
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        buildResult.Tests.Count.ShouldBe(1);
        var test = buildResult.Tests[0];
        test.SuiteName.ShouldBe("Assembly1");
        test.DisplayName.ShouldBe("Test1");
        test.FullyQualifiedName.ShouldBe("Full Test1");
        test.Duration.ShouldBe(TimeSpan.Zero);
        test.State.ShouldBe(TestState.Ignored);
        test.Message.ShouldBe("Some message");
        test.Details.ShouldBeEmpty();
        test.Output.ShouldBe([new Output(_startInfo.Object, false, "Some output", 11)]);
    }

    [Theory]
    [InlineData("Normal", BuildMessageState.StdOut)]
    [InlineData("normal", BuildMessageState.StdOut)]
    [InlineData("NORMAL", BuildMessageState.StdOut)]
    [InlineData("Warning", BuildMessageState.Warning)]
    [InlineData("warning", BuildMessageState.Warning)]
    [InlineData("WARNING", BuildMessageState.Warning)]
    [InlineData("Failure", BuildMessageState.Failure)]
    [InlineData("failure", BuildMessageState.Failure)]
    [InlineData("FAILURE", BuildMessageState.Failure)]
    [InlineData("Error", BuildMessageState.StdError)]
    [InlineData("error", BuildMessageState.StdError)]
    [InlineData("ERROR", BuildMessageState.StdError)]
    public void ShouldProcessMessage(string status, BuildMessageState state)
    {
        // Given
        var result = CreateInstance();
        var message = new ServiceMessage("message")
        {
            {"text", "some text"},
            {"status", status},
            {"errorDetails", "error details"},
            {"code", "xUnit1026"},
            {"file", @"C:\Projects\_temp\xu\TestProject1\TestProject1.cs"},
            {"subcategory", "Abc"},
            {"projectFile", @"C:\Projects\_temp\xu\TestProject1\TestProject1.csproj"},
            {"senderName", "Csc"},
            {"columnNumber", "1"},
            {"endColumnNumber", "2"},
            {"lineNumber", "3"},
            {"endLineNumber", "4"},
            {"importance", "High"}
        };

        var output = new Output(_startInfo.Object, false, string.Empty, 11);
        var buildMessage = new BuildMessage(
            output,
            state,
            default,
            "some text",
            "error details",
            "xUnit1026",
            @"C:\Projects\_temp\xu\TestProject1\TestProject1.cs",
            "Abc",
            @"C:\Projects\_temp\xu\TestProject1\TestProject1.csproj",
            "Csc",
            1,
            2,
            3,
            4,
            DotNetMessageImportance.High);

        // When
        result.ProcessMessage(output, message).ShouldBe([buildMessage]);
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        buildResult.Tests.Count.ShouldBe(0);
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (state)
        {
            case BuildMessageState.ServiceMessage:
            case BuildMessageState.StdOut:
                buildResult.Errors.ShouldBe(Array.Empty<BuildMessage>());
                break;

            case BuildMessageState.Warning:
                buildResult.Warnings.ShouldBe([buildMessage]);
                break;

            case BuildMessageState.Failure:
            case BuildMessageState.StdError:
            case BuildMessageState.BuildProblem:
                buildResult.Errors.ShouldBe([buildMessage]);
                break;
        }
    }

    [Fact]
    public void ShouldBuildProblem()
    {
        // Given
        var result = CreateInstance();
        var buildProblem = new ServiceMessage("buildProblem")
        {
            {"description", "Problem description"},
            {"errorDetails", "error details"},
            {"code", "xUnit1026"},
            {"file", @"C:\Projects\_temp\xu\TestProject1\TestProject1.cs"},
            {"subcategory", "Abc"},
            {"projectFile", @"C:\Projects\_temp\xu\TestProject1\TestProject1.csproj"},
            {"senderName", "Csc"},
            {"columnNumber", "1"},
            {"endColumnNumber", "2"},
            {"lineNumber", "3"},
            {"endLineNumber", "4"},
            {"importance", "High"}
        };

        var output = new Output(_startInfo.Object, false, string.Empty, 11);
        var buildMessage = new BuildMessage(
            output,
            BuildMessageState.BuildProblem,
            default,
            "Problem description",
            "error details",
            "xUnit1026",
            @"C:\Projects\_temp\xu\TestProject1\TestProject1.cs",
            "Abc",
            @"C:\Projects\_temp\xu\TestProject1\TestProject1.csproj",
            "Csc",
            1,
            2,
            3,
            4,
            DotNetMessageImportance.High);

        // When
        result.ProcessMessage(output, buildProblem).ShouldBe([buildMessage]);
        var buildResult = result.Create(_commandLineResult.Object);

        // Then
        buildResult.Tests.Count.ShouldBe(0);
        buildResult.Errors.ShouldBe([buildMessage]);
    }

    private static BuildContext CreateInstance() => new();
}