using System.Text.RegularExpressions;

namespace StringCalculator;

public class StringCalculator
{
    public int Add(string expression)
    {
        const int upperLimit = 1000;
        if (string.IsNullOrEmpty(expression))
        {
            return 0;
        }

        var numbersAsInts = ParseExpression(expression)
            .Where(x => x <= upperLimit);

        ThrowExceptionIfAnyNegativeNumbers(numbersAsInts);

        return numbersAsInts.Sum();
    }
    
    private IEnumerable<int> ParseExpression(string numbers)
    {
        var numbersToProcess = numbers;
        var separators = new List<string> {",", "\\n", "\\"};

        var partWithSeparators = numbers.Split("\\n").First();
        
        if(partWithSeparators.StartsWith("//"))
        {
            separators.AddRange(GetCustomSeparators(partWithSeparators));
            numbersToProcess = numbers.Substring(partWithSeparators.Length + 2);
        }
        
        return numbersToProcess.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x));
    }

    private IEnumerable<string> GetCustomSeparators(string input)
    {
        const int minimumCustomSeparatorLength = 3;
        
        var separators = Array.Empty<string>();

        if (input.Length == minimumCustomSeparatorLength)
        {
            separators = new[] { input[minimumCustomSeparatorLength - 1].ToString() };
        }
        else
        {
            separators = input.Split(new[] { ']', '[' }, StringSplitOptions.RemoveEmptyEntries);
        }

        return separators;
    }
    private void ThrowExceptionIfAnyNegativeNumbers(IEnumerable<int> numbers)
    {
        var negativeNumbers = numbers.Where(x => x < 0);
        
        if (negativeNumbers.Any())
        {
            throw new ArgumentException($"Negative numbers are not allowed ({string.Join(", ", negativeNumbers)})");
        }
    }
}