// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class AreaConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new AreaConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var areaValue = AreaValue.Meter(10);

        yield return [areaValue, "m^2", areaValue.ToMeter()];
        yield return [areaValue, "mm^2", areaValue.ToMillimeter()];
        yield return [areaValue, "cm^2", areaValue.ToCentimeter()];
        yield return [areaValue, "km^2", areaValue.ToKilometer()];
        yield return [areaValue, "in^2", areaValue.ToInch()];
        yield return [areaValue, "ft^2", areaValue.ToFoot()];
        yield return [areaValue, "yd^2", areaValue.ToYard()];
        yield return [areaValue, "mi^2", areaValue.ToMile()];
        yield return [areaValue, "ha", areaValue.ToHectare()];
        yield return [areaValue, "ac", areaValue.ToAcre()];

        var number = new NumberValue(10);

        yield return [number, "m^2", AreaValue.Meter(number)];
        yield return [number, "mm^2", AreaValue.Millimeter(number)];
        yield return [number, "cm^2", AreaValue.Centimeter(number)];
        yield return [number, "km^2", AreaValue.Kilometer(number)];
        yield return [number, "in^2", AreaValue.Inch(number)];
        yield return [number, "ft^2", AreaValue.Foot(number)];
        yield return [number, "yd^2", AreaValue.Yard(number)];
        yield return [number, "mi^2", AreaValue.Mile(number)];
        yield return [number, "ha", AreaValue.Hectare(number)];
        yield return [number, "ac", AreaValue.Acre(number)];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new AreaConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(resultAsObject, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return [AreaValue.Meter(10), "xxx"];
        yield return [new NumberValue(10), "xxx"];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new AreaConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new AreaConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "m^2"));
    }
}