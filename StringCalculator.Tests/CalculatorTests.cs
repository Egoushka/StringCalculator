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
    public void Add_WithDifferentSeparators_ShouldReturnSum()
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
    public void Add_NumbersAboveUpperLimit_ShouldSkipNumberAboveLimit()
    {
        var result = _calculator.Add("1,10000");
        
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void Add_NumbersWithCustomSeparator_ReturnsSum()
    {
        var result = _calculator.Add("//;\\n2;3");

        Assert.Equal(5, result);
    }
    
    [Fact]
    public void Add_NumbersWithLongCustomSeparator_ReturnsSum()
    {
        var result = _calculator.Add("//[***]\\n1***2***3");

        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_NumbersWithSeveralCustomSeparators_ReturnsSum()
    {
        var result = _calculator.Add("//[*][%]\\n1*2%3");
        
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void Add_CustomSeparatorAsNestedBrackets_ReturnSum()
    {
        var result = _calculator.Add("//[[[a][a]\\n1[[a2a");
        
        Assert.Equal(3, result);
    }
}