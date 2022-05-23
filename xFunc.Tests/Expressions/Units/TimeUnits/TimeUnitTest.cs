// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TimeUnits;

public class TimeUnitTest
{
    [Fact]
    public void EqualsTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Second;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var a = TimeUnit.Second;
        var b = LengthUnit.Millimeter;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Second as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = TimeUnit.Second;
        var b = 1 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Second;

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperatorTest()
    {
        var a = TimeUnit.Second;
        var b = TimeUnit.Millisecond;

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = TimeUnit.Second;

        Assert.Equal("s", a.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => TimeUnit.FromName(name, out _));
}