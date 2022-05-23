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

    public static IEnumerable<object[]> GetToAreaUnitTest()
    {
        yield return new object[] { LengthUnit.Meter, AreaUnit.Meter };
        yield return new object[] { LengthUnit.Millimeter, AreaUnit.Millimeter };
        yield return new object[] { LengthUnit.Centimeter, AreaUnit.Centimeter };
        yield return new object[] { LengthUnit.Kilometer, AreaUnit.Kilometer };
        yield return new object[] { LengthUnit.Inch, AreaUnit.Inch };
        yield return new object[] { LengthUnit.Foot, AreaUnit.Foot };
        yield return new object[] { LengthUnit.Yard, AreaUnit.Yard };
        yield return new object[] { LengthUnit.Mile, AreaUnit.Mile };
    }

    [Theory]
    [MemberData(nameof(GetToAreaUnitTest))]
    public void ToAreaUnitTest(LengthUnit unit, AreaUnit expected)
    {
        var actual = unit.ToAreaUnit();

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> GetToAreaUnitExceptionTest()
    {
        yield return new object[] { LengthUnit.Nanometer };
        yield return new object[] { LengthUnit.Micrometer };
        yield return new object[] { LengthUnit.Decimeter };
        yield return new object[] { LengthUnit.NauticalMile };
        yield return new object[] { LengthUnit.Chain };
        yield return new object[] { LengthUnit.Rod };
        yield return new object[] { LengthUnit.AstronomicalUnit };
        yield return new object[] { LengthUnit.LightYear };
        yield return new object[] { LengthUnit.Parsec };
    }

    [Theory]
    [MemberData(nameof(GetToAreaUnitExceptionTest))]
    public void ToAreaUnitExceptionTest(LengthUnit unit)
        => Assert.Throws<InvalidOperationException>(unit.ToAreaUnit);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void FromNameEmptyString(string name)
        => Assert.Throws<ArgumentNullException>(() => LengthUnit.FromName(name, out _));
}