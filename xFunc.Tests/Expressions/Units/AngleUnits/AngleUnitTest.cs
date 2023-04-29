namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class AngleUnitTest
{
    [Fact]
    public void EqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Degree;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Radian;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Degree as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectNotEqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = 1 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Degree;

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperatorTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Radian;

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = AngleUnit.Degree;

        Assert.Equal("degree", a.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => AngleUnit.FromName(name, out _));
}