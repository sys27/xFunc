using xFunc.Maths.Expressions.Domains;

namespace xFunc.Tests.Expressions.Domains;

public class DomainTests
{
    public static IEnumerable<object[]> GetCtorTestData()
    {
        yield return new object[] { null };
        yield return new object[] { Array.Empty<DomainRange>() };
    }

    [Test]
    [TestCaseSource(nameof(GetCtorTestData))]
    public void CtorNullTest(DomainRange[] ranges)
        => Assert.Throws<ArgumentNullException>(() => new Domain(ranges));

    [Test]
    public void CtorInvalidDomainTest()
        => Assert.Throws<ArgumentException>(() => new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.One, true, NumberValue.Two, true),
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
        }));

    [Test]
    public void EqualTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true) });

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) }) as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectNotEqualTest1()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true) }) as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectNotEqualTest2()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new object();

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualOperatorTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true) });

        Assert.That(a != b, Is.True);
    }

    [Test]
    public void ToStringTest()
    {
        var domain = new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
            new DomainRange(NumberValue.One, true, NumberValue.PositiveInfinity, false)
        });
        var expected = "(-∞; -1] ∪ [1; ∞)";

        Assert.That(domain.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void InRangeTest()
    {
        var domain = new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
            new DomainRange(NumberValue.One, true, NumberValue.PositiveInfinity, false)
        });
        var result = domain.IsInRange(NumberValue.One);

        Assert.That(result, Is.True);
    }

    [Test]
    public void NotInRangeTest()
    {
        var domain = new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
            new DomainRange(NumberValue.One, true, NumberValue.PositiveInfinity, false)
        });
        var result = domain.IsInRange(NumberValue.Zero);

        Assert.That(result, Is.False);
    }
}