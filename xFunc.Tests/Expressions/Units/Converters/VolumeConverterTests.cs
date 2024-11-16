// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class VolumeConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new VolumeConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var volumeValue = VolumeValue.Meter(10);

        yield return [volumeValue, "m^3", volumeValue.ToMeter()];
        yield return [volumeValue, "cm^3", volumeValue.ToCentimeter()];
        yield return [volumeValue, "l", volumeValue.ToLiter()];
        yield return [volumeValue, "in^3", volumeValue.ToInch()];
        yield return [volumeValue, "ft^3", volumeValue.ToFoot()];
        yield return [volumeValue, "yd^3", volumeValue.ToYard()];
        yield return [volumeValue, "gal", volumeValue.ToGallon()];

        var number = new NumberValue(10);

        yield return [number, "m^3", VolumeValue.Meter(number)];
        yield return [number, "cm^3", VolumeValue.Centimeter(number)];
        yield return [number, "l", VolumeValue.Liter(number)];
        yield return [number, "in^3", VolumeValue.Inch(number)];
        yield return [number, "ft^3", VolumeValue.Foot(number)];
        yield return [number, "yd^3", VolumeValue.Yard(number)];
        yield return [number, "gal", VolumeValue.Gallon(number)];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new VolumeConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(resultAsObject, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return [VolumeValue.Meter(10), "xxx"];
        yield return [new NumberValue(10), "xxx"];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new VolumeConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new VolumeConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "m^3"));
    }
}