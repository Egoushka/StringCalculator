namespace StringCalculator.Console;

public class ConsoleWrapper : IConsoleWrapper
{
    public string ReadLine()
    {
        return System.Console.ReadLine();
    }

    public void WriteLine(string message)
    {
        System.Console.WriteLine(message);
    }
}
