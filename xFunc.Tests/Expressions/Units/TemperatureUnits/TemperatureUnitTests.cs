namespace xFunc.Tests.Expressions.Units.TemperatureUnits;

public class TemperatureUnitTests
{
    [Fact]
    public void EqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Celsius;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void NotEqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Fahrenheit;

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void ObjectEqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Celsius as object;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void ObjectNotEqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = new object();

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Celsius;

        Assert.True(left == right);
    }

    [Fact]
    public void NotEqualOperatorTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Fahrenheit;

        Assert.True(left != right);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => TemperatureUnit.FromName(name, out _));
}