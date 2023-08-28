// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class PowerConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new PowerConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var power = PowerValue.Watt(10);

        yield return new object[] { power, "w", power.ToWatt() };
        yield return new object[] { power, "kw", power.ToKilowatt() };
        yield return new object[] { power, "hp", power.ToHorsepower() };

        var number = new NumberValue(10);

        yield return new object[] { number, "w", PowerValue.Watt(number) };
        yield return new object[] { number, "kw", PowerValue.Kilowatt(number) };
        yield return new object[] { number, "hp", PowerValue.Horsepower(number) };
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new PowerConverter();
        var result = converter.Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return new object[] { PowerValue.Watt(10), "xxx" };
        yield return new object[] { new NumberValue(10), "xxx" };
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new PowerConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new PowerConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "hp"));
    }
}