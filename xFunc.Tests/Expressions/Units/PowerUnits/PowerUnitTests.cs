namespace xFunc.Tests.Expressions.Units.PowerUnits;

public class PowerUnitTests
{
    [Test]
    public void EqualTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Watt;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Kilowatt;

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void ObjectEqualTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Watt as object;

        Assert.That(left.Equals(right), Is.True);
    }

    [Test]
    public void ObjectNotEqualTest()
    {
        var left = PowerUnit.Watt;
        var right = new object();

        Assert.That(left.Equals(right), Is.False);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Watt;

        Assert.That(left == right, Is.True);
    }

    [Test]
    public void NotEqualOperatorTest()
    {
        var left = PowerUnit.Watt;
        var right = PowerUnit.Kilowatt;

        Assert.That(left != right, Is.True);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => PowerUnit.FromName(name, out _));
}