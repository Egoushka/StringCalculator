namespace StringCalculator;

public class Calculator
{
    public virtual int Add(string expression)
    {
        var upperLimit = 1000;
        if (string.IsNullOrEmpty(expression))
        {
            return 0;
        }

        var parsedExpressionNumbers = ParseExpression(expression)
            .Where(x => x <= upperLimit);

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
            var separator = input.Substring(2, 1);
            separators.Add(separator);
        }
        else
        {
            var separatorsInBrackets = ExtractSeparatorsInBrackets(input);
            separators.AddRange(separatorsInBrackets);
        }

        return separators;
    }

    private IEnumerable<string> ExtractSeparatorsInBrackets(string input)
    {
        var result = new List<string>();

        for (var index = 0; index < input.Length; index++)
        {
            if (input[index] != '[') continue;

            var length = FindLenghtOfSeparatorInBrackets(input, index);
            var separator = input.Substring(index + 1, length - 1);

            index += length;
            result.Add(separator);
        }

        return result;
    }

    private int FindLenghtOfSeparatorInBrackets(string input, int startIndex)
    {
        var indexOfBrackets = input.IndexOf("][", startIndex, StringComparison.Ordinal);
        
        if (indexOfBrackets > 0)
        {
            return indexOfBrackets - startIndex;
        }
        return input.Length - startIndex - 1;
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