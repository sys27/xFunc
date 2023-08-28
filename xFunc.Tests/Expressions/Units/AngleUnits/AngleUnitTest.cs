namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class AngleUnitTest
{
    [Test]
    public void EqualsNullTest()
    {
        var a = AngleUnit.Degree;

        Assert.That(a.Equals(null), Is.False);
    }

    [Test]
    public void EqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Degree;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Radian;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualsNullTest()
    {
        var a = AngleUnit.Degree;

        Assert.That(a.Equals(null as object), Is.False);
    }

    [Test]
    public void ObjectEqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Degree as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectNotEqualsTest()
    {
        var a = AngleUnit.Degree;
        var b = 1 as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsOperatorTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Degree;

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualsOperatorTest()
    {
        var a = AngleUnit.Degree;
        var b = AngleUnit.Radian;

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var a = AngleUnit.Degree;

        Assert.That(a.ToString(), Is.EqualTo("degree"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => AngleUnit.FromName(name, out _));
}