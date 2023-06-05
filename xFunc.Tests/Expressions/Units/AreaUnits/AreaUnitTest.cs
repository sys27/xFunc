// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AreaUnits;

public class AreaUnitTest
{
    [Fact]
    public void EqualsNullTest()
    {
        var a = AreaUnit.Meter;

        Assert.False(a.Equals(null));
    }

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
    public void ObjectEqualsNullTest()
    {
        var a = AreaUnit.Meter;

        Assert.False(a.Equals(null as object));
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

    public static IEnumerable<object[]> GetToVolumeUnitTest()
    {
        yield return new object[] { AreaUnit.Meter, VolumeUnit.Meter };
        yield return new object[] { AreaUnit.Centimeter, VolumeUnit.Centimeter };
        yield return new object[] { AreaUnit.Inch, VolumeUnit.Inch };
        yield return new object[] { AreaUnit.Foot, VolumeUnit.Foot };
        yield return new object[] { AreaUnit.Yard, VolumeUnit.Yard };
    }

    [Theory]
    [MemberData(nameof(GetToVolumeUnitTest))]
    public void ToVolumeUnitTest(AreaUnit unit, VolumeUnit expected)
    {
        var actual = unit.ToVolumeUnit();

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> GetToVolumeUnitExceptionTest()
    {
        yield return new object[] { AreaUnit.Millimeter };
        yield return new object[] { AreaUnit.Kilometer };
        yield return new object[] { AreaUnit.Mile };
        yield return new object[] { AreaUnit.Hectare };
        yield return new object[] { AreaUnit.Acre };
    }

    [Theory]
    [MemberData(nameof(GetToVolumeUnitExceptionTest))]
    public void ToVolumeUnitExceptionTest(AreaUnit unit)
        => Assert.Throws<InvalidOperationException>(unit.ToVolumeUnit);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => AreaUnit.FromName(name, out _));
}