// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.VolumeUnits;

public class VolumeUnitTest
{
    [Fact]
    public void EqualsTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Meter;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Centimeter;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Meter as object;

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void ObjectEqualsWithDifferentTypesTest()
    {
        var a = VolumeUnit.Meter;
        var b = 1 as object;

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsOperatorTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Meter;

        Assert.True(a == b);
    }

    [Fact]
    public void NotEqualsOperatorTest()
    {
        var a = VolumeUnit.Meter;
        var b = VolumeUnit.Centimeter;

        Assert.True(a != b);
    }

    [Fact]
    public void ToStringTest()
    {
        var a = VolumeUnit.Meter;

        Assert.Equal("m^3", a.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => VolumeUnit.FromName(name, out _));
}