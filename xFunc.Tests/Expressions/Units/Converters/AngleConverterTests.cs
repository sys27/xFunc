// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class AngleConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new AngleConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var angle = AngleValue.Degree(90);

        yield return [angle, "rad", angle.ToRadian()];
        yield return [angle, "radian", angle.ToRadian()];
        yield return [angle, "radians", angle.ToRadian()];
        yield return [angle, "deg", angle.ToDegree()];
        yield return [angle, "degree", angle.ToDegree()];
        yield return [angle, "degrees", angle.ToDegree()];
        yield return [angle, "grad", angle.ToGradian()];
        yield return [angle, "gradian", angle.ToGradian()];
        yield return [angle, "gradians", angle.ToGradian()];

        var number = new NumberValue(10);

        yield return [number, "rad", AngleValue.Radian(number)];
        yield return [number, "radian", AngleValue.Radian(number)];
        yield return [number, "radians", AngleValue.Radian(number)];
        yield return [number, "deg", AngleValue.Degree(number)];
        yield return [number, "degree", AngleValue.Degree(number)];
        yield return [number, "degrees", AngleValue.Degree(number)];
        yield return [number, "grad", AngleValue.Gradian(number)];
        yield return [number, "gradian", AngleValue.Gradian(number)];
        yield return [number, "gradians", AngleValue.Gradian(number)];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new AngleConverter();
        var result = converter.Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return [AngleValue.Degree(90), "xxx"];
        yield return [new NumberValue(10), "xxx"];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new AngleConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new AngleConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "deg"));
    }
}