using Xunit;

namespace StringCalculatorTests;

public class StringCalculatorKataTests
{
    private readonly StringCalculator.StringCalculator _stringCalculator;

    public StringCalculatorKataTests()
    {
        _stringCalculator = new StringCalculator.StringCalculator();
    }

    [Fact]
    public void Add_EmptyString_ReturnsZero()
    {
        var result = _stringCalculator.Add("");
        
        Assert.Equal(0, result);
    }
  
    [Fact]
    public void Add_SingleNumberAndSeparator_ReturnsNumber()
    {
        var result = _stringCalculator.Add("1,");
        
        Assert.Equal(1, result);
    }

    [Fact]
    public void Add_WithDifferentSeparators_ShouldReturnNumber()
    {
        var result = _stringCalculator.Add("//;\\n1;\\n2,\\3");
        
        Assert.Equal(6, result);
    }
    
    [Fact]
    public void Add_NumbersWithNegativeNumber_ThrowsException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _stringCalculator.Add("-1,-3"));
        
        Assert.Equal("Negative numbers are not allowed (-1, -3)", exception.Message);
    }
    
    [Fact]
    public void Add_NumbersAboveUpperLimit_AreIgnored()
    {
        var result = _stringCalculator.Add("1,10000");
        
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void Add_NumbersWithCustomSeparator_ReturnsNumber()
    {
        var result = _stringCalculator.Add("//;\\n2;3");

        Assert.Equal(5, result);
    }
    
    [Fact]
    public void Add_NumbersWithLongCustomSeparator_ReturnsNumber()
    {
        var result = _stringCalculator.Add("//[***]\\n1***2***3");

        Assert.Equal(6, result);
    }

    [Fact]
    public void Add_NumbersWithSeveralCustomSeparators_ReturnsNumber()
    {
        var result = _stringCalculator.Add("//[*][%]\\n1*2%3");
        
        Assert.Equal(6, result);
    }

}