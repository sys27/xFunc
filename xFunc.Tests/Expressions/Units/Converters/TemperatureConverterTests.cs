// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class TemperatureConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new TemperatureConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var temperature = TemperatureValue.Celsius(10);

        yield return [temperature, "°c", temperature.ToCelsius()];
        yield return [temperature, "°f", temperature.ToFahrenheit()];
        yield return [temperature, "k", temperature.ToKelvin()];

        var number = new NumberValue(10);

        yield return [number, "°c", TemperatureValue.Celsius(number)];
        yield return [number, "°f", TemperatureValue.Fahrenheit(number)];
        yield return [number, "k", TemperatureValue.Kelvin(number)];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new TemperatureConverter();
        var result = converter.Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return [TemperatureValue.Celsius(10), "xxx"];
        yield return [new NumberValue(10), "xxx"];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new TemperatureConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new TemperatureConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "K"));
    }
}