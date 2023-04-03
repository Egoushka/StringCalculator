namespace StringCalculator;

public class StringCalculatorKata
{
    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var numbersAsInts = ParseNumbers(numbers);
        numbersAsInts = RemoveNumbersBiggerThanOneThousands(numbersAsInts);

        if (IsAnyNegativeNumbers(numbersAsInts, out var negativeNumbers))
        {
            throw new ArgumentException($"Negative numbers are not allowed ({string.Join(", ", negativeNumbers)})");
        }

        return numbersAsInts.Sum();
    }

    private int[] ParseNumbers(string numbers)
    {
        var separators = new[] { ',', ' ', '\n', ';', '\\', '/' , '[', ']', '*'};
        
        return numbers.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    }
    private bool IsAnyNegativeNumbers(IEnumerable<int> numbers, out IEnumerable<int> negativeNumbers)
    {
        negativeNumbers = numbers.Where(x => x < 0);
        
        return negativeNumbers.Any();
    }
    private int[] RemoveNumbersBiggerThanOneThousands(IEnumerable<int> numbers)
    {
        return numbers.Where(x => x < 1000).ToArray();
    }
    
}