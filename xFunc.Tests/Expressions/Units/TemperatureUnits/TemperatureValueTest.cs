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

    [Fact]
    public void ToStringUnsupported()
    {
        var temperature = new TemperatureValue(new NumberValue(10), (TemperatureUnit)10);

        Assert.Throws<InvalidOperationException>(() => temperature.ToString());
    }

    [Fact]
    public void CelsiusToCelsiusTest()
    {
        var temperature = TemperatureValue.Celsius(10);
        var actual = temperature.To(TemperatureUnit.Celsius);
        var expected = TemperatureValue.Celsius(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CelsiusToFahrenheitTest()
    {
        var temperature = TemperatureValue.Celsius(10);
        var actual = temperature.To(TemperatureUnit.Fahrenheit);
        var expected = TemperatureValue.Fahrenheit(50);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CelsiusToKelvinTest()
    {
        var temperature = TemperatureValue.Celsius(10);
        var actual = temperature.To(TemperatureUnit.Kelvin);
        var expected = TemperatureValue.Kelvin(283.15);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FahrenheitToCelsiusTest()
    {
        var temperature = TemperatureValue.Fahrenheit(10);
        var actual = temperature.To(TemperatureUnit.Celsius);
        var expected = TemperatureValue.Celsius(-12.222222222222221);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FahrenheitToFahrenheitTest()
    {
        var temperature = TemperatureValue.Fahrenheit(10);
        var actual = temperature.To(TemperatureUnit.Fahrenheit);
        var expected = TemperatureValue.Fahrenheit(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FahrenheitToKelvinTest()
    {
        var temperature = TemperatureValue.Fahrenheit(10);
        var actual = temperature.To(TemperatureUnit.Kelvin);
        var expected = TemperatureValue.Kelvin(260.92777777777775);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void KelvinToCelsiusTest()
    {
        var temperature = TemperatureValue.Kelvin(10);
        var actual = temperature.To(TemperatureUnit.Celsius);
        var expected = TemperatureValue.Celsius(-263.15);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void KelvinToFahrenheitTest()
    {
        var temperature = TemperatureValue.Kelvin(10);
        var actual = temperature.To(TemperatureUnit.Fahrenheit);
        var expected = TemperatureValue.Fahrenheit(-441.66999999999996);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void KelvinToKelvinTest()
    {
        var temperature = TemperatureValue.Kelvin(10);
        var actual = temperature.To(TemperatureUnit.Kelvin);
        var expected = TemperatureValue.Kelvin(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToUnsupportedUnit()
    {
        var temperature = TemperatureValue.Celsius(1);

        Assert.Throws<ArgumentOutOfRangeException>(() => temperature.To((TemperatureUnit)10));
    }

    [Fact]
    public void FromUnsupportedUnitToCelsius()
    {
        var temperature = new TemperatureValue(new NumberValue(10), (TemperatureUnit)10);

        Assert.Throws<InvalidOperationException>(() => temperature.ToCelsius());
    }

    [Fact]
    public void FromUnsupportedUnitToFahrenheit()
    {
        var temperature = new TemperatureValue(new NumberValue(10), (TemperatureUnit)10);

        Assert.Throws<InvalidOperationException>(() => temperature.ToFahrenheit());
    }

    [Fact]
    public void FromUnsupportedUnitToKelvin()
    {
        var temperature = new TemperatureValue(new NumberValue(10), (TemperatureUnit)10);

        Assert.Throws<InvalidOperationException>(() => temperature.ToKelvin());
    }

    [Theory]
    [InlineData(TemperatureUnit.Celsius, TemperatureUnit.Celsius, TemperatureUnit.Celsius)]
    [InlineData(TemperatureUnit.Fahrenheit, TemperatureUnit.Fahrenheit, TemperatureUnit.Fahrenheit)]
    [InlineData(TemperatureUnit.Kelvin, TemperatureUnit.Kelvin, TemperatureUnit.Kelvin)]
    [InlineData(TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius)]
    [InlineData(TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius, TemperatureUnit.Celsius)]
    [InlineData(TemperatureUnit.Celsius, TemperatureUnit.Kelvin, TemperatureUnit.Celsius)]
    [InlineData(TemperatureUnit.Kelvin, TemperatureUnit.Celsius, TemperatureUnit.Celsius)]
    [InlineData(TemperatureUnit.Fahrenheit, TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit)]
    [InlineData(TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit, TemperatureUnit.Fahrenheit)]
    public void CommonUnitsTests(TemperatureUnit left, TemperatureUnit right, TemperatureUnit expected)
    {
        var x = new TemperatureValue(new NumberValue(90), left);
        var y = new TemperatureValue(new NumberValue(90), right);
        var result = x + y;

        Assert.Equal(expected, result.Unit);
    }
}