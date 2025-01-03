namespace CSharpInteractive.Tests;

public class SettingCommandRunnerTests
{
    [Fact]
    public void ShouldSetValue()
    {
        // Given
        var settingSetter = new Mock<ISettingSetter<VerbosityLevel>>();
        var runner = new SettingCommandRunner<VerbosityLevel>(Mock.Of<ILog<SettingCommandRunner<VerbosityLevel>>>(), settingSetter.Object);

        // When
        runner.TryRun(new SettingCommand<VerbosityLevel>(VerbosityLevel.Diagnostic));

        // Then
        settingSetter.Verify(i => i.SetSetting(VerbosityLevel.Diagnostic));
    }

    [Fact]
    public void ShouldNotSetValueWhenOtherCommand()
    {
        // Given
        var settingSetter = new Mock<ISettingSetter<VerbosityLevel>>();
        var runner = new SettingCommandRunner<VerbosityLevel>(Mock.Of<ILog<SettingCommandRunner<VerbosityLevel>>>(), settingSetter.Object);

        // When
        runner.TryRun(HelpCommand.Shared);

        // Then
        settingSetter.Verify(i => i.SetSetting(It.IsAny<VerbosityLevel>()), Times.Never);
    }
}