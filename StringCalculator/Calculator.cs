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
            var separatorsInBrackets = ExtractValuesInBrackets("");
            separators.AddRange(separatorsInBrackets);
        }

        return separators;
    }

    private IEnumerable<string> ExtractValuesInBrackets(string input)
    {
        var stack = new Stack<int>();

        for (var index = 0; index < input.Length; index++)
        {
            var c = input[index];
            if (c == '[' && stack.Count == 0)
            {
                stack.Push(index);
            }
            else if (c == ']' && stack.Count > 0 && (index == input.Length - 1 || input[index + 1] == '['))
            {
                var startIndex = stack.Pop();
                var length = index - startIndex - 1;
                var value = input.Substring(startIndex + 1, length);
                yield return value;
            }
        }
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