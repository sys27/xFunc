// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        => Value == other.Value && Unit == other.Unit;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is PowerValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(PowerValue other)
    {
        var valueComparison = Value.CompareTo(other.Value);
        if (valueComparison != 0)
            return valueComparison;

        return Unit.CompareTo(other.Unit);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is PowerValue other)
            return CompareTo(other);

        throw new ArgumentException($"Object must be of type {nameof(PowerValue)}");
    }

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(Value, (int)Unit);

    /// <inheritdoc />
    public override string ToString() => Unit switch
    {
        PowerUnit.Watt => $"{Value} W",
        PowerUnit.Kilowatt => $"{Value} kW",
        PowerUnit.Horsepower => $"{Value} hp",
        _ => throw new InvalidOperationException(),
    };

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
        (left, right) = ToCommonUnits(left, right);

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
        (left, right) = ToCommonUnits(left, right);

        return new PowerValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static PowerValue operator -(PowerValue value)
        => new PowerValue(-value.Value, value.Unit);

    /// <summary>
    /// Multiplies two objects of <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static PowerValue operator *(PowerValue left, PowerValue right)
    {
        (left, right) = ToCommonUnits(left, right);

        return new PowerValue(left.Value * right.Value, left.Unit);
    }

    /// <summary>
    /// Divides two objects of <see cref="PowerValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static PowerValue operator /(PowerValue left, PowerValue right)
    {
        (left, right) = ToCommonUnits(left, right);

        return new PowerValue(left.Value / right.Value, left.Unit);
    }

    private static (PowerValue Left, PowerValue Right) ToCommonUnits(PowerValue left, PowerValue right)
    {
        var commonUnit = GetCommonUnit(left.Unit, right.Unit);

        return (left.To(commonUnit), right.To(commonUnit));
    }

    private static PowerUnit GetCommonUnit(PowerUnit left, PowerUnit right)
        => (left, right) switch
        {
            _ when left == right => left,

            (PowerUnit.Kilowatt, PowerUnit.Horsepower) or
                (PowerUnit.Horsepower, PowerUnit.Kilowatt)
                => PowerUnit.Kilowatt,

            _ => PowerUnit.Watt,
        };

    /// <summary>
    /// Converts the current object to the specified <paramref name="unit"/>.
    /// </summary>
    /// <param name="unit">The unit to convert to.</param>
    /// <returns>The power value which is converted to the specified <paramref name="unit"/>.</returns>
    public PowerValue To(PowerUnit unit) => unit switch
    {
        PowerUnit.Watt => ToWatt(),
        PowerUnit.Kilowatt => ToKilowatt(),
        PowerUnit.Horsepower => ToHorsepower(),
        _ => throw new ArgumentOutOfRangeException(nameof(unit)),
    };

    /// <summary>
    /// Converts the current object to degrees.
    /// </summary>
    /// <returns>The power value which is converted to degrees.</returns>
    public PowerValue ToWatt() => Unit switch
    {
        PowerUnit.Watt => this,
        PowerUnit.Kilowatt => Watt(Value * 1000.0),
        PowerUnit.Horsepower => Watt(Value * 745.69987158227022),
        _ => throw new InvalidOperationException(),
    };

    /// <summary>
    /// Converts the current object to radians.
    /// </summary>
    /// <returns>The power value which is converted to radians.</returns>
    public PowerValue ToKilowatt() => Unit switch
    {
        PowerUnit.Watt => Kilowatt(Value / 1000.0),
        PowerUnit.Kilowatt => this,
        PowerUnit.Horsepower => Kilowatt(Value * 745.69987158227022 / 1000),
        _ => throw new InvalidOperationException(),
    };

    /// <summary>
    /// Converts the current object to gradians.
    /// </summary>
    /// <returns>The power value which is converted to gradians.</returns>
    public PowerValue ToHorsepower() => Unit switch
    {
        PowerUnit.Watt => Horsepower(Value / 745.69987158227022),
        PowerUnit.Kilowatt => Horsepower(Value / 745.69987158227022 * 1000),
        PowerUnit.Horsepower => this,
        _ => throw new InvalidOperationException(),
    };

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
    /// Converts <see cref="PowerValue"/> to <see cref="Power"/>.
    /// </summary>
    /// <returns>The angle number.</returns>
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