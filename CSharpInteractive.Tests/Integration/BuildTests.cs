namespace CSharpInteractive.Tests.Integration;

[CollectionDefinition("Integration", DisableParallelization = true)]
[Trait("Integration", "True")]
public class BuildTests
{
    [Fact]
    public void ShouldRunCustomRestoreBuildTest()
    {
        // Given

        // When
        var result = TestTool.Run(
            "using HostApi;",
            "using System.Linq;",
            "var cmdRunner = GetService<ICommandLineRunner>();",
            "var buildRunner = GetService<IBuildRunner>();",
            "var exitCode = cmdRunner.Run(new DotNetCustom(\"new\", \"mstest\")).ExitCode;",
            "WriteLine(\"Custom=\" + exitCode);",
            "var result = buildRunner.Build(new DotNetRestore());",
            "WriteLine(\"Restore=\" + result.Tests.Count());",
            "result = new DotNetBuild().Build();",
            "WriteLine(\"Build=\" + result.Tests.Count());",
            "result = buildRunner.Build(new DotNetTest());",
            "WriteLine(\"Tests=\" + result.Tests.Count());"
        );

        // Then
        result.ExitCode.ShouldBe(0, result.ToString());
        result.StdErr.ShouldBeEmpty(result.ToString());
        result.StdOut.Contains("Custom=0").ShouldBeTrue();
        result.StdOut.Contains("Restore=0").ShouldBeTrue();
        result.StdOut.Contains("Build=0").ShouldBeTrue();
        result.StdOut.Contains("Tests=1").ShouldBeTrue();
    }
}