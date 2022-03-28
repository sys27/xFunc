// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AreaUnits;

public class AreaUnitTest
{
    [Fact]
    public void EqualsTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Meter;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Kilometer;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Meter as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = AreaUnit.Meter;
        var b = 1 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Meter;

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperatorTest()
    {
        var a = AreaUnit.Meter;
        var b = AreaUnit.Kilometer;

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = AreaUnit.Meter;

        Assert.Equal("m^2", a.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => AreaUnit.FromName(name, out _));
}