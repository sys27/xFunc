// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.LengthUnits;

public class LengthValueTest
{
    [Fact]
    public void EqualTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(10);

        Assert.True(length1.Equals(length2));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(10);

        Assert.True(length1 == length2);
    }

    [Fact]
    public void NotEqualTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(12);

        Assert.True(length1 != length2);
    }

    [Fact]
    public void LessTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(12);

        Assert.True(length1 < length2);
    }

    [Fact]
    public void LessFalseTest()
    {
        var length1 = LengthValue.Meter(20);
        var length2 = LengthValue.Meter(12);

        Assert.False(length1 < length2);
    }

    [Fact]
    public void GreaterTest()
    {
        var length1 = LengthValue.Meter(20);
        var length2 = LengthValue.Meter(12);

        Assert.True(length1 > length2);
    }

    [Fact]
    public void GreaterFalseTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(12);

        Assert.False(length1 > length2);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(10);

        Assert.True(length1 <= length2);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(10);

        Assert.True(length1 >= length2);
    }

    [Fact]
    public void CompareToNull()
    {
        var length = LengthValue.Meter(10);

        Assert.True(length.CompareTo(null) > 0);
    }

    [Fact]
    public void CompareToObject()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = (object)LengthValue.Meter(10);

        Assert.True(length1.CompareTo(length2) == 0);
    }

    [Fact]
    public void CompareToDouble()
    {
        var length = LengthValue.Meter(10);

        Assert.Throws<ArgumentException>(() => length.CompareTo(1));
    }

    [Fact]
    public void ValueNotEqualTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(12);

        Assert.False(length1.Equals(length2));
    }

    [Fact]
    public void UnitNotEqualTest2()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Kilometer(10);

        Assert.False(length1.Equals(length2));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = 3;

        Assert.False(length1.Equals(length2));
    }

    [Fact]
    public void EqualObjectTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(10);

        Assert.True(length1.Equals(length2 as object));
    }

    [Fact]
    public void NotEqualObjectTest()
    {
        var length1 = LengthValue.Meter(10);
        var length2 = LengthValue.Meter(20);

        Assert.False(length1.Equals(length2 as object));
    }

    [Fact]
    public void ToStringMeterTest()
    {
        var length = LengthValue.Meter(10);

        Assert.Equal("10 m", length.ToString());
    }

    [Fact]
    public void ToStringNanometerTest()
    {
        var length = LengthValue.Nanometer(10);

        Assert.Equal("10 nm", length.ToString());
    }

    [Fact]
    public void ToStringMicrometerTest()
    {
        var length = LengthValue.Micrometer(10);

        Assert.Equal("10 µm", length.ToString());
    }

    [Fact]
    public void ToStringMillimeterTest()
    {
        var length = LengthValue.Millimeter(10);

        Assert.Equal("10 mm", length.ToString());
    }

    [Fact]
    public void ToStringCentimeterTest()
    {
        var length = LengthValue.Centimeter(10);

        Assert.Equal("10 cm", length.ToString());
    }

    [Fact]
    public void ToStringDecimeterTest()
    {
        var length = LengthValue.Decimeter(10);

        Assert.Equal("10 dm", length.ToString());
    }

    [Fact]
    public void ToStringKilometerTest()
    {
        var length = LengthValue.Kilometer(10);

        Assert.Equal("10 km", length.ToString());
    }

    [Fact]
    public void ToStringInchTest()
    {
        var length = LengthValue.Inch(10);

        Assert.Equal("10 in", length.ToString());
    }

    [Fact]
    public void ToStringFootTest()
    {
        var length = LengthValue.Foot(10);

        Assert.Equal("10 ft", length.ToString());
    }

    [Fact]
    public void ToStringYardTest()
    {
        var length = LengthValue.Yard(10);

        Assert.Equal("10 yd", length.ToString());
    }

    [Fact]
    public void ToStringMileTest()
    {
        var length = LengthValue.Mile(10);

        Assert.Equal("10 mi", length.ToString());
    }

    [Fact]
    public void ToStringNauticalMileTest()
    {
        var length = LengthValue.NauticalMile(10);

        Assert.Equal("10 nmi", length.ToString());
    }

    [Fact]
    public void ToStringChainTest()
    {
        var length = LengthValue.Chain(10);

        Assert.Equal("10 ch", length.ToString());
    }

    [Fact]
    public void ToStringRodTest()
    {
        var length = LengthValue.Rod(10);

        Assert.Equal("10 rd", length.ToString());
    }

    [Fact]
    public void ToStringAstronomicalUnitTest()
    {
        var length = LengthValue.AstronomicalUnit(10);

        Assert.Equal("10 au", length.ToString());
    }

    [Fact]
    public void ToStringLightYearTest()
    {
        var length = LengthValue.LightYear(10);

        Assert.Equal("10 ly", length.ToString());
    }

    [Fact]
    public void ToStringParsecTest()
    {
        var length = LengthValue.Parsec(10);

        Assert.Equal("10 pc", length.ToString());
    }

    [Fact]
    public void AddOperatorTest()
    {
        var length1 = LengthValue.Meter(1);
        var length2 = LengthValue.Kilometer(1);
        var expected = LengthValue.Meter(1001);
        var result = length1 + length2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubOperatorTest()
    {
        var length1 = LengthValue.Kilometer(1);
        var length2 = LengthValue.Meter(1);
        var expected = LengthValue.Kilometer(0.999);
        var result = length1 - length2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MulOperatorTest()
    {
        var length1 = LengthValue.Kilometer(2);
        var length2 = LengthValue.Meter(2000);
        var expected = AreaValue.Kilometer(4);
        var result = length1 * length2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MulOperatorAreaLengthTest()
    {
        var length1 = AreaValue.Centimeter(20000);
        var length2 = LengthValue.Meter(2);
        var expected = VolumeValue.Centimeter(40000);
        var result = length1 * length2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void MulOperatorLengthAreaTest()
    {
        var length1 = LengthValue.Meter(2);
        var length2 = AreaValue.Centimeter(20000);
        var expected = VolumeValue.Centimeter(40000);
        var result = length1 * length2;

        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Meter, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Nanometer, 10000000000.0 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Micrometer, 10000000.0 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Millimeter, 10000.0 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Centimeter, 1000.0 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Decimeter, 100.0 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Kilometer, 0.01 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Inch, 393.7007874 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Foot, 32.80839895 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Yard, 10.936132983 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Mile, 0.0062137119 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.NauticalMile, 0.005399568 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Chain, 0.4970969538 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Rod, 1.9883878152 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.AstronomicalUnit, 6.684587122E-11 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.LightYear, 1.057000834E-15 };
        yield return new object[] { 10.0, LengthUnit.Meter, LengthUnit.Parsec, 3.240779289E-16 };

        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Nanometer, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Meter, 1.0E-8 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Micrometer, 0.01 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Millimeter, 0.00001 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Centimeter, 0.000001 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Decimeter, 1.0E-7 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Kilometer, 1.0E-11 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Inch, 3.937007874E-7 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Foot, 3.280839895E-8 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Yard, 1.093613298E-8 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Mile, 6.213711922E-12 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.NauticalMile, 5.399568034E-12 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Chain, 4.970969537E-10 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Rod, 1.988387815E-9 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.AstronomicalUnit, 6.684587122E-20 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.LightYear, 1.057000834E-24 };
        yield return new object[] { 10.0, LengthUnit.Nanometer, LengthUnit.Parsec, 3.240779289E-25 };

        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Micrometer, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Meter, 0.00001 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Nanometer, 10000.0 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Millimeter, 0.01 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Centimeter, 0.001 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Decimeter, 0.0001 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Kilometer, 1.0E-8 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Inch, 0.0003937008 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Foot, 0.0000328084 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Yard, 0.0000109361 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Mile, 6.213711922E-9 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.NauticalMile, 5.399568034E-9 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Chain, 4.970969537E-7 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Rod, 0.0000019884 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.AstronomicalUnit, 6.684587122E-17 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.LightYear, 1.057000834E-21 };
        yield return new object[] { 10.0, LengthUnit.Micrometer, LengthUnit.Parsec, 3.240779289E-22 };

        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Millimeter, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Meter, 0.01 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Nanometer, 10000000.0 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Micrometer, 10000.0 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Centimeter, 1.0 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Decimeter, 0.1 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Kilometer, 0.00001 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Inch, 0.3937007874 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Foot, 0.032808399 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Yard, 0.010936133 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Mile, 0.0000062137 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.NauticalMile, 0.0000053996 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Chain, 0.000497097 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Rod, 0.0019883878 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.AstronomicalUnit, 6.684587122E-14 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.LightYear, 1.057000834E-18 };
        yield return new object[] { 10.0, LengthUnit.Millimeter, LengthUnit.Parsec, 3.240779289E-19 };

        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Centimeter, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Meter, 0.1 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Nanometer, 100000000.0 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Micrometer, 100000.0 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Millimeter, 100.0 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Decimeter, 1.0 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Kilometer, 0.0001 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Inch, 3.937007874 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Foot, 0.3280839895 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Yard, 0.1093613298 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Mile, 0.0000621371 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.NauticalMile, 0.0000539957 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Chain, 0.0049709695 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Rod, 0.0198838782 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.AstronomicalUnit, 6.684587122E-13 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.LightYear, 1.057000834E-17 };
        yield return new object[] { 10.0, LengthUnit.Centimeter, LengthUnit.Parsec, 3.240779289E-18 };

        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Decimeter, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Meter, 1.0 };
        yield return new object[] { 1.0, LengthUnit.Decimeter, LengthUnit.Nanometer, 100000000.0 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Micrometer, 1000000.0 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Millimeter, 1000.0 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Centimeter, 100.0 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Kilometer, 0.001 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Inch, 39.37007874 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Foot, 3.280839895 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Yard, 1.0936132983 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Mile, 0.0006213712 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.NauticalMile, 0.0005399568 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Chain, 0.0497096954 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Rod, 0.1988387815 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.AstronomicalUnit, 6.684587122E-12 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.LightYear, 1.057000834E-16 };
        yield return new object[] { 10.0, LengthUnit.Decimeter, LengthUnit.Parsec, 3.240779289E-17 };

        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Kilometer, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Meter, 10000.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Nanometer, 10000000000000.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Micrometer, 10000000000.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Millimeter, 10000000.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Centimeter, 1000000.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Decimeter, 100000.0 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Inch, 393700.7874015748 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Foot, 32808.39895013 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Yard, 10936.132983377 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Mile, 6.2137119224 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.NauticalMile, 5.3995680346 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Chain, 497.09695379 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Rod, 1988.387815159 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.AstronomicalUnit, 6.684587122E-8 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.LightYear, 1.057000834E-12 };
        yield return new object[] { 10.0, LengthUnit.Kilometer, LengthUnit.Parsec, 3.240779289E-13 };

        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Inch, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Meter, 0.254 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Nanometer, 254000000.0 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Micrometer, 254000.0 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Millimeter, 254.0 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Centimeter, 25.4 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Decimeter, 2.54 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Kilometer, 0.000254 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Foot, 0.8333333333 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Yard, 0.2777777778 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Mile, 0.0001578283 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.NauticalMile, 0.000137149 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Chain, 0.0126262626 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Rod, 0.0505050505 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.AstronomicalUnit, 1.697885129E-12 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.LightYear, 2.684782118E-17 };
        yield return new object[] { 10.0, LengthUnit.Inch, LengthUnit.Parsec, 8.231579395E-18 };

        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Foot, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Meter, 3.048 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Nanometer, 3048000000 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Micrometer, 3048000 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Millimeter, 3048 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Centimeter, 304.8 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Decimeter, 30.48 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Kilometer, 0.003048 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Inch, 120.0 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Yard, 3.3333333333 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Mile, 0.0018939394 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.NauticalMile, 0.0016457883 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Chain, 0.1515151515 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Rod, 0.6060606061 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.AstronomicalUnit, 2.037462154E-11 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.LightYear, 3.221738542E-16 };
        yield return new object[] { 10.0, LengthUnit.Foot, LengthUnit.Parsec, 9.877895274E-17 };

        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Yard, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Meter, 9.144 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Nanometer, 9144000000 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Micrometer, 9144000 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Millimeter, 9144 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Centimeter, 914.4 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Decimeter, 91.44 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Kilometer, 0.009144 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Inch, 360.0 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Foot, 30.0 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Mile, 0.0056818182 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.NauticalMile, 0.004937365010799136 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Chain, 0.4545454545 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Rod, 1.8181818182 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.AstronomicalUnit, 6.112386464E-11 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.LightYear, 9.665215626E-16 };
        yield return new object[] { 10.0, LengthUnit.Yard, LengthUnit.Parsec, 2.963368582E-16 };

        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Mile, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Meter, 16093.44 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Nanometer, 16093440000000 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Micrometer, 16093440000 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Millimeter, 16093440 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Centimeter, 1609344 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Decimeter, 160934.4 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Kilometer, 16.09344 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Inch, 633600 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Foot, 52800 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Yard, 17600 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.NauticalMile, 8.689762419 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Chain, 800.0 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Rod, 3200.0 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.AstronomicalUnit, 1.075780017E-7 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.LightYear, 1.70107795E-12 };
        yield return new object[] { 10.0, LengthUnit.Mile, LengthUnit.Parsec, 5.215528705E-13 };

        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.NauticalMile, 10.0 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Meter, 18520 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Nanometer, 18520000000000 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Micrometer, 18520000000 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Millimeter, 18520000 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Centimeter, 1852000 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Decimeter, 185200 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Kilometer, 18.52 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Inch, 729133.85827 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Foot, 60761.154856 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Yard, 20253.7182852 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Mile, 11.50779448 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Chain, 920.62355842 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Rod, 3682.4942337 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.AstronomicalUnit, 1.237985535E-7 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.LightYear, 1.957565544E-12 };
        yield return new object[] { 10.0, LengthUnit.NauticalMile, LengthUnit.Parsec, 6.001923244E-13 };

        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Chain, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Meter, 201.168 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Nanometer, 201168000000 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Micrometer, 201168000 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Millimeter, 201168 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Centimeter, 20116.8 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Decimeter, 2011.68 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Kilometer, 0.201168 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Inch, 7920.0 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Foot, 660.0 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Yard, 220.0 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Mile, 0.125 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.NauticalMile, 0.1086220302 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Rod, 40.0 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.AstronomicalUnit, 1.344725022E-9 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.LightYear, 2.126347437E-14 };
        yield return new object[] { 10.0, LengthUnit.Chain, LengthUnit.Parsec, 6.519410881E-15 };

        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Rod, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Meter, 50.292 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Nanometer, 50292000000 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Micrometer, 50292000 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Millimeter, 50292 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Centimeter, 5029.2 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Decimeter, 502.92 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Kilometer, 0.050292 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Inch, 1980.0 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Foot, 165.0 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Yard, 55.0 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Mile, 0.03125 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.NauticalMile, 0.0271555076 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Chain, 2.5 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.AstronomicalUnit, 3.361812555E-10 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.LightYear, 5.315868594E-15 };
        yield return new object[] { 10.0, LengthUnit.Rod, LengthUnit.Parsec, 1.62985272E-15 };

        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.AstronomicalUnit, 10.0 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Meter, 1495978706910 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Nanometer, 1.49597870691E+21 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Micrometer, 1.49597870691E+18 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Millimeter, 1495978706910000 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Centimeter, 149597870691000 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Decimeter, 14959787069100 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Kilometer, 1495978706.91 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Inch, 58896799484645.67 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Foot, 4908066623720.473 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Yard, 1636022207906.8242 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Mile, 929558072.6743319 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.NauticalMile, 807763880.6209503 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Chain, 74364645813.94655 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Rod, 297458583255.7862 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.LightYear, 0.0001581251 };
        yield return new object[] { 10.0, LengthUnit.AstronomicalUnit, LengthUnit.Parsec, 0.0000484814 };

        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.LightYear, 10.0 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Meter, 94607304725800450 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Nanometer, 9.460730472580045E+25 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Micrometer, 9.460730472580046E+22 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Millimeter, 94607304725800450000.0 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Centimeter, 9460730472580045000.0 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Decimeter, 946073047258004500.0 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Kilometer, 94607304725800.45 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Inch, 3724697036448836600 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Foot, 310391419704069700 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Yard, 103463806568023230 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Mile, 58786253731831.38 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.NauticalMile, 51083857843304.79 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Chain, 4702900298546510 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Rod, 18811601194186040 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.AstronomicalUnit, 632410.77088 };
        yield return new object[] { 10.0, LengthUnit.LightYear, LengthUnit.Parsec, 3.0660139381 };

        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Parsec, 10.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Meter, 308567758127995900.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Nanometer, 3.0856775812799587E+26 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Micrometer, 3.085677581279959E+23 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Millimeter, 308567758127995900000.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Centimeter, 30856775812799590000.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Decimeter, 3085677581279959000.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Kilometer, 308567758127995.9 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Inch, 12148336934173067000.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Foot, 1012361411181088900.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Yard, 337453803727029600.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Mile, 191735115753994.1 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.NauticalMile, 166613260328291.53 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Chain, 15338809260319528.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.Rod, 61355237041278110.0 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.AstronomicalUnit, 2062648.0624537375 };
        yield return new object[] { 10.0, LengthUnit.Parsec, LengthUnit.LightYear, 32.615637769 };
    }

    [Theory]
    [MemberData(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, LengthUnit unit, LengthUnit to, double expected)
    {
        var length = new LengthValue(new NumberValue(value), unit);
        var converted = length.To(to);

        Assert.Equal(expected, converted.Value.Number, 5);
    }
}