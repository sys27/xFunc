// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class TimeConverterTests
{
    [Test]
    [TestCase(null, null)]
    [TestCase(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new TimeConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var lengthValue = TimeValue.Second(10);

        yield return [lengthValue, "s", lengthValue.ToSecond()];
        yield return [lengthValue, "ns", lengthValue.ToNanosecond()];
        yield return [lengthValue, "μs", lengthValue.ToMicrosecond()];
        yield return [lengthValue, "ms", lengthValue.ToMillisecond()];
        yield return [lengthValue, "min", lengthValue.ToMinute()];
        yield return [lengthValue, "h", lengthValue.ToHour()];
        yield return [lengthValue, "day", lengthValue.ToDay()];
        yield return [lengthValue, "week", lengthValue.ToWeek()];
        yield return [lengthValue, "year", lengthValue.ToYear()];

        var number = new NumberValue(10);

        yield return [number, "s", TimeValue.Second(number)];
        yield return [number, "ns", TimeValue.Nanosecond(number)];
        yield return [number, "μs", TimeValue.Microsecond(number)];
        yield return [number, "ms", TimeValue.Millisecond(number)];
        yield return [number, "min", TimeValue.Minute(number)];
        yield return [number, "h", TimeValue.Hour(number)];
        yield return [number, "day", TimeValue.Day(number)];
        yield return [number, "week", TimeValue.Week(number)];
        yield return [number, "year", TimeValue.Year(number)];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new TimeConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(resultAsObject, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return [TimeValue.Second(10), "xxx"];
        yield return [new NumberValue(10), "xxx"];
    }

    [Test]
    [TestCaseSource(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new TimeConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Test]
    public void ConvertUnsupportedValue()
    {
        var converter = new TimeConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "s"));
    }
}