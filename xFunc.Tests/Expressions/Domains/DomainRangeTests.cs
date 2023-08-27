// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions.Domains;

namespace xFunc.Tests.Expressions.Domains;

public class DomainRangeTests
{
    public static IEnumerable<object[]> GetCtorTestData()
    {
        yield return new object[] { NumberValue.NegativeInfinity, true, NumberValue.One, false };
        yield return new object[] { NumberValue.PositiveInfinity, true, NumberValue.One, false };
        yield return new object[] { NumberValue.One, false, NumberValue.NegativeInfinity, true };
        yield return new object[] { NumberValue.One, false, NumberValue.PositiveInfinity, true };
        yield return new object[] { NumberValue.NegativeInfinity, true, NumberValue.PositiveInfinity, true };
        yield return new object[] { NumberValue.Two, true, NumberValue.One, true };
    }

    [Test]
    [TestCaseSource(nameof(GetCtorTestData))]
    public void CtorTest(NumberValue start, bool isStartInclusive, NumberValue end, bool isEndInclusive)
        => Assert.Throws<ArgumentException>(() => new DomainRange(start, isStartInclusive, end, isEndInclusive));

    [Test]
    public void EqualTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, true);

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void NotEqualTest1()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.Two, true, NumberValue.One, true);

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void NotEqualTest2()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, false, NumberValue.One, true);

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void NotEqualTest3()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.Two, true);

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void NotEqualTest4()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, false);

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectEqualTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, true) as object;

        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void ObjectNotEqualTest1()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, false, NumberValue.One, true) as object;

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void ObjectNotEqualTest2()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new object();

        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, true);

        Assert.That(a == b, Is.True);
    }

    [Test]
    public void NotEqualOperatorTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, false, NumberValue.One, true);

        Assert.That(a != b, Is.True);
    }

    public static IEnumerable<object[]> GetToStringTestData()
    {
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true), "[-1; 1]" };
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, false), "[-1; 1)" };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true), "(-1; 1]" };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, false), "(-1; 1)" };
    }

    [Test]
    [TestCaseSource(nameof(GetToStringTestData))]
    public void ToStringTest(DomainRange range, string expected)
        => Assert.That(range.ToString(), Is.EqualTo(expected));

    public static IEnumerable<object[]> GetInRangeTestData()
    {
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true), NumberValue.Zero };
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, false), NumberValue.Zero };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true), NumberValue.Zero };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, false), NumberValue.Zero };
    }

    [Test]
    [TestCaseSource(nameof(GetInRangeTestData))]
    public void InRangeTest(DomainRange range, NumberValue number)
        => Assert.That(range.IsInRange(number), Is.True);

    public static IEnumerable<object[]> GetNotInRangeTestTest()
    {
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true), NumberValue.Two };
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, false), NumberValue.Two };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true), NumberValue.Two };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, false), NumberValue.Two };
    }

    [Test]
    [TestCaseSource(nameof(GetNotInRangeTestTest))]
    public void NotInRangeTest(DomainRange range, NumberValue number)
        => Assert.That(range.IsInRange(number), Is.False);
}