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
    public void Run_EmptyString_PrintNothing()
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
    public void Run_TwoInputs_PrintTwoResults()
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
    public void Run_OneNumber_PrintResult()
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

    [Fact]
    public void Run_WithDifferentSeparators_PrintSum()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//;\\n1;\\n2,\\3")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithNegativeNumber_ShouldThrowsException()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("-1,-3")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => _consoleApp.Run());

        // Assert
        Assert.Equal("Negative numbers are not allowed (-1, -3)", exception.Message);
    }

    [Fact]
    public void Run_NumbersAboveUpperLimit_PrintSumWithoutNumberAboveLimit()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1,10000")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);

    }

    [Fact]
    public void Run_NumbersWithCustomSeparator_PrintSum()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//;\\n2;3")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 5"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithCustomSeparatorAndNewLine_PrintSum()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//;\\n2;3\\n4")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 9"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithSeparatorOfAnyLength_PrintSum()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//[***]\\n1***2***3")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithMultipleCustomSeparators_PrintSum()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//[*][%]\\n1*2%3")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithCustomSeparatorAsNestedBrackets_PrintSum()
    {
        // Arrange
        var consoleWrapper = new Mock<IConsoleWrapper>();
        consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//[**[**]\\n1**[**2**[**3")
            .Returns("");
        _consoleApp = new ConsoleApp(consoleWrapper.Object);

        // Act
        _consoleApp.Run();

        // Assert
        consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }
}