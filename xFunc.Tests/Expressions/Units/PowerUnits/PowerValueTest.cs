// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.PowerUnits;

public class PowerValueTest
{
    [Fact]
    public void EqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.True(power1.Equals(power2));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.True(power1 == power2);
    }

    [Fact]
    public void NotEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.True(power1 != power2);
    }

    [Fact]
    public void LessTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.True(power1 < power2);
    }

    [Fact]
    public void LessFalseTest()
    {
        var power1 = PowerValue.Watt(20);
        var power2 = PowerValue.Watt(12);

        Assert.False(power1 < power2);
    }

    [Fact]
    public void GreaterTest()
    {
        var power1 = PowerValue.Watt(20);
        var power2 = PowerValue.Watt(12);

        Assert.True(power1 > power2);
    }

    [Fact]
    public void GreaterFalseTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.False(power1 > power2);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.True(power1 <= power2);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.True(power1 >= power2);
    }

    [Fact]
    public void CompareToNull()
    {
        var power = PowerValue.Watt(10);

        Assert.True(power.CompareTo(null) > 0);
    }

    [Fact]
    public void CompareToObject()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = (object)PowerValue.Watt(10);

        Assert.True(power1.CompareTo(power2) == 0);
    }

    [Fact]
    public void CompareToDouble()
    {
        var power = PowerValue.Watt(10);

        Assert.Throws<ArgumentException>(() => power.CompareTo(1));
    }

    [Fact]
    public void ValueNotEqualTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(12);

        Assert.False(power1.Equals(power2));
    }

    [Fact]
    public void UnitNotEqualTest2()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Kilowatt(10);

        Assert.False(power1.Equals(power2));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = 3;

        Assert.False(power1.Equals(power2));
    }

    [Fact]
    public void EqualObjectTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(10);

        Assert.True(power1.Equals(power2 as object));
    }

    [Fact]
    public void NotEqualObjectTest()
    {
        var power1 = PowerValue.Watt(10);
        var power2 = PowerValue.Watt(20);

        Assert.False(power1.Equals(power2 as object));
    }

    [Fact]
    public void ToStringWattTest()
    {
        var power = PowerValue.Watt(10);

        Assert.Equal("10 W", power.ToString());
    }

    [Fact]
    public void ToStringKilowattTest()
    {
        var power = PowerValue.Kilowatt(10);

        Assert.Equal("10 kW", power.ToString());
    }

    [Fact]
    public void ToStringHorsepowerTest()
    {
        var power = PowerValue.Horsepower(10);

        Assert.Equal("10 hp", power.ToString());
    }

    [Fact]
    public void ToStringUnsupported()
    {
        var power = new PowerValue(new NumberValue(10), (PowerUnit)10);

        Assert.Throws<InvalidOperationException>(() => power.ToString());
    }

    [Fact]
    public void WattToWattTest()
    {
        var power = PowerValue.Watt(10);
        var actual = power.To(PowerUnit.Watt);
        var expected = PowerValue.Watt(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WattToKilowattTest()
    {
        var power = PowerValue.Watt(10);
        var actual = power.To(PowerUnit.Kilowatt);
        var expected = PowerValue.Kilowatt(10.0 / 1000);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WattToHorsepowerTest()
    {
        var power = PowerValue.Watt(10);
        var actual = power.To(PowerUnit.Horsepower);
        var expected = PowerValue.Horsepower(10 / 745.69987158227022);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void KilowattToWattTest()
    {
        var power = PowerValue.Kilowatt(10);
        var actual = power.To(PowerUnit.Watt);
        var expected = PowerValue.Watt(10.0 * 1000.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void KilowattToKilowattTest()
    {
        var power = PowerValue.Kilowatt(10);
        var actual = power.To(PowerUnit.Kilowatt);
        var expected = PowerValue.Kilowatt(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void KilowattToHorsepowerTest()
    {
        var power = PowerValue.Kilowatt(10);
        var actual = power.To(PowerUnit.Horsepower);
        var expected = PowerValue.Horsepower(10 * 1000 / 745.69987158227022);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HorsepowerToWattTest()
    {
        var power = PowerValue.Horsepower(10);
        var actual = power.To(PowerUnit.Watt);
        var expected = PowerValue.Watt(10 * 745.69987158227022);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HorsepowerToKilowattTest()
    {
        var power = PowerValue.Horsepower(10);
        var actual = power.To(PowerUnit.Kilowatt);
        var expected = PowerValue.Kilowatt(10 * 745.69987158227022 / 1000);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HorsepowerToHorsepowerTest()
    {
        var power = PowerValue.Horsepower(10);
        var actual = power.To(PowerUnit.Horsepower);
        var expected = PowerValue.Horsepower(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToUnsupportedUnit()
    {
        var power = PowerValue.Watt(1);

        Assert.Throws<ArgumentOutOfRangeException>(() => power.To((PowerUnit)10));
    }

    [Fact]
    public void FromUnsupportedUnitToWatt()
    {
        var power = new PowerValue(new NumberValue(10), (PowerUnit)10);

        Assert.Throws<InvalidOperationException>(() => power.ToWatt());
    }

    [Fact]
    public void FromUnsupportedUnitToKilowatt()
    {
        var power = new PowerValue(new NumberValue(10), (PowerUnit)10);

        Assert.Throws<InvalidOperationException>(() => power.ToKilowatt());
    }

    [Fact]
    public void FromUnsupportedUnitToHorsepower()
    {
        var power = new PowerValue(new NumberValue(10), (PowerUnit)10);

        Assert.Throws<InvalidOperationException>(() => power.ToHorsepower());
    }

    [Theory]
    [InlineData(PowerUnit.Watt, PowerUnit.Watt, PowerUnit.Watt)]
    [InlineData(PowerUnit.Kilowatt, PowerUnit.Kilowatt, PowerUnit.Kilowatt)]
    [InlineData(PowerUnit.Horsepower, PowerUnit.Horsepower, PowerUnit.Horsepower)]
    [InlineData(PowerUnit.Watt, PowerUnit.Kilowatt, PowerUnit.Watt)]
    [InlineData(PowerUnit.Kilowatt, PowerUnit.Watt, PowerUnit.Watt)]
    [InlineData(PowerUnit.Watt, PowerUnit.Horsepower, PowerUnit.Watt)]
    [InlineData(PowerUnit.Horsepower, PowerUnit.Watt, PowerUnit.Watt)]
    [InlineData(PowerUnit.Kilowatt, PowerUnit.Horsepower, PowerUnit.Kilowatt)]
    [InlineData(PowerUnit.Horsepower, PowerUnit.Kilowatt, PowerUnit.Kilowatt)]
    public void CommonUnitsTests(PowerUnit left, PowerUnit right, PowerUnit expected)
    {
        var x = new PowerValue(new NumberValue(90), left);
        var y = new PowerValue(new NumberValue(90), right);
        var result = x + y;

        Assert.Equal(expected, result.Unit);
    }
}