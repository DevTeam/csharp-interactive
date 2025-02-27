namespace CSharpInteractive.Tests;

using System;
using Core;
using Environment = System.Environment;

public class ProcessOutputWriterTests
{
    private readonly Mock<IConsole> _console = new();

    [Fact]
    public void ShouldWriteStdOut()
    {
        // Given
        var writer = CreateInstance();

        // When
        writer.Write(new Output(Mock.Of<IStartInfo>(), false, "Out", 11));

        // Then
        _console.Verify(i => i.WriteToOut(It.Is<(ConsoleColor? color, string output)[]>(items => items.Length == 2 && items[0].output == "Out" && items[1].output == Environment.NewLine)));
    }

    [Fact]
    public void ShouldWriteStdErr()
    {
        // Given
        var writer = CreateInstance();

        // When
        writer.Write(new Output(Mock.Of<IStartInfo>(), true, "Err", 11));

        // Then
        _console.Verify(i => i.WriteToErr(new ValueTuple<ConsoleColor?, string>(null, "Err"), new ValueTuple<ConsoleColor?, string>(null, Environment.NewLine)));
    }

    private ProcessOutputWriter CreateInstance() =>
        new(_console.Object);
}