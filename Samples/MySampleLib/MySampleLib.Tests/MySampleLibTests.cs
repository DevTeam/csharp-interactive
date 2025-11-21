namespace MySampleLib.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

public class CalculatorTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(3, 2, 5)]
    [InlineData(3, -2, 1)]
    public void ShouldAdd(int op1, int op2, int expected)
    {
        // Given
        var calculator = new Calculator();

        // When
        var actual = calculator.Add(op1, op2);

        // Then
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(3, 2, 1)]
    [InlineData(3, -2, 5)]
    public void ShouldSub(int op1, int op2, int expected)
    {
        // Given
        var calculator = new Calculator();

        // When
        var actual = calculator.Sub(op1, op2);

        // Then
        Assert.Equal(expected, actual);
    }

    [Fact(Skip = "")]
    [SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped")]
    public void ShouldDiv()
    { }

    [Fact]
    public void ShouldMul()
    {
        throw new Exception("Some exception");
    }
}