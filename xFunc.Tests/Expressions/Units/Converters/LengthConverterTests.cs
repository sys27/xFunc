// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class LengthConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new LengthConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var lengthValue = LengthValue.Meter(10);

        yield return [lengthValue, "m", lengthValue.ToMeter()];
        yield return [lengthValue, "nm", lengthValue.ToNanometer()];
        yield return [lengthValue, "µm", lengthValue.ToMicrometer()];
        yield return [lengthValue, "mm", lengthValue.ToMillimeter()];
        yield return [lengthValue, "cm", lengthValue.ToCentimeter()];
        yield return [lengthValue, "dm", lengthValue.ToDecimeter()];
        yield return [lengthValue, "km", lengthValue.ToKilometer()];
        yield return [lengthValue, "in", lengthValue.ToInch()];
        yield return [lengthValue, "ft", lengthValue.ToFoot()];
        yield return [lengthValue, "yd", lengthValue.ToYard()];
        yield return [lengthValue, "mi", lengthValue.ToMile()];
        yield return [lengthValue, "nmi", lengthValue.ToNauticalMile()];
        yield return [lengthValue, "ch", lengthValue.ToChain()];
        yield return [lengthValue, "rd", lengthValue.ToRod()];
        yield return [lengthValue, "au", lengthValue.ToAstronomicalUnit()];
        yield return [lengthValue, "ly", lengthValue.ToLightYear()];
        yield return [lengthValue, "pc", lengthValue.ToParsec()];

        var number = new NumberValue(10);

        yield return [number, "m", LengthValue.Meter(number)];
        yield return [number, "nm", LengthValue.Nanometer(number)];
        yield return [number, "µm", LengthValue.Micrometer(number)];
        yield return [number, "mm", LengthValue.Millimeter(number)];
        yield return [number, "cm", LengthValue.Centimeter(number)];
        yield return [number, "dm", LengthValue.Decimeter(number)];
        yield return [number, "km", LengthValue.Kilometer(number)];
        yield return [number, "in", LengthValue.Inch(number)];
        yield return [number, "ft", LengthValue.Foot(number)];
        yield return [number, "yd", LengthValue.Yard(number)];
        yield return [number, "mi", LengthValue.Mile(number)];
        yield return [number, "nmi", LengthValue.NauticalMile(number)];
        yield return [number, "ch", LengthValue.Chain(number)];
        yield return [number, "rd", LengthValue.Rod(number)];
        yield return [number, "au", LengthValue.AstronomicalUnit(number)];
        yield return [number, "ly", LengthValue.LightYear(number)];
        yield return [number, "pc", LengthValue.Parsec(number)];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new LengthConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(resultAsObject, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return [LengthValue.Meter(10), "xxx"];
        yield return [new NumberValue(10), "xxx"];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new LengthConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new LengthConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "m"));
    }
}