// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.MassUnits;

/// <summary>
/// Represents a number with unit.
/// </summary>
public readonly struct MassValue : IEquatable<MassValue>, IComparable<MassValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MassValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public MassValue(NumberValue value, MassUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Kilogram</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Kilogram(double value)
        => Kilogram(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Kilogram</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Kilogram(NumberValue value)
        => new MassValue(value, MassUnit.Kilogram);

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Milligram</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Milligram(double value)
        => Milligram(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Milligram</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Milligram(NumberValue value)
        => new MassValue(value, MassUnit.Milligram);

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Gram</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Gram(double value)
        => Gram(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Gram</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Gram(NumberValue value)
        => new MassValue(value, MassUnit.Gram);

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Tonne</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Tonne(double value)
        => Tonne(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Tonne</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Tonne(NumberValue value)
        => new MassValue(value, MassUnit.Tonne);

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Ounce</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Ounce(double value)
        => Ounce(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Ounce</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Ounce(NumberValue value)
        => new MassValue(value, MassUnit.Ounce);

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Pound</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Pound(double value)
        => Pound(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="MassValue"/> struct with <c>Pound</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The mass value.</returns>
    public static MassValue Pound(NumberValue value)
        => new MassValue(value, MassUnit.Pound);

    /// <inheritdoc />
    public bool Equals(MassValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is MassValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(MassValue other)
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
            MassValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(MassValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} '{Unit.UnitName}'";

    /// <summary>
    /// Determines whether two specified instances of <see cref="MassValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MassValue left, MassValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="MassValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MassValue left, MassValue right)
        => !(left == right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(MassValue left, MassValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(MassValue left, MassValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(MassValue left, MassValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left value.</param>
    /// <param name="right">The right value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(MassValue left, MassValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="MassValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MassValue operator +(MassValue left, MassValue right)
    {
        right = right.To(left.Unit);

        return new MassValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="MassValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MassValue operator -(MassValue left, MassValue right)
    {
        right = right.To(left.Unit);

        return new MassValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="MassValue"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static MassValue operator -(MassValue value)
        => new MassValue(-value.Value, value.Unit);

    private MassValue ToBase()
        => new MassValue(Value * Unit.Factor, MassUnit.Kilogram);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public MassValue To(MassUnit newUnit)
    {
        var inBase = Value * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new MassValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the value to the kilogram unit.
    /// </summary>
    /// <returns>The value in kilogram unit.</returns>
    public MassValue ToKilogram()
        => To(MassUnit.Kilogram);

    /// <summary>
    /// Converts the value to the milligram unit.
    /// </summary>
    /// <returns>The value in milligram unit.</returns>
    public MassValue ToMilligram()
        => To(MassUnit.Milligram);

    /// <summary>
    /// Converts the value to the gram unit.
    /// </summary>
    /// <returns>The value in gram unit.</returns>
    public MassValue ToGram()
        => To(MassUnit.Gram);

    /// <summary>
    /// Converts the value to the tonne unit.
    /// </summary>
    /// <returns>The value in tonne unit.</returns>
    public MassValue ToTonne()
        => To(MassUnit.Tonne);

    /// <summary>
    /// Converts the value to the ounce unit.
    /// </summary>
    /// <returns>The value in ounce unit.</returns>
    public MassValue ToOunce()
        => To(MassUnit.Ounce);

    /// <summary>
    /// Converts the value to the pound unit.
    /// </summary>
    /// <returns>The value in pound unit.</returns>
    public MassValue ToPound()
        => To(MassUnit.Pound);

    /// <summary>
    /// Returns the absolute value of a specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static MassValue Abs(MassValue value)
        => new MassValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The smallest integral value.</returns>
    public static MassValue Ceiling(MassValue value)
        => new MassValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified value number.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The largest integral value.</returns>
    public static MassValue Floor(MassValue value)
        => new MassValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified value number.
    /// </summary>
    /// <param name="value">An value to truncate.</param>
    /// <returns>The integral part of value number.</returns>
    public static MassValue Truncate(MassValue value)
        => new MassValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the value number.
    /// </summary>
    /// <param name="value">The value number.</param>
    /// <returns>The fractional part.</returns>
    public static MassValue Frac(MassValue value)
        => new MassValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="massValue">The number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="massValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="massValue"/> is returned unchanged.</returns>
    public static MassValue Round(MassValue massValue, NumberValue digits)
        => new MassValue(NumberValue.Round(massValue.Value, digits), massValue.Unit);

    /// <summary>
    /// Converts <see cref="MassValue"/> to <see cref="Mass"/>.
    /// </summary>
    /// <returns>The mass number.</returns>
    public Mass AsExpression()
        => new Mass(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public MassUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}