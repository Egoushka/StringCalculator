using Moq;
using StringCalculator.Console;
using Xunit;

namespace StringCalculatorTests;

public class ConsoleAppTests
{
    private ConsoleApp _consoleApp;
    public ConsoleAppTests()
    {
    }
    [Fact]
    public void Run_EmptyString_ReturnsNothing()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.Setup(x => x.ReadLine()).Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);
        
        // Act
        _consoleApp.Run();
        
        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Enter comma separated numbers(enter to exit):"), Times.Once);
    }
    [Fact]
    public void Run_OneNumberAndAnotherNumber_ReturnTwoResults()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("2")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);
        
        // Act
        _consoleApp.Run();
        
        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Enter comma separated numbers(enter to exit):"), Times.Once);
        consoleWrapper.Verify(x => x.WriteLine("you can enter other numbers (enter to exit)?"), Times.Exactly(2));
        consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);
        consoleWrapper.Verify(x => x.WriteLine("Result: 2"), Times.Once);
    }

    [Fact]
    public void Run_OneNumber_ReturnResult()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);
        
        // Act
        _consoleApp.Run();
        
        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);
    }
    
}