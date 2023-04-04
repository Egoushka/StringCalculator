using Moq;
using StringCalculator.Console;
using Xunit;

namespace StringCalculatorTests;

public class ConsoleAppTests
{
    private readonly ConsoleApp _consoleApp;
    private readonly Mock<IConsoleWrapper> _consoleWrapper;
    public ConsoleAppTests()
    {
        _consoleWrapper = new Mock<IConsoleWrapper>();
        _consoleApp = new ConsoleApp(_consoleWrapper.Object);
    }

    [Fact]
    public void Run_EmptyString_PrintNothing()
    {
        // Arrange
        _consoleWrapper.Setup(x => x.ReadLine()).Returns("");
        
        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Enter comma separated numbers(enter to exit):"), Times.Once);
    }

    [Fact]
    public void Run_TwoInputs_PrintTwoResults()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("2")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Enter comma separated numbers(enter to exit):"), Times.Once);
        _consoleWrapper.Verify(x => x.WriteLine("you can enter other numbers (enter to exit)?"), Times.Exactly(2));
        _consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);
        _consoleWrapper.Verify(x => x.WriteLine("Result: 2"), Times.Once);
    }

    [Fact]
    public void Run_OneNumber_PrintResult()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);
    }

    [Fact]
    public void Run_WithDifferentSeparators_PrintSum()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//;\\n1;\\n2,\\3")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithNegativeNumber_ShouldThrowsException()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("-1,-3")
            .Returns("");

        // Act
        var exception = Assert.Throws<ArgumentException>(() => _consoleApp.Run());

        // Assert
        Assert.Equal("Negative numbers are not allowed (-1, -3)", exception.Message);
    }

    [Fact]
    public void Run_NumbersAboveUpperLimit_PrintSumWithoutNumberAboveLimit()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("1,10000")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 1"), Times.Once);

    }

    [Fact]
    public void Run_NumbersWithCustomSeparator_PrintSum()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//;\\n2;3")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 5"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithCustomSeparatorAndNewLine_PrintSum()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//;\\n2;3\\n4")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 9"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithSeparatorOfAnyLength_PrintSum()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//[***]\\n1***2***3")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithMultipleCustomSeparators_PrintSum()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//[*][%]\\n1*2%3")
            .Returns("");
        
        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }

    [Fact]
    public void Run_NumbersWithCustomSeparatorAsNestedBrackets_PrintSum()
    {
        // Arrange
        _consoleWrapper.SetupSequence(x => x.ReadLine())
            .Returns("//[**[**]\\n1**[**2**[**3")
            .Returns("");

        // Act
        _consoleApp.Run();

        // Assert
        _consoleWrapper.Verify(x => x.WriteLine("Result: 6"), Times.Once);
    }
}