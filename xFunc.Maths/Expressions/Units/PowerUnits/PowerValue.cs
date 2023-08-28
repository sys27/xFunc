// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.PowerUnits;

/// <summary>
/// Represents a number with power unit.
/// </summary>
public readonly struct PowerValue : IEquatable<PowerValue>, IComparable<PowerValue>, IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PowerValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public PowerValue(NumberValue value, PowerUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="PowerValue"/> struct with <c>Watt</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value.</returns>
    public static PowerValue Watt(double value)
        => new PowerValue(new NumberValue(value), PowerUnit.Watt);

    /// <summary>
    /// Creates the <see cref="PowerValue"/> struct with <c>Watt</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The power value.</returns>
    public static PowerValue Watt(NumberValue numberValue)
        => new PowerValue(numberValue, PowerUnit.Watt);

    /// <summary>
    /// Creates the <see cref="PowerValue"/> struct with <c>Kilowatt</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value.</returns>
    public static PowerValue Kilowatt(double value)
        => new PowerValue(new NumberValue(value), PowerUnit.Kilowatt);

    /// <summary>
    /// Creates the <see cref="PowerValue"/> struct with <c>Kilowatt</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The power value.</returns>
    public static PowerValue Kilowatt(NumberValue numberValue)
        => new PowerValue(numberValue, PowerUnit.Kilowatt);

    /// <summary>
    /// Creates the <see cref="PowerValue"/> struct with <c>Gradian</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The power value.</returns>
    public static PowerValue Horsepower(double value)
        => new PowerValue(new NumberValue(value), PowerUnit.Horsepower);

    /// <summary>
    /// Creates the <see cref="PowerValue"/> struct with <c>Gradian</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The power value.</returns>
    public static PowerValue Horsepower(NumberValue numberValue)
        => new PowerValue(numberValue, PowerUnit.Horsepower);

    /// <inheritdoc />
    public bool Equals(PowerValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is PowerValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(PowerValue other)
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
            PowerValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(PowerValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} '{Unit}'";

    /// <summary>
    /// Determines whether two specified instances of <see cref="PowerValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(PowerValue left, PowerValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="PowerValue"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(PowerValue left, PowerValue right)
        => !left.Equals(right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(PowerValue left, PowerValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(PowerValue left, PowerValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(PowerValue left, PowerValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(PowerValue left, PowerValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static PowerValue operator +(PowerValue left, PowerValue right)
    {
        right = right.To(left.Unit);

        return new PowerValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static PowerValue operator -(PowerValue left, PowerValue right)
    {
        right = right.To(left.Unit);

        return new PowerValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static PowerValue operator -(PowerValue value)
        => new PowerValue(-value.Value, value.Unit);

    private PowerValue ToBase()
        => new PowerValue(Value * Unit.Factor, PowerUnit.Watt);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public PowerValue To(PowerUnit newUnit)
    {
        var inBase = Value * Unit.Factor;
        var converted = inBase / newUnit.Factor;

        return new PowerValue(converted, newUnit);
    }

    /// <summary>
    /// Converts the current object to watt.
    /// </summary>
    /// <returns>The power value which is converted to watts.</returns>
    public PowerValue ToWatt()
        => To(PowerUnit.Watt);

    /// <summary>
    /// Converts the current object to kilowatt.
    /// </summary>
    /// <returns>The power value which is converted to kilowatts.</returns>
    public PowerValue ToKilowatt()
        => To(PowerUnit.Kilowatt);

    /// <summary>
    /// Converts the current object to horsepower.
    /// </summary>
    /// <returns>The power value which is converted to horsepowers.</returns>
    public PowerValue ToHorsepower()
        => To(PowerUnit.Horsepower);

    /// <summary>
    /// Returns the absolute value of a specified power value.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The power value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static PowerValue Abs(PowerValue value)
        => new PowerValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified power value.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The smallest integral value.</returns>
    public static PowerValue Ceiling(PowerValue value)
        => new PowerValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified power value number.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The largest integral value.</returns>
    public static PowerValue Floor(PowerValue value)
        => new PowerValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified power value number.
    /// </summary>
    /// <param name="value">An power value to truncate.</param>
    /// <returns>The integral part of power value number.</returns>
    public static PowerValue Truncate(PowerValue value)
        => new PowerValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the power value number.
    /// </summary>
    /// <param name="value">The power value number.</param>
    /// <returns>The fractional part.</returns>
    public static PowerValue Frac(PowerValue value)
        => new PowerValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="powerValue">The power number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="powerValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="powerValue"/> is returned unchanged.</returns>
    public static PowerValue Round(PowerValue powerValue, NumberValue digits)
        => new PowerValue(NumberValue.Round(powerValue.Value, digits), powerValue.Unit);

    /// <summary>
    /// Converts <see cref="PowerValue"/> to <see cref="Power"/>.
    /// </summary>
    /// <returns>The power number.</returns>
    public Power AsExpression()
        => new Power(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public PowerUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}