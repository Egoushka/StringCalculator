namespace StringCalculator.Console;

public class ConsoleApp
{
    private readonly ConsoleWrapper _consoleWrapper;
    private readonly Calculator _calculator;

    public ConsoleApp(ConsoleWrapper consoleWrapper, Calculator calculator)
    {
        _consoleWrapper = consoleWrapper;
        _calculator = calculator;
    }

    public void Run()
    {
        _consoleWrapper.WriteLine("Enter comma separated numbers(enter to exit):");

        while (true)
        {
            var input = _consoleWrapper.ReadLine();
            
            if (string.IsNullOrEmpty(input))
            {
                break;
            }
            
            var result = _calculator.Add(input);
            
            _consoleWrapper.WriteLine($"Result: {result}");
            _consoleWrapper.WriteLine("you can enter other numbers (enter to exit)?");

        }
    }
}