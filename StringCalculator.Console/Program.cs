namespace StringCalculator.Console;

static class Program
{
    public static void Main()
    {
        var consoleApp = new ConsoleApp(new ConsoleWrapper(), new Calculator());
        consoleApp.Run();
    }
}