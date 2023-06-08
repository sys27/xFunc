// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.AreaUnits;

/// <summary>
/// Represents a number with unit.
/// </summary>
public readonly struct AreaValue : IEquatable<AreaValue>, IComparable<AreaValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AreaValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public AreaValue(NumberValue value, AreaUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Meter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Meter(double value)
        => Meter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Meter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Meter(NumberValue value)
        => new AreaValue(value, AreaUnit.Meter);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Millimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Millimeter(double value)
        => Millimeter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Millimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Millimeter(NumberValue value)
        => new AreaValue(value, AreaUnit.Millimeter);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Centimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Centimeter(double value)
        => Centimeter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Centimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Centimeter(NumberValue value)
        => new AreaValue(value, AreaUnit.Centimeter);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Kilometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Kilometer(double value)
        => Kilometer(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Kilometer</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Kilometer(NumberValue value)
        => new AreaValue(value, AreaUnit.Kilometer);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Inch</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Inch(double value)
        => Inch(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Inch</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Inch(NumberValue value)
        => new AreaValue(value, AreaUnit.Inch);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Foot</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Foot(double value)
        => Foot(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Foot</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Foot(NumberValue value)
        => new AreaValue(value, AreaUnit.Foot);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Yard</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Yard(double value)
        => Yard(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Yard</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Yard(NumberValue value)
        => new AreaValue(value, AreaUnit.Yard);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Mile</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Mile(double value)
        => Mile(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Mile</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Mile(NumberValue value)
        => new AreaValue(value, AreaUnit.Mile);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Hectare</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Hectare(double value)
        => Hectare(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Hectare</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Hectare(NumberValue value)
        => new AreaValue(value, AreaUnit.Hectare);

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Acre</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Acre(double value)
        => Acre(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="AreaValue"/> struct with <c>Acre</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The area value.</returns>
    public static AreaValue Acre(NumberValue value)
        => new AreaValue(value, AreaUnit.Acre);

    /// <inheritdoc />
    public bool Equals(AreaValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is AreaValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(AreaValue other)
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
            AreaValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(AreaValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} {Unit.UnitName}";

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(AreaValue left, AreaValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(AreaValue left, AreaValue right)
        => !(left == right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(AreaValue left, AreaValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(AreaValue left, AreaValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(AreaValue left, AreaValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(AreaValue left, AreaValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="AreaValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static AreaValue operator +(AreaValue left, AreaValue right)
    {
        right = right.To(left.Unit);

        return new AreaValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="AreaValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static AreaValue operator -(AreaValue left, AreaValue right)
    {
        right = right.To(left.Unit);

        return new AreaValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="AreaValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static AreaValue operator -(AreaValue value)
        => new AreaValue(-value.Value, value.Unit);

    private AreaValue ToBase()
        => new AreaValue(Value * Unit.Factor, AreaUnit.Meter);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public AreaValue To(AreaUnit newUnit)
    {
        var inBase = Value * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new AreaValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the value to the meter unit.
    /// </summary>
    /// <returns>The value in meter unit.</returns>
    public AreaValue ToMeter()
        => To(AreaUnit.Meter);

    /// <summary>
    /// Converts the value to the millimeter unit.
    /// </summary>
    /// <returns>The value in millimeter unit.</returns>
    public AreaValue ToMillimeter()
        => To(AreaUnit.Millimeter);

    /// <summary>
    /// Converts the value to the centimeter unit.
    /// </summary>
    /// <returns>The value in centimeter unit.</returns>
    public AreaValue ToCentimeter()
        => To(AreaUnit.Centimeter);

    /// <summary>
    /// Converts the value to the kilometer unit.
    /// </summary>
    /// <returns>The value in kilometer unit.</returns>
    public AreaValue ToKilometer()
        => To(AreaUnit.Kilometer);

    /// <summary>
    /// Converts the value to the inch unit.
    /// </summary>
    /// <returns>The value in inch unit.</returns>
    public AreaValue ToInch()
        => To(AreaUnit.Inch);

    /// <summary>
    /// Converts the value to the foot unit.
    /// </summary>
    /// <returns>The value in foot unit.</returns>
    public AreaValue ToFoot()
        => To(AreaUnit.Foot);

    /// <summary>
    /// Converts the value to the yard unit.
    /// </summary>
    /// <returns>The value in yard unit.</returns>
    public AreaValue ToYard()
        => To(AreaUnit.Yard);

    /// <summary>
    /// Converts the value to the mile unit.
    /// </summary>
    /// <returns>The value in mile unit.</returns>
    public AreaValue ToMile()
        => To(AreaUnit.Mile);

    /// <summary>
    /// Converts the value to the hectare unit.
    /// </summary>
    /// <returns>The value in mile unit.</returns>
    public AreaValue ToHectare()
        => To(AreaUnit.Hectare);

    /// <summary>
    /// Converts the value to the acre unit.
    /// </summary>
    /// <returns>The value in mile unit.</returns>
    public AreaValue ToAcre()
        => To(AreaUnit.Acre);

    /// <summary>
    /// Returns the absolute value of a specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static AreaValue Abs(AreaValue value)
        => new AreaValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The smallest integral value.</returns>
    public static AreaValue Ceiling(AreaValue value)
        => new AreaValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified value number.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The largest integral value.</returns>
    public static AreaValue Floor(AreaValue value)
        => new AreaValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified value number.
    /// </summary>
    /// <param name="value">An value to truncate.</param>
    /// <returns>The integral part of value number.</returns>
    public static AreaValue Truncate(AreaValue value)
        => new AreaValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the value number.
    /// </summary>
    /// <param name="value">The value number.</param>
    /// <returns>The fractional part.</returns>
    public static AreaValue Frac(AreaValue value)
        => new AreaValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="areaValue">The number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="areaValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="areaValue"/> is returned unchanged.</returns>
    public static AreaValue Round(AreaValue areaValue, NumberValue digits)
        => new AreaValue(NumberValue.Round(areaValue.Value, digits), areaValue.Unit);

    /// <summary>
    /// Converts <see cref="AreaValue"/> to <see cref="Area"/>.
    /// </summary>
    /// <returns>The area number.</returns>
    public Area AsExpression()
        => new Area(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public AreaUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}