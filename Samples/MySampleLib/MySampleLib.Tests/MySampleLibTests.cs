using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MySampleLib.Tests;

[TestClass]
public class CalculatorTests
{
    [TestMethod]
    public void ShouldAdd()
    {
        // Given
        var calculator = new Calculator();

        // When
        var result = calculator.Add(1, 2);

        // Then
        Assert.AreEqual(3, result);
    }
    
    [TestMethod]
    public void ShouldSub()
    {
        // Given
        var calculator = new Calculator();

        // When
        var result = calculator.Sub(2, 1);

        // Then
        Assert.AreEqual(1, result);
    }
}