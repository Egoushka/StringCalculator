namespace StringCalculator;

public class StringCalculatorKata
{
    private readonly char[] _separators = { ',', ' ', '\n', ';', '\\', '/' };
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numbersArray = numbers.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        var numbersAsInts = numbersArray.Select(int.Parse).ToArray();
        
        var sum = numbersAsInts.Sum();

        return sum;
    }
}