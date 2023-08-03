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

    [Theory]
    [MemberData(nameof(GetCtorTestData))]
    public void CtorTest(NumberValue start, bool isStartInclusive, NumberValue end, bool isEndInclusive)
        => Assert.Throws<ArgumentException>(() => new DomainRange(start, isStartInclusive, end, isEndInclusive));

    [Fact]
    public void EqualTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, true);

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualTest1()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.Two, true, NumberValue.One, true);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void NotEqualTest2()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, false, NumberValue.One, true);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void NotEqualTest3()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.Two, true);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void NotEqualTest4()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, false);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, true) as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectNotEqualTest1()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, false, NumberValue.One, true) as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectNotEqualTest2()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new object();

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, true, NumberValue.One, true);

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualOperatorTest()
    {
        var a = new DomainRange(-NumberValue.One, true, NumberValue.One, true);
        var b = new DomainRange(-NumberValue.One, false, NumberValue.One, true);

        Assert.True(a != b);
    }

    public static IEnumerable<object[]> GetToStringTestData()
    {
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true), "[-1; 1]" };
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, false), "[-1; 1)" };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true), "(-1; 1]" };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, false), "(-1; 1)" };
    }

    [Theory]
    [MemberData(nameof(GetToStringTestData))]
    public void ToStringTest(DomainRange range, string expected)
        => Assert.Equal(expected, range.ToString());

    public static IEnumerable<object[]> GetInRangeTestData()
    {
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true), NumberValue.Zero };
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, false), NumberValue.Zero };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true), NumberValue.Zero };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, false), NumberValue.Zero };
    }

    [Theory]
    [MemberData(nameof(GetInRangeTestData))]
    public void InRangeTest(DomainRange range, NumberValue number)
        => Assert.True(range.IsInRange(number));

    public static IEnumerable<object[]> GetNotInRangeTestTest()
    {
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, true), NumberValue.Two };
        yield return new object[] { new DomainRange(-NumberValue.One, true, NumberValue.One, false), NumberValue.Two };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, true), NumberValue.Two };
        yield return new object[] { new DomainRange(-NumberValue.One, false, NumberValue.One, false), NumberValue.Two };
    }

    [Theory]
    [MemberData(nameof(GetNotInRangeTestTest))]
    public void NotInRangeTest(DomainRange range, NumberValue number)
        => Assert.False(range.IsInRange(number));
}