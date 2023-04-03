using StringCalculator;
using Xunit;

namespace StringCalculatorTests;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        var result = _calculator.Add("");
        
        Assert.Equal(0, result);
    }
  
    [Fact]
    public void Add_SingleNumberAndSeparator_ReturnsNumber()
    {
        var result = _calculator.Add("1,");
        
        Assert.Equal(1, result);
    }

    [Fact]
    public void Add_WithDifferentSeparators_ShouldReturnNumber()
    {
        var result = _calculator.Add("//;\\n1;\\n2,\\3");
        
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void Add_NumbersWithNegativeNumber_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _calculator.Add("-1,-3"));
        
        Assert.Equal("Negative numbers are not allowed (-1, -3)", exception.Message);
    }
    
    [Fact]
    public void Add_NumbersAboveUpperLimit_AreIgnored()
    {
        var result = _calculator.Add("1,10000");
        
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void Add_NumbersWithCustomSeparator_ReturnsNumber()
    {
        var result = _calculator.Add("//;\\n2;3");

        Assert.Equal(5, result);
    }
    
    [Fact]
    public void Add_NumbersWithLongCustomSeparator_ReturnsNumber()
    {
        var result = _calculator.Add("//[***]\\n1***2***3");

        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_NumbersWithSeveralCustomSeparators_ReturnsNumber()
    {
        var result = _calculator.Add("//[*][%]\\n1*2%3");
        
        Assert.Equal(6, result);
    }

}