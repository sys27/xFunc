// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TemperatureUnits;

public class TemperatureValueTest
{
    [Fact]
    public void EqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.True(temperature1.Equals(temperature2));
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.True(temperature1 == temperature2);
    }

    [Fact]
    public void NotEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.True(temperature1 != temperature2);
    }

    [Fact]
    public void LessTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.True(temperature1 < temperature2);
    }

    [Fact]
    public void LessFalseTest()
    {
        var temperature1 = TemperatureValue.Celsius(20);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.False(temperature1 < temperature2);
    }

    [Fact]
    public void GreaterTest()
    {
        var temperature1 = TemperatureValue.Celsius(20);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.True(temperature1 > temperature2);
    }

    [Fact]
    public void GreaterFalseTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.False(temperature1 > temperature2);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.True(temperature1 <= temperature2);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.True(temperature1 >= temperature2);
    }

    [Fact]
    public void CompareToNull()
    {
        var temperature = TemperatureValue.Celsius(10);

        Assert.True(temperature.CompareTo(null) > 0);
    }

    [Fact]
    public void CompareToObject()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = (object)TemperatureValue.Celsius(10);

        Assert.True(temperature1.CompareTo(temperature2) == 0);
    }

    [Fact]
    public void CompareToDouble()
    {
        var temperature = TemperatureValue.Celsius(10);

        Assert.Throws<ArgumentException>(() => temperature.CompareTo(1));
    }

    [Fact]
    public void ValueNotEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.False(temperature1.Equals(temperature2));
    }

    [Fact]
    public void UnitNotEqualTest2()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Fahrenheit(10);

        Assert.False(temperature1.Equals(temperature2));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = 3;

        Assert.False(temperature1.Equals(temperature2));
    }

    [Fact]
    public void EqualObjectTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.True(temperature1.Equals(temperature2 as object));
    }

    [Fact]
    public void NotEqualObjectTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(20);

        Assert.False(temperature1.Equals(temperature2 as object));
    }

    [Fact]
    public void AddOperatorTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Kelvin(10);
        var expected = TemperatureValue.Celsius(10 - 263.15);
        var actual = temperature1 + temperature2;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SubOperatorTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Kelvin(10);
        var expected = TemperatureValue.Celsius(10 + 263.15);
        var actual = temperature1 - temperature2;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToStringCelsiusTest()
    {
        var temperature = TemperatureValue.Celsius(10);

        Assert.Equal("10 °C", temperature.ToString());
    }

    [Fact]
    public void ToStringFahrenheitTest()
    {
        var temperature = TemperatureValue.Fahrenheit(10);

        Assert.Equal("10 °F", temperature.ToString());
    }

    [Fact]
    public void ToStringKelvinTest()
    {
        var temperature = TemperatureValue.Kelvin(10);

        Assert.Equal("10 K", temperature.ToString());
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, TemperatureUnit.Celsius, TemperatureUnit.Celsius, 10.0 };
        yield return new object[] { 10.0, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit, 50.0 };
        yield return new object[] { 10.0, TemperatureUnit.Celsius, TemperatureUnit.Kelvin, 283.15 };

        yield return new object[] { 10.0, TemperatureUnit.Fahrenheit, TemperatureUnit.Fahrenheit, 10.0 };
        yield return new object[] { 10.0, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius, -12.222222 };
        yield return new object[] { 10.0, TemperatureUnit.Fahrenheit, TemperatureUnit.Kelvin, 260.927778 };

        yield return new object[] { 10.0, TemperatureUnit.Kelvin, TemperatureUnit.Kelvin, 10.0 };
        yield return new object[] { 10.0, TemperatureUnit.Kelvin, TemperatureUnit.Celsius, -263.15 };
        yield return new object[] { 10.0, TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit, -441.66999999999996 };
    }

    [Theory]
    [MemberData(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, TemperatureUnit unit, TemperatureUnit to, double expected)
    {
        var temperatureValue = new TemperatureValue(new NumberValue(value), unit);
        var converted = temperatureValue.To(to);

        Assert.Equal(expected, converted.Value.Number, 6);
    }
}