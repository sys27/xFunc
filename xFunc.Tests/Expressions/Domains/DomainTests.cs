using xFunc.Maths.Expressions.Domains;

namespace xFunc.Tests.Expressions.Domains;

public class DomainTests
{
    public static IEnumerable<object[]> GetCtorTestData()
    {
        yield return new object[] { null };
        yield return new object[] { Array.Empty<DomainRange>() };
    }

    [Theory]
    [MemberData(nameof(GetCtorTestData))]
    public void CtorNullTest(DomainRange[] ranges)
        => Assert.Throws<ArgumentNullException>(() => new Domain(ranges));

    [Fact]
    public void CtorInvalidDomainTest()
        => Assert.Throws<ArgumentException>(() => new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.One, true, NumberValue.Two, true),
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
        }));

    [Fact]
    public void EqualTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true) });

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) }) as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectNotEqualTest1()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true) }) as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectNotEqualTest2()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new object();

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualOperatorTest()
    {
        var a = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true) });
        var b = new Domain(new DomainRange[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true) });

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var domain = new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
            new DomainRange(NumberValue.One, true, NumberValue.PositiveInfinity, false)
        });
        var expected = "(-∞; -1] ∪ [1; ∞)";

        Assert.Equal(expected, domain.ToString());
    }

    [Fact]
    public void InRangeTest()
    {
        var domain = new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
            new DomainRange(NumberValue.One, true, NumberValue.PositiveInfinity, false)
        });
        var result = domain.IsInRange(NumberValue.One);

        Assert.True(result);
    }

    [Fact]
    public void NotInRangeTest()
    {
        var domain = new Domain(new DomainRange[]
        {
            new DomainRange(NumberValue.NegativeInfinity, false, -NumberValue.One, true),
            new DomainRange(NumberValue.One, true, NumberValue.PositiveInfinity, false)
        });
        var result = domain.IsInRange(NumberValue.Zero);

        Assert.False(result);
    }
}