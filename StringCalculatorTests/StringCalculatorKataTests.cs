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
    public void SumOfEmptyStringIsZero()
    {
        var result = _stringCalculator.Add("");
        
        Assert.Equal(0, result);
    }
    [Fact]
    public void SumIsCorrectWithOneNumber()
    {
        var result = _stringCalculator.Add("1");
        
        Assert.Equal(1, result);
    }
    [Fact]
    public void SumIsCorrectWithOneNumberAndComma()
    {
        var result = _stringCalculator.Add("1,");
        
        Assert.Equal(1, result);
    }
    [Fact]
    public void SumIsCorrectWithDifferentSeparators()
    {
        var result = _stringCalculator.Add("//;\n1;2,3\n4/5\\6");
        
        Assert.Equal(21, result);
    }
    
}