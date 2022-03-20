namespace xFunc.Tests.Expressions.Units.PowerUnits;

public class PowerUnitTests
{
    [Fact]
    public void EqualTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Watt;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void NotEqualTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Kilowatt;

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void ObjectEqualTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Watt as object;

        Assert.True(left.Equals(right));
    }

    [Fact]
    public void ObjectNotEqualTest()
    {
        var left = PowerUnit.Watt;
        var right = new object();

        Assert.False(left.Equals(right));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Watt;

        Assert.True(left == right);
    }

    [Fact]
    public void NotEqualOperatorTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Kilowatt;

        Assert.True(left != right);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => PowerUnit.FromName(name, out _));
}