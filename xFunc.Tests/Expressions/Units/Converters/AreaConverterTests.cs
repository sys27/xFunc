// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class AreaConverterTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new AreaConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var lengthValue = AreaValue.Meter(10);

        yield return new object[] { lengthValue, "m^2", lengthValue.ToMeter() };
        yield return new object[] { lengthValue, "mm^2", lengthValue.ToMillimeter() };
        yield return new object[] { lengthValue, "cm^2", lengthValue.ToCentimeter() };
        yield return new object[] { lengthValue, "km^2", lengthValue.ToKilometer() };
        yield return new object[] { lengthValue, "in^2", lengthValue.ToInch() };
        yield return new object[] { lengthValue, "ft^2", lengthValue.ToFoot() };
        yield return new object[] { lengthValue, "yd^2", lengthValue.ToYard() };
        yield return new object[] { lengthValue, "mi^2", lengthValue.ToMile() };
        yield return new object[] { lengthValue, "ha", lengthValue.ToHectare() };
        yield return new object[] { lengthValue, "ac", lengthValue.ToAcre() };

        var number = new NumberValue(10);

        yield return new object[] { number, "m^2", AreaValue.Meter(number) };
        yield return new object[] { number, "mm^2", AreaValue.Millimeter(number) };
        yield return new object[] { number, "cm^2", AreaValue.Centimeter(number) };
        yield return new object[] { number, "km^2", AreaValue.Kilometer(number) };
        yield return new object[] { number, "in^2", AreaValue.Inch(number) };
        yield return new object[] { number, "ft^2", AreaValue.Foot(number) };
        yield return new object[] { number, "yd^2", AreaValue.Yard(number) };
        yield return new object[] { number, "mi^2", AreaValue.Mile(number) };
        yield return new object[] { number, "ha", AreaValue.Hectare(number) };
        yield return new object[] { number, "ac", AreaValue.Acre(number) };
    }

    [Theory]
    [MemberData(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new AreaConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.Equal(expected, result);
        Assert.Equal(expected, resultAsObject);
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return new object[] { AreaValue.Meter(10), "xxx" };
        yield return new object[] { new NumberValue(10), "xxx" };
    }

    [Theory]
    [MemberData(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new AreaConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Fact]
    public void ConvertUnsupportedValue()
    {
        var converter = new AreaConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "m^2"));
    }
}