// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.LengthUnits;

public class LengthUnitTest
{
    [Fact]
    public void EqualsTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Meter;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Millimeter;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Meter as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = LengthUnit.Meter;
        var b = 1 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Meter;

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperatorTest()
    {
        var a = LengthUnit.Meter;
        var b = LengthUnit.Millimeter;

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = LengthUnit.Meter;

        Assert.Equal("m", a.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => LengthUnit.FromName(name, out _));
}