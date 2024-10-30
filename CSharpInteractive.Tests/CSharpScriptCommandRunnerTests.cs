namespace CSharpInteractive.Tests;

public class CSharpScriptCommandRunnerTests
{
    private readonly Mock<ICSharpScriptRunner> _csharpScriptRunner = new();

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldRunScriptCommand(bool result)
    {
        // Given
        var command = new ScriptCommand("abc", "code");
        var commandRunner = CreateInstance();
        // ReSharper disable once RedundantArgumentDefaultValue
        _csharpScriptRunner.Setup(i => i.Run(command, "code")).Returns(new CommandResult(command, result, default));

        // When
        var actualResult = commandRunner.TryRun(command);

        // Then
        _csharpScriptRunner.Verify(i => i.Run(command, "code"));
        actualResult.Command.ShouldBe(command);
        actualResult.Success.ShouldBe(result);
    }

    [Fact]
    public void ShouldRunResetCommand()
    {
        // Given
        var command = ResetCommand.Shared;
        var commandRunner = CreateInstance();

        // When
        var actualResult = commandRunner.TryRun(command);

        // Then
        _csharpScriptRunner.Verify(i => i.Reset());
        actualResult.Command.ShouldBe(command);
        actualResult.Success.ShouldBe(true);
    }

    [Fact]
    public void ShouldSkipUnhandledCommand()
    {
        // Given
        var command = new CodeCommand();
        var commandRunner = CreateInstance();

        // When
        var actualResult = commandRunner.TryRun(command);

        // Then
        actualResult.Command.ShouldBe(command);
        actualResult.Success.ShouldBe(default);
    }

    private CSharpScriptCommandRunner CreateInstance() =>
        new(_csharpScriptRunner.Object);
}