// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.MassUnits;

public class MassUnitTest
{
    [Fact]
    public void EqualsTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Kilogram;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Gram;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Kilogram as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = MassUnit.Kilogram;
        var b = 1 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Kilogram;

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperatorTest()
    {
        var a = MassUnit.Kilogram;
        var b = MassUnit.Gram;

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = MassUnit.Kilogram;

        Assert.Equal("kg", a.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => MassUnit.FromName(name, out _));
}