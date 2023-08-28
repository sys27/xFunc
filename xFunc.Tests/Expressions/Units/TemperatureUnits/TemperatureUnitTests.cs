namespace xFunc.Tests.Expressions.Units.TemperatureUnits;

public class TemperatureUnitTests
{
    [Test]
    public void EqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Celsius;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Fahrenheit;

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void ObjectEqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Celsius as object;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void ObjectNotEqualTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = new object();

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Celsius;

        Assert.That(left == right, Is.True);
    }

    [Test]
    public void NotEqualOperatorTest()
    {
        var left = TemperatureUnit.Celsius;
        var right = TemperatureUnit.Fahrenheit;

        Assert.That(left != right, Is.True);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => TemperatureUnit.FromName(name, out _));
}