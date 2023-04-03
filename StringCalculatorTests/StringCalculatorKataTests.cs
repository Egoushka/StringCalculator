using StringCalculator;

namespace StringCalculatorTests;

public class StringCalculatorKataTests
{
    private readonly StringCalculatorKata _stringCalculator;

    public StringCalculatorKataTests()
    {
        _stringCalculator = new StringCalculatorKata();
    }

    [Fact]
    public void Add_OfEmptyString_IsZero()
    {
        var result = _stringCalculator.Add("");
        
        Assert.Equal(0, result);
    }
  
    [Fact]
    public void Add_WithOneNumberAndComma_IsCorrect()
    {
        var result = _stringCalculator.Add("1,");
        
        Assert.Equal(1, result);
    }
    [Fact]
    public void Add_WithDifferentSeparators_IsCorrect()
    {
        var result = _stringCalculator.Add("//;\n1;\n2,\\3");
        
        Assert.Equal(6, result);
    }
    [Fact]
    public void Add_WithNegativeNumbers_ThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() => _stringCalculator.Add("-1,-3"));
        
        Assert.Equal("Negative numbers are not allowed (-1, -3)", exception.Message);
    }
    [Fact]
    public void Add_WithNumberBiggerThanOneThousands_IsCorrect()
    {
        var result = _stringCalculator.Add("1,10000");
        
        Assert.Equal(1, result);
    }
}