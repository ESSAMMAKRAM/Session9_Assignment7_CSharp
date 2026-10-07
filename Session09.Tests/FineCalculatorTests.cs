using Xunit;

public class FineCalculatorTests
{
    private readonly FineCalculator _calculator;

    public FineCalculatorTests()
    {
        _calculator = new FineCalculator();
    }

    // 1
    [Fact]
    public void Add_ShouldReturnCorrectSum()
    {
        // Arrange
        int a = 5, b = 3;
        int expected = 8;

        // Act
        int actual = _calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, actual);
    }

    // 2
    [Fact]
    public void Add_ShouldReturnNotEqual()
    {
        // Arrange
        int a = 5, b = 3;
        int notExpected = 10;

        // Act
        int actual = _calculator.Add(a, b);

        // Assert
        Assert.NotEqual(notExpected, actual);
    }

    // 3
    [Fact]
    public void Subtract_ShouldReturnCorrectDifference()
    {
        // Arrange
        int a = 10, b = 4;
        int expected = 6;

        // Act
        int actual = _calculator.Subtract(a, b);

        // Assert
        Assert.Equal(expected, actual);
    }

    // 4
    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(0, 9, 0)]
    [InlineData(-4, 5, -20)]   // negative number case
    public void Multiply_ShouldReturnCorrectResult(int a, int b, int expected)
    {
        // Arrange (data comes from InlineData)

        // Act
        int actual = _calculator.Multiply(a, b);

        // Assert
        Assert.Equal(expected, actual);
    }

    // 5
    [Theory]
    [InlineData(2, 3, 7)]
    [InlineData(-4, 5, 20)]
    [InlineData(10, 10, 0)]
    public void Multiply_ShouldReturnNotEqual(int a, int b, int notExpected)
    {
        // Arrange (data comes from InlineData)

        // Act
        int actual = _calculator.Multiply(a, b);

        // Assert
        Assert.NotEqual(notExpected, actual);
    }

    // 6
    [Fact]
    public void Divide_ByZero_ShouldThrowException()
    {
        // Arrange
        int a = 10, b = 0;

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
    }
}
