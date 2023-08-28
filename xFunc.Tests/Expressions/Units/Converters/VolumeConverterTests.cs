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

        yield return new object[] { volumeValue, "m^3", volumeValue.ToMeter() };
        yield return new object[] { volumeValue, "cm^3", volumeValue.ToCentimeter() };
        yield return new object[] { volumeValue, "l", volumeValue.ToLiter() };
        yield return new object[] { volumeValue, "in^3", volumeValue.ToInch() };
        yield return new object[] { volumeValue, "ft^3", volumeValue.ToFoot() };
        yield return new object[] { volumeValue, "yd^3", volumeValue.ToYard() };
        yield return new object[] { volumeValue, "gal", volumeValue.ToGallon() };

        var number = new NumberValue(10);

        yield return new object[] { number, "m^3", VolumeValue.Meter(number) };
        yield return new object[] { number, "cm^3", VolumeValue.Centimeter(number) };
        yield return new object[] { number, "l", VolumeValue.Liter(number) };
        yield return new object[] { number, "in^3", VolumeValue.Inch(number) };
        yield return new object[] { number, "ft^3", VolumeValue.Foot(number) };
        yield return new object[] { number, "yd^3", VolumeValue.Yard(number) };
        yield return new object[] { number, "gal", VolumeValue.Gallon(number) };
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
        yield return new object[] { VolumeValue.Meter(10), "xxx" };
        yield return new object[] { new NumberValue(10), "xxx" };
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