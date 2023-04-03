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
            var separator = input.Substring(2, 1);
            separators.Add(separator);
        }
        else
        {
            var separatorsInBrackets = GetSeparatorsInBrackets(input);
            separators.AddRange(separatorsInBrackets);
        }

        return separators;
    }

    private IEnumerable<string> GetSeparatorsInBrackets(string input)
    {
        var values = new List<string>();
        var stack = new Stack<int>();

        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (c == '[' && stack.Count == 0)
            {
                stack.Push(i);
            }
            else if (c == ']')
            {
                //if it's not the last character and the next character is not a '['
                if (i != input.Length - 1 && input[i + 1] != '[')
                {
                    continue;
                }

                if (stack.Count <= 0) continue;

                var startIndex = stack.Pop();
                var length = i - startIndex - 1;
                var value = input.Substring(startIndex + 1, length);

                values.Add(value);
            }
        }

        return values.ToArray();
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