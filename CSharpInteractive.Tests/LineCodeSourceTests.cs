namespace CSharpInteractive.Tests;

public class LineCodeSourceTests
{
    [Fact]
    public void ShouldIntiLine()
    {
        // Given

        // When
        var source = CreateInstance("Abc");

        // Then
        source.Name.ShouldBe("Abc");
        source.ToArray().ShouldBe(["Abc"]);
    }

    private static LineCodeSource CreateInstance(string line) =>
        new(line);
}