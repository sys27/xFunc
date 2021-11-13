// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class AngleValueTest
{
    [Fact]
    public void EqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.True(angle1.Equals(angle2));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.True(angle1 == angle2);
    }

    [Fact]
    public void NotEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.True(angle1 != angle2);
    }

    [Fact]
    public void LessTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.True(angle1 < angle2);
    }

    [Fact]
    public void LessFalseTest()
    {
        var angle1 = AngleValue.Degree(20);
        var angle2 = AngleValue.Degree(12);

        Assert.False(angle1 < angle2);
    }

    [Fact]
    public void GreaterTest()
    {
        var angle1 = AngleValue.Degree(20);
        var angle2 = AngleValue.Degree(12);

        Assert.True(angle1 > angle2);
    }

    [Fact]
    public void GreaterFalseTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.False(angle1 > angle2);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.True(angle1 <= angle2);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.True(angle1 >= angle2);
    }

    [Fact]
    public void CompareToNull()
    {
        var angle = AngleValue.Degree(10);

        Assert.True(angle.CompareTo(null) > 0);
    }

    [Fact]
    public void CompareToObject()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = (object)AngleValue.Degree(10);

        Assert.True(angle1.CompareTo(angle2) == 0);
    }

    [Fact]
    public void CompareToDouble()
    {
        var angle = AngleValue.Degree(10);

        Assert.Throws<ArgumentException>(() => angle.CompareTo(1));
    }

    [Fact]
    public void ValueNotEqualTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(12);

        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void UnitNotEqualTest2()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Radian(10);

        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = 3;

        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void EqualObjectTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(10);

        Assert.True(angle1.Equals(angle2 as object));
    }

    [Fact]
    public void NotEqualObjectTest()
    {
        var angle1 = AngleValue.Degree(10);
        var angle2 = AngleValue.Degree(20);

        Assert.False(angle1.Equals(angle2 as object));
    }

    [Fact]
    public void ToStringDegreeTest()
    {
        var angle = AngleValue.Degree(10);

        Assert.Equal("10 degree", angle.ToString());
    }

    [Fact]
    public void ToStringRadianTest()
    {
        var angle = AngleValue.Radian(10);

        Assert.Equal("10 radian", angle.ToString());
    }

    [Fact]
    public void ToStringGradianTest()
    {
        var angle = AngleValue.Gradian(10);

        Assert.Equal("10 gradian", angle.ToString());
    }

    [Fact]
    public void ToStringUnsupported()
    {
        var angle = new AngleValue(new NumberValue(10), (AngleUnit)10);

        Assert.Throws<InvalidOperationException>(() => angle.ToString());
    }

    [Fact]
    public void DegreeToDegreeTest()
    {
        var angle = AngleValue.Degree(10);
        var actual = angle.To(AngleUnit.Degree);
        var expected = AngleValue.Degree(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DegreeToRadianTest()
    {
        var angle = AngleValue.Degree(10);
        var actual = angle.To(AngleUnit.Radian);
        var expected = AngleValue.Radian(10 * Math.PI / 180);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void DegreeToGradianTest()
    {
        var angle = AngleValue.Degree(10);
        var actual = angle.To(AngleUnit.Gradian);
        var expected = AngleValue.Gradian(10 / 0.9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RadianToDegreeTest()
    {
        var angle = AngleValue.Radian(10);
        var actual = angle.To(AngleUnit.Degree);
        var expected = AngleValue.Degree(10 * 180 / Math.PI);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RadianToRadianTest()
    {
        var angle = AngleValue.Radian(10);
        var actual = angle.To(AngleUnit.Radian);
        var expected = AngleValue.Radian(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RadianToGradianTest()
    {
        var angle = AngleValue.Radian(10);
        var actual = angle.To(AngleUnit.Gradian);
        var expected = AngleValue.Gradian(10 * 200 / Math.PI);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GradianToDegreeTest()
    {
        var angle = AngleValue.Gradian(10);
        var actual = angle.To(AngleUnit.Degree);
        var expected = AngleValue.Degree(10 * 0.9);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GradianToRadianTest()
    {
        var angle = AngleValue.Gradian(10);
        var actual = angle.To(AngleUnit.Radian);
        var expected = AngleValue.Radian(10 * Math.PI / 200);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GradianToGradianTest()
    {
        var angle = AngleValue.Gradian(10);
        var actual = angle.To(AngleUnit.Gradian);
        var expected = AngleValue.Gradian(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToUnsupportedUnit()
    {
        var angle = AngleValue.Degree(1);

        Assert.Throws<ArgumentOutOfRangeException>(() => angle.To((AngleUnit)10));
    }

    [Fact]
    public void FromUnsupportedUnitToWatt()
    {
        var angle = new AngleValue(new NumberValue(10), (AngleUnit)10);

        Assert.Throws<InvalidOperationException>(() => angle.ToDegree());
    }

    [Fact]
    public void FromUnsupportedUnitToKilowatt()
    {
        var angle = new AngleValue(new NumberValue(10), (AngleUnit)10);

        Assert.Throws<InvalidOperationException>(() => angle.ToRadian());
    }

    [Fact]
    public void FromUnsupportedUnitToHorsepower()
    {
        var angle = new AngleValue(new NumberValue(10), (AngleUnit)10);

        Assert.Throws<InvalidOperationException>(() => angle.ToGradian());
    }

    [Theory]
    [InlineData(AngleUnit.Gradian, AngleUnit.Gradian, AngleUnit.Gradian)]
    [InlineData(AngleUnit.Radian, AngleUnit.Gradian, AngleUnit.Radian)]
    [InlineData(AngleUnit.Gradian, AngleUnit.Radian, AngleUnit.Radian)]
    [InlineData(AngleUnit.Radian, AngleUnit.Degree, AngleUnit.Degree)]
    public void CommonUnitsTests(AngleUnit left, AngleUnit right, AngleUnit expected)
    {
        var x = new AngleValue(new NumberValue(90), left);
        var y = new AngleValue(new NumberValue(90), right);
        var result = x + y;

        Assert.Equal(expected, result.Unit);
    }

    [Theory]
    [InlineData(0, AngleUnit.Degree, 0)]
    [InlineData(0, AngleUnit.Radian, 0)]
    [InlineData(0, AngleUnit.Gradian, 0)]
    [InlineData(90, AngleUnit.Degree, 90)]
    [InlineData(1.5707963267948966, AngleUnit.Radian, 1.5707963267948966)]
    [InlineData(100, AngleUnit.Gradian, 100)]
    [InlineData(360, AngleUnit.Degree, 0)]
    [InlineData(6.283185307179586, AngleUnit.Radian, 0)]
    [InlineData(400, AngleUnit.Gradian, 0)]
    [InlineData(1110.0, AngleUnit.Degree, 30)]
    [InlineData(19.37315469713706, AngleUnit.Radian, 0.5235987755982988)]
    [InlineData(1233, AngleUnit.Gradian, 33)]
    [InlineData(1770.0, AngleUnit.Degree, 330)]
    [InlineData(30.892327760299633, AngleUnit.Radian, 5.759586531581287)]
    [InlineData(1966, AngleUnit.Gradian, 366)]
    [InlineData(-390.0, AngleUnit.Degree, 330)]
    [InlineData(-6.8067840827778845, AngleUnit.Radian, 5.759586531581287)]
    [InlineData(-434.0, AngleUnit.Gradian, 366)]
    public void NormalizeTests(double angleValue, AngleUnit unit, double expectedValue)
    {
        var angle = new AngleValue(new NumberValue(angleValue), unit);
        var normalized = angle.Normalize();
        var expected = new AngleValue(new NumberValue(expectedValue), unit);

        Assert.Equal(expected, normalized);
    }

    [Fact]
    public void NormalizeUnsupportedUnit()
    {
        var angle = new AngleValue(new NumberValue(10), (AngleUnit)10);

        Assert.Throws<InvalidOperationException>(() => angle.Normalize());
    }
}