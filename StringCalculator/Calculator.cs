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
            var separatorsInBrackets = ExtractValuesInBrackets(input);
            separators.AddRange(separatorsInBrackets);
        }

        return separators;
    }

    private IEnumerable<string> ExtractValuesInBrackets(string input)
    {
        var result = new List<string>();
        
        for (var index = 0; index < input.Length; index++)
        {
            if (input[index] != '[') continue;

            var length = FindLenghtOfValueInBrackets(input, index);
            var value = ExtractValueInBrackets(input, index, length);
            
            index += length;
            result.Add(value);
        }

        return result;
    }
    private int FindLenghtOfValueInBrackets(string input, int startIndex)
    {
        var length = 0;
        for (var index = startIndex; index < input.Length - 1; index++, length++)
        {
            var character = input[index];
            
            if(character == '[') continue;
            
            if (input[index + 1] == '[')
            {
                break;
            }
        }
        return length;
    }
    private string ExtractValueInBrackets(string input, int startIndex, int length)
    {
        var value = input.Substring(startIndex + 1, length - 1);
        return value;
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