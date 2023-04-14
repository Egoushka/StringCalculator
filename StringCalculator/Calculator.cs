namespace StringCalculator;

public class Calculator
{
    public virtual int Add(string expression)
    {
        var upperLimit = 1000;

        if (string.IsNullOrEmpty(expression))
        {
            return default;
        }

        var parsedExpressionNumbers = ParseExpression(expression);
        
        parsedExpressionNumbers = FilterNumbersAboveUpperLimit(parsedExpressionNumbers, upperLimit);

        ThrowExceptionIfAnyNegativeNumbers(parsedExpressionNumbers);

        return parsedExpressionNumbers.Sum();
    }

    private IEnumerable<int> ParseExpression(string expression)
    {
        var separators = new List<string> { ",", "\\n", "\\" };
        var numbersToProcess = expression;
        var partWithSeparators = expression.Split("\\n").First();

        if (partWithSeparators.StartsWith("//"))
        {
            var customSeparators = GetCustomSeparators(partWithSeparators);
            separators = customSeparators.ToList();

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
            var separator = input.Substring(2, 1);
            separators.Add(separator);
        }
        else
        {
            var lengthOfCustomSeparators = input.Length - minimumCustomSeparatorLength - 1;
            
            var customSeparators = 
                input.Substring(minimumCustomSeparatorLength, lengthOfCustomSeparators)
                     .Split("][");
            
            separators.AddRange(customSeparators);
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
    
    private IEnumerable<int> FilterNumbersAboveUpperLimit(IEnumerable<int> numbers, int upperLimit = 1000)
    {
        return numbers.Where(x => x <= upperLimit);
    }
}