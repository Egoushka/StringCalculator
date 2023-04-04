using Moq;
using StringCalculator;
using StringCalculator.Console;
using Xunit;

namespace StringCalculatorTests;

public class ConsoleAppTests
{
    private readonly ConsoleApp _consoleApp;
    private readonly Mock<ConsoleWrapper> _consoleWrapper;
    private readonly Mock<Calculator> _calculator;

    public ConsoleAppTests()
    {
        _calculator = new Mock<Calculator>();
        _consoleWrapper = new Mock<ConsoleWrapper>();

        _consoleApp = new ConsoleApp(_consoleWrapper.Object, _calculator.Object);
    }

    [Fact]
    public void Run_EmptyString_PrintStartMessage()
    {
        // Arrange
        _consoleWrapper.Setup(x => x.ReadLine()).Returns("");
        _calculator.Setup(x => x.Add("")).Returns(0);

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Enter comma separated numbers(enter to exit):"), Times.Once);
    }

    [Fact]
    public void Run_OneNumber_PrintSecondInputMessage()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("");
        _calculator.SetupSequence(x => x.Add(It.IsAny<string>()))
            .Returns(1);

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("you can enter other numbers (enter to exit)?"), Times.Once);
    }

    [Fact]
    public void Run_TwoInputs_PrintResults()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("2")
            .Returns("");
        _calculator.SetupSequence(x => x.Add(It.IsAny<string>()))
            .Returns(1)
            .Returns(2);

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);
        _consoleWrapper.Verify(x => x.WriteLine("Result: 2"), Times.Once);
    }
}