// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.VolumeUnits;

public class VolumeValueTest
{
    [Fact]
    public void EqualTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(10);

        Assert.True(area1.Equals(area2));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(10);

        Assert.True(area1 == area2);
    }

    [Fact]
    public void NotEqualTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(12);

        Assert.True(area1 != area2);
    }

    [Fact]
    public void LessTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(12);

        Assert.True(area1 < area2);
    }

    [Fact]
    public void LessFalseTest()
    {
        var area1 = VolumeValue.Meter(20);
        var area2 = VolumeValue.Meter(12);

        Assert.False(area1 < area2);
    }

    [Fact]
    public void GreaterTest()
    {
        var area1 = VolumeValue.Meter(20);
        var area2 = VolumeValue.Meter(12);

        Assert.True(area1 > area2);
    }

    [Fact]
    public void GreaterFalseTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(12);

        Assert.False(area1 > area2);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(10);

        Assert.True(area1 <= area2);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(10);

        Assert.True(area1 >= area2);
    }

    [Fact]
    public void CompareToNull()
    {
        var areaValue = VolumeValue.Meter(10);

        Assert.True(areaValue.CompareTo(null) > 0);
    }

    [Fact]
    public void CompareToObject()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = (object)VolumeValue.Meter(10);

        Assert.True(area1.CompareTo(area2) == 0);
    }

    [Fact]
    public void CompareToDouble()
    {
        var areaValue = VolumeValue.Meter(10);

        Assert.Throws<ArgumentException>(() => areaValue.CompareTo(1));
    }

    [Fact]
    public void ValueNotEqualTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(12);

        Assert.False(area1.Equals(area2));
    }

    [Fact]
    public void UnitNotEqualTest2()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Centimeter(10);

        Assert.False(area1.Equals(area2));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = 3;

        Assert.False(area1.Equals(area2));
    }

    [Fact]
    public void EqualObjectTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(10);

        Assert.True(area1.Equals(area2 as object));
    }

    [Fact]
    public void NotEqualObjectTest()
    {
        var area1 = VolumeValue.Meter(10);
        var area2 = VolumeValue.Meter(20);

        Assert.False(area1.Equals(area2 as object));
    }

    [Fact]
    public void ToStringSecondTest()
    {
        var areaValue = VolumeValue.Meter(10);

        Assert.Equal("10 m^3", areaValue.ToString());
    }

    [Fact]
    public void AddOperatorTest()
    {
        var area1 = VolumeValue.Centimeter(1);
        var area2 = VolumeValue.Meter(1);
        var expected = VolumeValue.Centimeter(1000001);
        var result = area1 + area2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubOperatorTest()
    {
        var area1 = VolumeValue.Meter(1);
        var area2 = VolumeValue.Centimeter(500000);
        var expected = VolumeValue.Meter(0.5);
        var result = area1 - area2;

        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Meter, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Centimeter, 10000000.0 };
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Liter, 10000.0 };
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Inch, 610236.10035 };
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Foot, 353.14666721 };
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Yard, 13.079506193 };
        yield return new object[] { 10.0, VolumeUnit.Meter, VolumeUnit.Gallon, 2641.7205124 };

        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Centimeter, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Meter, 0.00001 };
        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Liter, 0.01 };
        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Inch, 0.6102374409 };
        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Foot, 0.0003531467 };
        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Yard, 0.0000130795 };
        yield return new object[] { 10.0, VolumeUnit.Centimeter, VolumeUnit.Gallon, 0.0026417205 };

        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Liter, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Meter, 0.01 };
        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Centimeter, 10000.0 };
        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Inch, 610.2361 };
        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Foot, 0.3531466672 };
        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Yard, 0.0130795062 };
        yield return new object[] { 10.0, VolumeUnit.Liter, VolumeUnit.Gallon, 2.6417205236 };

        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Inch, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Meter, 0.0001638706 };
        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Centimeter, 163.871 };
        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Liter, 0.16387064 };
        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Foot, 0.005787037 };
        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Yard, 0.0002143347 };
        yield return new object[] { 10.0, VolumeUnit.Inch, VolumeUnit.Gallon, 0.0432900433 };

        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Foot, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Meter, 0.2831684659 };
        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Centimeter, 283168.466 };
        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Liter, 283.16846592 };
        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Inch, 17279.96204 };
        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Yard, 0.3703703704 };
        yield return new object[] { 10.0, VolumeUnit.Foot, VolumeUnit.Gallon, 74.805194805 };

        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Yard, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Meter, 7.6455485798 };
        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Centimeter, 7645548.58 };
        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Liter, 7645.5485798 };
        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Inch, 466558.97505 };
        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Foot, 270.0 };
        yield return new object[] { 10.0, VolumeUnit.Yard, VolumeUnit.Gallon, 2019.74025 };

        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Gallon, 10.0 };
        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Meter, 0.0378541178 };
        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Centimeter, 37854.118 };
        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Liter, 37.85411784 };
        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Inch, 2309.99494 };
        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Foot, 1.3368055556 };
        yield return new object[] { 10.0, VolumeUnit.Gallon, VolumeUnit.Yard, 0.0495113169 };
    }

    [Theory]
    [MemberData(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, VolumeUnit unit, VolumeUnit to, double expected)
    {
        var area = new VolumeValue(new NumberValue(value), unit);
        var converted = area.To(to);

        Assert.Equal(expected, converted.Value.Number, 5);
    }
}