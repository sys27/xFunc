// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class LengthConverterTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new LengthConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var lengthValue = LengthValue.Meter(10);

        yield return new object[] { lengthValue, "m", lengthValue.ToMeter() };
        yield return new object[] { lengthValue, "nm", lengthValue.ToNanometer() };
        yield return new object[] { lengthValue, "µm", lengthValue.ToMicrometer() };
        yield return new object[] { lengthValue, "mm", lengthValue.ToMillimeter() };
        yield return new object[] { lengthValue, "cm", lengthValue.ToCentimeter() };
        yield return new object[] { lengthValue, "dm", lengthValue.ToDecimeter() };
        yield return new object[] { lengthValue, "km", lengthValue.ToKilometer() };
        yield return new object[] { lengthValue, "in", lengthValue.ToInch() };
        yield return new object[] { lengthValue, "ft", lengthValue.ToFoot() };
        yield return new object[] { lengthValue, "yd", lengthValue.ToYard() };
        yield return new object[] { lengthValue, "mi", lengthValue.ToMile() };
        yield return new object[] { lengthValue, "nmi", lengthValue.ToNauticalMile() };
        yield return new object[] { lengthValue, "ch", lengthValue.ToChain() };
        yield return new object[] { lengthValue, "rd", lengthValue.ToRod() };
        yield return new object[] { lengthValue, "au", lengthValue.ToAstronomicalUnit() };
        yield return new object[] { lengthValue, "ly", lengthValue.ToLightYear() };
        yield return new object[] { lengthValue, "pc", lengthValue.ToParsec() };

        var number = new NumberValue(10);

        yield return new object[] { number, "m", LengthValue.Meter(number) };
        yield return new object[] { number, "nm", LengthValue.Nanometer(number) };
        yield return new object[] { number, "µm", LengthValue.Micrometer(number) };
        yield return new object[] { number, "mm", LengthValue.Millimeter(number) };
        yield return new object[] { number, "cm", LengthValue.Centimeter(number) };
        yield return new object[] { number, "dm", LengthValue.Decimeter(number) };
        yield return new object[] { number, "km", LengthValue.Kilometer(number) };
        yield return new object[] { number, "in", LengthValue.Inch(number) };
        yield return new object[] { number, "ft", LengthValue.Foot(number) };
        yield return new object[] { number, "yd", LengthValue.Yard(number) };
        yield return new object[] { number, "mi", LengthValue.Mile(number) };
        yield return new object[] { number, "nmi", LengthValue.NauticalMile(number) };
        yield return new object[] { number, "ch", LengthValue.Chain(number) };
        yield return new object[] { number, "rd", LengthValue.Rod(number) };
        yield return new object[] { number, "au", LengthValue.AstronomicalUnit(number) };
        yield return new object[] { number, "ly", LengthValue.LightYear(number) };
        yield return new object[] { number, "pc", LengthValue.Parsec(number) };
    }

    [Theory]
    [MemberData(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new LengthConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.Equal(expected, result);
        Assert.Equal(expected, resultAsObject);
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return new object[] { LengthValue.Meter(10), "xxx" };
        yield return new object[] { new NumberValue(10), "xxx" };
    }

    [Theory]
    [MemberData(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new LengthConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Fact]
    public void ConvertUnsupportedValue()
    {
        var converter = new LengthConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "m"));
    }
}