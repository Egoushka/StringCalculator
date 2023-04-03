using StringCalculator;

Console.WriteLine("Enter numbers with separators to add:");

var stringCalculator = new Calculator();

var userInput = Console.ReadLine();
var result = stringCalculator.Add(userInput);

Console.WriteLine(result);