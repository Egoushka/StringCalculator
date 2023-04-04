using StringCalculator;

var calculator = new Calculator();

while (true)
{
    Console.WriteLine("Enter comma separated numbers(enter to exit):");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        break;
    }
    var result = calculator.Add(input);
    Console.WriteLine($"Result: {result}");
}