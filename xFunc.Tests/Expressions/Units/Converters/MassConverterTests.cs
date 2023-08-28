// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class MassConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new MassConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var mass = MassValue.Gram(10);

        yield return new object[] { mass, "mg", mass.ToMilligram() };
        yield return new object[] { mass, "g", mass.ToGram() };
        yield return new object[] { mass, "kg", mass.ToKilogram() };
        yield return new object[] { mass, "t", mass.ToTonne() };
        yield return new object[] { mass, "oz", mass.ToOunce() };
        yield return new object[] { mass, "lb", mass.ToPound() };

        var number = new NumberValue(10);

        yield return new object[] { number, "mg", MassValue.Milligram(number) };
        yield return new object[] { number, "g", MassValue.Gram(number) };
        yield return new object[] { number, "kg", MassValue.Kilogram(number) };
        yield return new object[] { number, "t", MassValue.Tonne(number) };
        yield return new object[] { number, "oz", MassValue.Ounce(number) };
        yield return new object[] { number, "lb", MassValue.Pound(number) };
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new MassConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(resultAsObject, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return new object[] { MassValue.Gram(10), "xxx" };
        yield return new object[] { new NumberValue(10), "xxx" };
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new MassConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new MassConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "g"));
    }
}