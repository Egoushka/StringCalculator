namespace StringCalculator;

public class Calculator
{
    public int Add(string expression)
    {
        var upperLimit = 1000;
        if (string.IsNullOrEmpty(expression))
        {
            return 0;
        }

        var numbersAsInts = ParseExpression(expression)
            .Where(x => x <= upperLimit);

        ThrowExceptionIfAnyNegativeNumbers(numbersAsInts);

        return numbersAsInts.Sum();
    }
    
    private IEnumerable<int> ParseExpression(string expression)
    {
        var separators = new List<string> {",", "\\n", "\\"};
        var numbersToProcess = expression;
        var partWithSeparators = expression.Split("\\n").First();
        
        if(partWithSeparators.StartsWith("//"))
        {
            var customSeparators = GetCustomSeparators(partWithSeparators);
            separators.AddRange(customSeparators);
            
            var numbersStartIndex = partWithSeparators.Length + 2;
            numbersToProcess = expression.Substring(numbersStartIndex);
        }
        
        return numbersToProcess.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x));
    }

    private IEnumerable<string> GetCustomSeparators(string input)
    {
        var minimumCustomSeparatorLength = 3;

        var separators = new List<string>();

        if (input.Length == minimumCustomSeparatorLength)
        {
            separators = new[] { input[minimumCustomSeparatorLength - 1].ToString() };
        }
        else
        {
            separators = input.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
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