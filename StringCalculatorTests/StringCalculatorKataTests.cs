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
    public void SumIsCorrect()
    {
        var result = _stringCalculator.Add("//;\n1;2");
        
        Assert.Equal(3, result);
    }
}