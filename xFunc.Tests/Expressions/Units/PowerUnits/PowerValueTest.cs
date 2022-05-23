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

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, PowerUnit.Watt, PowerUnit.Watt, 10.0 };
        yield return new object[] { 10.0, PowerUnit.Watt, PowerUnit.Kilowatt, 10.0 / 1000 };
        yield return new object[] { 10.0, PowerUnit.Watt, PowerUnit.Horsepower, 10.0 / 745.69987158227022 };
        yield return new object[] { 10.0, PowerUnit.Kilowatt, PowerUnit.Kilowatt, 10.0 };
        yield return new object[] { 10.0, PowerUnit.Kilowatt, PowerUnit.Watt, 10.0 * 1000 };
        yield return new object[] { 10.0, PowerUnit.Kilowatt, PowerUnit.Horsepower, 10.0 * 1000 / 745.69987158227022 };
        yield return new object[] { 10.0, PowerUnit.Horsepower, PowerUnit.Horsepower, 10.0 };
        yield return new object[] { 10.0, PowerUnit.Horsepower, PowerUnit.Watt, 10.0 * 745.69987158227022 };
        yield return new object[] { 10.0, PowerUnit.Horsepower, PowerUnit.Kilowatt, 10.0 * 745.69987158227022 / 1000 };
    }

    [Theory]
    [MemberData(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, PowerUnit unit, PowerUnit to, double expected)
    {
        var temperatureValue = new PowerValue(new NumberValue(value), unit);
        var converted = temperatureValue.To(to);

        Assert.Equal(expected, converted.Value.Number, 6);
    }
}