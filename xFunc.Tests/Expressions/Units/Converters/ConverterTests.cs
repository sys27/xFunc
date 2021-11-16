// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class ConverterTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new Converter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var angle = AngleValue.Radian(90);

        yield return new object[] { angle, "rad", angle.ToRadian() };

        var power = PowerValue.Watt(10);

        yield return new object[] { power, "w", power.ToWatt() };

        var temperature = TemperatureValue.Celsius(10);

        yield return new object[] { temperature, "k", temperature.ToKelvin() };
    }

    [Theory]
    [MemberData(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new Converter();
        var result = converter.Convert(value, unit);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertUnsupportedUnit()
    {
        var converter = new Converter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(new NumberValue(10), "xxx"));
    }
}