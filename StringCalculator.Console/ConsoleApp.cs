namespace StringCalculator.Console;

public class ConsoleApp
{
    private readonly IConsoleWrapper _consoleWrapper;

    public ConsoleApp(IConsoleWrapper consoleWrapper)
    {
        _consoleWrapper = consoleWrapper;
    }

    public void Run()
    {
        var calculator = new Calculator();
        _consoleWrapper.WriteLine("Enter comma separated numbers(enter to exit):");

        while (true)
        {
            var input = _consoleWrapper.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                break;
            }
            var result = calculator.Add(input);
            _consoleWrapper.WriteLine($"Result: {result}");
            _consoleWrapper.WriteLine("you can enter other numbers (enter to exit)?");

        }
    }
}