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
        
        var negativeNumbers = numbersAsInts.Where(x => x < 0).ToArray();
        if (negativeNumbers.Any())
        {
            var negativeNumbersString = string.Join(", ", negativeNumbers);
            throw new ArgumentException($"Negative numbers are not allowed ({negativeNumbersString})");
        }

        var sum = numbersAsInts.Sum();

        return sum;
    }
}