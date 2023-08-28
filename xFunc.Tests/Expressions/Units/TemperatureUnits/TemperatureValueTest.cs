// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TemperatureUnits;

public class TemperatureValueTest
{
    [Test]
    public void EqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.That(temperature1.Equals(temperature2), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.That(temperature1 == temperature2, Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.That(temperature1 != temperature2, Is.True);
    }

    [Test]
    public void LessTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.That(temperature1 < temperature2, Is.True);
    }

    [Test]
    public void LessFalseTest()
    {
        var temperature1 = TemperatureValue.Celsius(20);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.That(temperature1 < temperature2, Is.False);
    }

    [Test]
    public void GreaterTest()
    {
        var temperature1 = TemperatureValue.Celsius(20);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.That(temperature1 > temperature2, Is.True);
    }

    [Test]
    public void GreaterFalseTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.That(temperature1 > temperature2, Is.False);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.That(temperature1 <= temperature2, Is.True);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.That(temperature1 >= temperature2, Is.True);
    }

    [Test]
    public void CompareToNull()
    {
        var temperature = TemperatureValue.Celsius(10);

        Assert.That(temperature.CompareTo(null) > 0, Is.True);
    }

    [Test]
    public void CompareToObject()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = (object)TemperatureValue.Celsius(10);

        Assert.That(temperature1.CompareTo(temperature2) == 0, Is.True);
    }

    [Test]
    public void CompareToDouble()
    {
        var temperature = TemperatureValue.Celsius(10);

        Assert.Throws<ArgumentException>(() => temperature.CompareTo(1));
    }

    [Test]
    public void ValueNotEqualTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(12);

        Assert.That(temperature1.Equals(temperature2), Is.False);
    }

    [Test]
    public void UnitNotEqualTest2()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Fahrenheit(10);

        Assert.That(temperature1.Equals(temperature2), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = 3;

        Assert.That(temperature1.Equals(temperature2), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(10);

        Assert.That(temperature1.Equals(temperature2 as object), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Celsius(20);

        Assert.That(temperature1.Equals(temperature2 as object), Is.False);
    }

    [Test]
    public void AddOperatorTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Kelvin(10);
        var expected = TemperatureValue.Celsius(10 - 263.15);
        var actual = temperature1 + temperature2;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void SubOperatorTest()
    {
        var temperature1 = TemperatureValue.Celsius(10);
        var temperature2 = TemperatureValue.Kelvin(10);
        var expected = TemperatureValue.Celsius(10 + 263.15);
        var actual = temperature1 - temperature2;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToStringCelsiusTest()
    {
        var temperature = TemperatureValue.Celsius(10);

        Assert.That(temperature.ToString(), Is.EqualTo("10 °C"));
    }

    [Test]
    public void ToStringFahrenheitTest()
    {
        var temperature = TemperatureValue.Fahrenheit(10);

        Assert.That(temperature.ToString(), Is.EqualTo("10 °F"));
    }

    [Test]
    public void ToStringKelvinTest()
    {
        var temperature = TemperatureValue.Kelvin(10);

        Assert.That(temperature.ToString(), Is.EqualTo("10 K"));
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

    [Test]
    [TestCaseSource(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, TemperatureUnit unit, TemperatureUnit to, double expected)
    {
        var temperatureValue = new TemperatureValue(new NumberValue(value), unit);
        var converted = temperatureValue.To(to);

        Assert.That(converted.Value.Number, Is.EqualTo(expected).Within(6));
    }
}