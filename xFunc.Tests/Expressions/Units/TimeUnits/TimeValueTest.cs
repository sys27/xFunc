// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TimeUnits;

public class TimeValueTest
{
    [Test]
    public void EqualTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(10);

        Assert.That(time1.Equals(time2), Is.True);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(10);

        Assert.That(time1 == time2, Is.True);
    }

    [Test]
    public void NotEqualTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(12);

        Assert.That(time1 != time2, Is.True);
    }

    [Test]
    public void LessTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(12);

        Assert.That(time1 < time2, Is.True);
    }

    [Test]
    public void LessFalseTest()
    {
        var time1 = TimeValue.Second(20);
        var time2 = TimeValue.Second(12);

        Assert.That(time1 < time2, Is.False);
    }

    [Test]
    public void GreaterTest()
    {
        var time1 = TimeValue.Second(20);
        var time2 = TimeValue.Second(12);

        Assert.That(time1 > time2, Is.True);
    }

    [Test]
    public void GreaterFalseTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(12);

        Assert.That(time1 > time2, Is.False);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(10);

        Assert.That(time1 <= time2, Is.True);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(10);

        Assert.That(time1 >= time2, Is.True);
    }

    [Test]
    public void CompareToNull()
    {
        var timeValue = TimeValue.Second(10);

        Assert.That(timeValue.CompareTo(null) > 0, Is.True);
    }

    [Test]
    public void CompareToObject()
    {
        var time1 = TimeValue.Second(10);
        var time2 = (object)TimeValue.Second(10);

        Assert.That(time1.CompareTo(time2) == 0, Is.True);
    }

    [Test]
    public void CompareToDouble()
    {
        var timeValue = TimeValue.Second(10);

        Assert.Throws<ArgumentException>(() => timeValue.CompareTo(1));
    }

    [Test]
    public void ValueNotEqualTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(12);

        Assert.That(time1.Equals(time2), Is.False);
    }

    [Test]
    public void UnitNotEqualTest2()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Minute(10);

        Assert.That(time1.Equals(time2), Is.False);
    }

    [Test]
    public void EqualDiffTypeTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = 3;

        Assert.That(time1.Equals(time2), Is.False);
    }

    [Test]
    public void EqualObjectTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(10);

        Assert.That(time1.Equals(time2 as object), Is.True);
    }

    [Test]
    public void NotEqualObjectTest()
    {
        var time1 = TimeValue.Second(10);
        var time2 = TimeValue.Second(20);

        Assert.That(time1.Equals(time2 as object), Is.False);
    }

    [Test]
    public void ToStringSecondTest()
    {
        var timeValue = TimeValue.Second(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 's'"));
    }

    [Test]
    public void ToStringNanosecondTest()
    {
        var timeValue = TimeValue.Nanosecond(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'ns'"));
    }

    [Test]
    public void ToStringMicrosecondTest()
    {
        var timeValue = TimeValue.Microsecond(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'μs'"));
    }

    [Test]
    public void ToStringMillisecondTest()
    {
        var timeValue = TimeValue.Millisecond(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'ms'"));
    }

    [Test]
    public void ToStringMinuteTest()
    {
        var timeValue = TimeValue.Minute(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'min'"));
    }

    [Test]
    public void ToStringHourTest()
    {
        var timeValue = TimeValue.Hour(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'h'"));
    }

    [Test]
    public void ToStringDayTest()
    {
        var timeValue = TimeValue.Day(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'day'"));
    }

    [Test]
    public void ToStringWeekTest()
    {
        var timeValue = TimeValue.Week(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'week'"));
    }

    [Test]
    public void ToStringYearTest()
    {
        var timeValue = TimeValue.Year(10);

        Assert.That(timeValue.ToString(), Is.EqualTo("10 'year'"));
    }

    [Test]
    public void AddOperatorTest()
    {
        var time1 = TimeValue.Second(1);
        var time2 = TimeValue.Minute(1);
        var expected = TimeValue.Second(61);
        var result = time1 + time2;

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SubOperatorTest()
    {
        var time1 = TimeValue.Minute(1);
        var time2 = TimeValue.Second(30);
        var expected = TimeValue.Minute(0.5);
        var result = time1 - time2;

        Assert.That(result, Is.EqualTo(expected));
    }

    public static IEnumerable<object[]> GetConversionTestCases()
    {
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Second, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Nanosecond, 10000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Microsecond, 10000000.0 };
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Millisecond, 10000.0 };
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Minute, 0.16666666666666666 };
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Hour, 0.002777777777777778 };
        yield return new object[] { 10.0, TimeUnit.Second, TimeUnit.Day, 0.0001157407407407407 };
        yield return new object[] { 100.0, TimeUnit.Second, TimeUnit.Week, 0.00016666666666666666 };
        yield return new object[] { 100000.0, TimeUnit.Second, TimeUnit.Year, 0.0031688088 };

        yield return new object[] { 10.0, TimeUnit.Nanosecond, TimeUnit.Nanosecond, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Nanosecond, TimeUnit.Second, 0.000000001 };
        yield return new object[] { 10.0, TimeUnit.Nanosecond, TimeUnit.Microsecond, 0.01 };
        yield return new object[] { 10.0, TimeUnit.Nanosecond, TimeUnit.Millisecond, 0.00001 };
        yield return new object[] { 1000000000000.0, TimeUnit.Nanosecond, TimeUnit.Minute, 16.666666667 };
        yield return new object[] { 1000000000000.0, TimeUnit.Nanosecond, TimeUnit.Hour, 0.2777777778 };
        yield return new object[] { 1000000000000.0, TimeUnit.Nanosecond, TimeUnit.Day, 0.0115740741 };
        yield return new object[] { 1000000000000.0, TimeUnit.Nanosecond, TimeUnit.Week, 0.0016534392 };
        yield return new object[] { 1000000000000.0, TimeUnit.Nanosecond, TimeUnit.Year, 0.0000316881 };

        yield return new object[] { 10.0, TimeUnit.Microsecond, TimeUnit.Microsecond, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Microsecond, TimeUnit.Second, 0.00001 };
        yield return new object[] { 10.0, TimeUnit.Microsecond, TimeUnit.Nanosecond, 10000.0 };
        yield return new object[] { 10.0, TimeUnit.Microsecond, TimeUnit.Millisecond, 0.01 };
        yield return new object[] { 1000000000.0, TimeUnit.Microsecond, TimeUnit.Minute, 16.666666667 };
        yield return new object[] { 1000000000.0, TimeUnit.Microsecond, TimeUnit.Hour, 0.2777777778 };
        yield return new object[] { 1000000000.0, TimeUnit.Microsecond, TimeUnit.Day, 0.0115740741 };
        yield return new object[] { 1000000000.0, TimeUnit.Microsecond, TimeUnit.Week, 0.0016534392 };
        yield return new object[] { 1000000000.0, TimeUnit.Microsecond, TimeUnit.Year, 0.0000316881 };

        yield return new object[] { 10.0, TimeUnit.Millisecond, TimeUnit.Millisecond, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Millisecond, TimeUnit.Second, 0.01 };
        yield return new object[] { 10.0, TimeUnit.Millisecond, TimeUnit.Nanosecond, 10000000.0 };
        yield return new object[] { 10.0, TimeUnit.Millisecond, TimeUnit.Microsecond, 10000.0 };
        yield return new object[] { 10.0, TimeUnit.Millisecond, TimeUnit.Minute, 0.0001666667 };
        yield return new object[] { 1000000.0, TimeUnit.Millisecond, TimeUnit.Hour, 0.2777777778 };
        yield return new object[] { 1000000.0, TimeUnit.Millisecond, TimeUnit.Day, 0.0115740741 };
        yield return new object[] { 1000000.0, TimeUnit.Millisecond, TimeUnit.Week, 0.0016534392 };
        yield return new object[] { 1000000.0, TimeUnit.Millisecond, TimeUnit.Year, 0.0000316881 };

        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Minute, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Second, 600.0 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Nanosecond, 600000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Microsecond, 600000000.0 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Millisecond, 600000.0 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Hour, 0.1666666667 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Day, 0.006944444444444444 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Week, 0.0009920635 };
        yield return new object[] { 10.0, TimeUnit.Minute, TimeUnit.Year, 0.0000190129 };

        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Hour, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Second, 36000.0 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Nanosecond, 36000000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Microsecond, 36000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Millisecond, 36000000.0 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Minute, 600.0 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Day, 0.4166666667 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Week, 0.0595238095 };
        yield return new object[] { 10.0, TimeUnit.Hour, TimeUnit.Year, 0.0011407712 };

        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Day, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Second, 864000.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Nanosecond, 864000000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Microsecond, 864000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Millisecond, 864000000.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Minute, 14400.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Hour, 240.0 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Week, 1.4285714286 };
        yield return new object[] { 10.0, TimeUnit.Day, TimeUnit.Year, 0.0273973 };

        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Week, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Second, 6048000.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Nanosecond, 6048000000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Microsecond, 6048000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Millisecond, 6048000000.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Minute, 100800.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Hour, 1680.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Day, 70.0 };
        yield return new object[] { 10.0, TimeUnit.Week, TimeUnit.Year, 0.1917808219178082 };

        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Year, 10.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Second, 315360000.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Nanosecond, 315360000000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Microsecond, 315360000000000.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Millisecond, 315360000000.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Minute, 5256000.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Hour, 87600.0 };
        yield return new object[] { 10.0, TimeUnit.Year, TimeUnit.Day, 3650.0 };
    }

    [Test]
    [TestCaseSource(nameof(GetConversionTestCases))]
    public void ConversionTests(double value, TimeUnit unit, TimeUnit to, double expected)
    {
        var time = new TimeValue(new NumberValue(value), unit);
        var converted = time.To(to);

        Assert.That(converted.Value.Number, Is.EqualTo(expected).Within(5));
    }
}