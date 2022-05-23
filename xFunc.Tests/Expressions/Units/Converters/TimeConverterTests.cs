// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.Converters;

public class TimeConverterTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData(1, null)]
    public void ConvertNull(object value, string unit)
    {
        var converter = new TimeConverter();

        Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
    }

    public static IEnumerable<object[]> GetConvertTestsData()
    {
        var lengthValue = TimeValue.Second(10);

        yield return new object[] { lengthValue, "s", lengthValue.ToSecond() };
        yield return new object[] { lengthValue, "ns", lengthValue.ToNanosecond() };
        yield return new object[] { lengthValue, "μs", lengthValue.ToMicrosecond() };
        yield return new object[] { lengthValue, "ms", lengthValue.ToMillisecond() };
        yield return new object[] { lengthValue, "min", lengthValue.ToMinute() };
        yield return new object[] { lengthValue, "h", lengthValue.ToHour() };
        yield return new object[] { lengthValue, "day", lengthValue.ToDay() };
        yield return new object[] { lengthValue, "week", lengthValue.ToWeek() };
        yield return new object[] { lengthValue, "year", lengthValue.ToYear() };

        var number = new NumberValue(10);

        yield return new object[] { number, "s", TimeValue.Second(number) };
        yield return new object[] { number, "ns", TimeValue.Nanosecond(number) };
        yield return new object[] { number, "μs", TimeValue.Microsecond(number) };
        yield return new object[] { number, "ms", TimeValue.Millisecond(number) };
        yield return new object[] { number, "min", TimeValue.Minute(number) };
        yield return new object[] { number, "h", TimeValue.Hour(number) };
        yield return new object[] { number, "day", TimeValue.Day(number) };
        yield return new object[] { number, "week", TimeValue.Week(number) };
        yield return new object[] { number, "year", TimeValue.Year(number) };
    }

    [Theory]
    [MemberData(nameof(GetConvertTestsData))]
    public void ConvertTests(object value, string unit, object expected)
    {
        var converter = new TimeConverter();
        var result = converter.Convert(value, unit);
        var resultAsObject = ((IConverter<object>)converter).Convert(value, unit);

        Assert.Equal(expected, result);
        Assert.Equal(expected, resultAsObject);
    }

    public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
    {
        yield return new object[] { TimeValue.Second(10), "xxx" };
        yield return new object[] { new NumberValue(10), "xxx" };
    }

    [Theory]
    [MemberData(nameof(GetConvertUnsupportedUnitData))]
    public void ConvertUnsupportedUnit(object value, string unit)
    {
        var converter = new TimeConverter();

        Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
    }

    [Fact]
    public void ConvertUnsupportedValue()
    {
        var converter = new TimeConverter();

        Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "s"));
    }
}