// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.TimeUnits;

/// <summary>
/// Represents a number with unit.
/// </summary>
public readonly struct TimeValue : IEquatable<TimeValue>, IComparable<TimeValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public TimeValue(NumberValue value, TimeUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Second</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Second(double value)
        => Second(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Second</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Second(NumberValue value)
        => new TimeValue(value, TimeUnit.Second);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Nanosecond</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Nanosecond(double value)
        => Nanosecond(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Nanosecond</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Nanosecond(NumberValue value)
        => new TimeValue(value, TimeUnit.Nanosecond);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Microsecond</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Microsecond(double value)
        => Microsecond(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Microsecond</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Microsecond(NumberValue value)
        => new TimeValue(value, TimeUnit.Microsecond);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Millisecond</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Millisecond(double value)
        => Millisecond(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Millisecond</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Millisecond(NumberValue value)
        => new TimeValue(value, TimeUnit.Millisecond);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Minute</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Minute(double value)
        => Minute(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Minute</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Minute(NumberValue value)
        => new TimeValue(value, TimeUnit.Minute);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Hour</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Hour(double value)
        => Hour(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Hour</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Hour(NumberValue value)
        => new TimeValue(value, TimeUnit.Hour);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Day</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Day(double value)
        => Day(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Day</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Day(NumberValue value)
        => new TimeValue(value, TimeUnit.Day);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Week</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Week(double value)
        => Week(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Week</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Week(NumberValue value)
        => new TimeValue(value, TimeUnit.Week);

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Year</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Year(double value)
        => Year(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TimeValue"/> struct with <c>Year</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The time value.</returns>
    public static TimeValue Year(NumberValue value)
        => new TimeValue(value, TimeUnit.Year);

    /// <inheritdoc />
    public bool Equals(TimeValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is TimeValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(TimeValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.CompareTo(otherInBase.Value);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
        => obj switch
        {
            null => 1,
            TimeValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(TimeValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} '{Unit.UnitName}'";

    /// <summary>
    /// Determines whether two specified instances of <see cref="TimeValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(TimeValue left, TimeValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="TimeValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(TimeValue left, TimeValue right)
        => !(left == right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(TimeValue left, TimeValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(TimeValue left, TimeValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(TimeValue left, TimeValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(TimeValue left, TimeValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="TimeValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TimeValue operator +(TimeValue left, TimeValue right)
    {
        right = right.To(left.Unit);

        return new TimeValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="TimeValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TimeValue operator -(TimeValue left, TimeValue right)
    {
        right = right.To(left.Unit);

        return new TimeValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="TimeValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static TimeValue operator -(TimeValue value)
        => new TimeValue(-value.Value, value.Unit);

    private TimeValue ToBase()
        => new TimeValue(Value * Unit.Factor, TimeUnit.Second);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public TimeValue To(TimeUnit newUnit)
    {
        var inBase = Value * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new TimeValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the value to the second unit.
    /// </summary>
    /// <returns>The value in second unit.</returns>
    public TimeValue ToSecond()
        => To(TimeUnit.Second);

    /// <summary>
    /// Converts the value to the nanosecond unit.
    /// </summary>
    /// <returns>The value in nanosecond unit.</returns>
    public TimeValue ToNanosecond()
        => To(TimeUnit.Nanosecond);

    /// <summary>
    /// Converts the value to the microsecond unit.
    /// </summary>
    /// <returns>The value in microsecond unit.</returns>
    public TimeValue ToMicrosecond()
        => To(TimeUnit.Microsecond);

    /// <summary>
    /// Converts the value to the millisecond unit.
    /// </summary>
    /// <returns>The value in millisecond unit.</returns>
    public TimeValue ToMillisecond()
        => To(TimeUnit.Millisecond);

    /// <summary>
    /// Converts the value to the minute unit.
    /// </summary>
    /// <returns>The value in minute unit.</returns>
    public TimeValue ToMinute()
        => To(TimeUnit.Minute);

    /// <summary>
    /// Converts the value to the hour unit.
    /// </summary>
    /// <returns>The value in hour unit.</returns>
    public TimeValue ToHour()
        => To(TimeUnit.Hour);

    /// <summary>
    /// Converts the value to the day unit.
    /// </summary>
    /// <returns>The value in day unit.</returns>
    public TimeValue ToDay()
        => To(TimeUnit.Day);

    /// <summary>
    /// Converts the value to the week unit.
    /// </summary>
    /// <returns>The value in week unit.</returns>
    public TimeValue ToWeek()
        => To(TimeUnit.Week);

    /// <summary>
    /// Converts the value to the year unit.
    /// </summary>
    /// <returns>The value in year unit.</returns>
    public TimeValue ToYear()
        => To(TimeUnit.Year);

    /// <summary>
    /// Returns the absolute value of a specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static TimeValue Abs(TimeValue value)
        => new TimeValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The smallest integral value.</returns>
    public static TimeValue Ceiling(TimeValue value)
        => new TimeValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified value number.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The largest integral value.</returns>
    public static TimeValue Floor(TimeValue value)
        => new TimeValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified value number.
    /// </summary>
    /// <param name="value">An value to truncate.</param>
    /// <returns>The integral part of value number.</returns>
    public static TimeValue Truncate(TimeValue value)
        => new TimeValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the value number.
    /// </summary>
    /// <param name="value">The value number.</param>
    /// <returns>The fractional part.</returns>
    public static TimeValue Frac(TimeValue value)
        => new TimeValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="timeValue">The number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="timeValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="timeValue"/> is returned unchanged.</returns>
    public static TimeValue Round(TimeValue timeValue, NumberValue digits)
        => new TimeValue(NumberValue.Round(timeValue.Value, digits), timeValue.Unit);

    /// <summary>
    /// Converts <see cref="TimeValue"/> to <see cref="Time"/>.
    /// </summary>
    /// <returns>The length number.</returns>
    public Time AsExpression()
        => new Time(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public TimeUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}