// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.VolumeUnits;

/// <summary>
/// Represents a number with unit.
/// </summary>
public readonly struct VolumeValue : IEquatable<VolumeValue>, IComparable<VolumeValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VolumeValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public VolumeValue(NumberValue value, VolumeUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Meter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Meter(double value)
        => Meter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Meter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Meter(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Meter);

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Centimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Centimeter(double value)
        => Centimeter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Centimeter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Centimeter(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Centimeter);

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Liter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Liter(double value)
        => Liter(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Liter</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Liter(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Liter);

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Inch</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Inch(double value)
        => Inch(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Inch</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Inch(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Inch);

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Foot</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Foot(double value)
        => Foot(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Foot</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Foot(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Foot);

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Yard</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Yard(double value)
        => Yard(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Yard</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Yard(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Yard);

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Gallon</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Gallon(double value)
        => Gallon(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="VolumeValue"/> struct with <c>Gallon</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The volume value.</returns>
    public static VolumeValue Gallon(NumberValue value)
        => new VolumeValue(value, VolumeUnit.Gallon);

    /// <inheritdoc />
    public bool Equals(VolumeValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is VolumeValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(VolumeValue other)
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
            VolumeValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(VolumeValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} '{Unit.UnitName}'";

    /// <summary>
    /// Determines whether two specified instances of <see cref="VolumeValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(VolumeValue left, VolumeValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="VolumeValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(VolumeValue left, VolumeValue right)
        => !(left == right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(VolumeValue left, VolumeValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(VolumeValue left, VolumeValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(VolumeValue left, VolumeValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(VolumeValue left, VolumeValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="VolumeValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VolumeValue operator +(VolumeValue left, VolumeValue right)
    {
        right = right.To(left.Unit);

        return new VolumeValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="VolumeValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VolumeValue operator -(VolumeValue left, VolumeValue right)
    {
        right = right.To(left.Unit);

        return new VolumeValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="VolumeValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static VolumeValue operator -(VolumeValue value)
        => new VolumeValue(-value.Value, value.Unit);

    private VolumeValue ToBase()
        => new VolumeValue(Value * Unit.Factor, VolumeUnit.Meter);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public VolumeValue To(VolumeUnit newUnit)
    {
        var inBase = Value * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new VolumeValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the value to the meter unit.
    /// </summary>
    /// <returns>The value in meter unit.</returns>
    public VolumeValue ToMeter()
        => To(VolumeUnit.Meter);

    /// <summary>
    /// Converts the value to the centimeter unit.
    /// </summary>
    /// <returns>The value in centimeter unit.</returns>
    public VolumeValue ToCentimeter()
        => To(VolumeUnit.Centimeter);

    /// <summary>
    /// Converts the value to the liter unit.
    /// </summary>
    /// <returns>The value in kilometer unit.</returns>
    public VolumeValue ToLiter()
        => To(VolumeUnit.Liter);

    /// <summary>
    /// Converts the value to the inch unit.
    /// </summary>
    /// <returns>The value in inch unit.</returns>
    public VolumeValue ToInch()
        => To(VolumeUnit.Inch);

    /// <summary>
    /// Converts the value to the foot unit.
    /// </summary>
    /// <returns>The value in foot unit.</returns>
    public VolumeValue ToFoot()
        => To(VolumeUnit.Foot);

    /// <summary>
    /// Converts the value to the yard unit.
    /// </summary>
    /// <returns>The value in yard unit.</returns>
    public VolumeValue ToYard()
        => To(VolumeUnit.Yard);

    /// <summary>
    /// Converts the value to the gallon unit.
    /// </summary>
    /// <returns>The value in mile unit.</returns>
    public VolumeValue ToGallon()
        => To(VolumeUnit.Gallon);

    /// <summary>
    /// Returns the absolute value of a specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static VolumeValue Abs(VolumeValue value)
        => new VolumeValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The smallest integral value.</returns>
    public static VolumeValue Ceiling(VolumeValue value)
        => new VolumeValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified value number.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The largest integral value.</returns>
    public static VolumeValue Floor(VolumeValue value)
        => new VolumeValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified value number.
    /// </summary>
    /// <param name="value">An value to truncate.</param>
    /// <returns>The integral part of value number.</returns>
    public static VolumeValue Truncate(VolumeValue value)
        => new VolumeValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the value number.
    /// </summary>
    /// <param name="value">The value number.</param>
    /// <returns>The fractional part.</returns>
    public static VolumeValue Frac(VolumeValue value)
        => new VolumeValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="volumeValue">The number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="volumeValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="volumeValue"/> is returned unchanged.</returns>
    public static VolumeValue Round(VolumeValue volumeValue, NumberValue digits)
        => new VolumeValue(NumberValue.Round(volumeValue.Value, digits), volumeValue.Unit);

    /// <summary>
    /// Converts <see cref="VolumeValue"/> to <see cref="Volume"/>.
    /// </summary>
    /// <returns>The volume number.</returns>
    public Volume AsExpression()
        => new Volume(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public VolumeUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}