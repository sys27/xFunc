// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.TemperatureUnits;

/// <summary>
/// Represents a number with temperature unit.
/// </summary>
public readonly struct TemperatureValue :
    IEquatable<TemperatureValue>,
    IComparable<TemperatureValue>,
    IComparable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TemperatureValue"/> struct.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="unit">The unit of number.</param>
    public TemperatureValue(NumberValue value, TemperatureUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Creates the <see cref="TemperatureValue"/> struct with <c>Celsius</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The temperature value.</returns>
    public static TemperatureValue Celsius(double value)
        => Celsius(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TemperatureValue"/> struct with <c>Celsius</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The temperature value.</returns>
    public static TemperatureValue Celsius(NumberValue numberValue)
        => new TemperatureValue(numberValue, TemperatureUnit.Celsius);

    /// <summary>
    /// Creates the <see cref="TemperatureValue"/> struct with <c>Fahrenheit</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The temperature value.</returns>
    public static TemperatureValue Fahrenheit(double value)
        => Fahrenheit(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TemperatureValue"/> struct with <c>Fahrenheit</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The temperature value.</returns>
    public static TemperatureValue Fahrenheit(NumberValue numberValue)
        => new TemperatureValue(numberValue, TemperatureUnit.Fahrenheit);

    /// <summary>
    /// Creates the <see cref="TemperatureValue"/> struct with <c>Kelvin</c> unit.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The temperature value.</returns>
    public static TemperatureValue Kelvin(double value)
        => Kelvin(new NumberValue(value));

    /// <summary>
    /// Creates the <see cref="TemperatureValue"/> struct with <c>Kelvin</c> unit.
    /// </summary>
    /// <param name="numberValue">The value.</param>
    /// <returns>The temperature value.</returns>
    public static TemperatureValue Kelvin(NumberValue numberValue)
        => new TemperatureValue(numberValue, TemperatureUnit.Kelvin);

    /// <inheritdoc />
    public bool Equals(TemperatureValue other)
    {
        var inBase = ToBase();
        var otherInBase = other.ToBase();

        return inBase.Value.Equals(otherInBase.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is TemperatureValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(TemperatureValue other)
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
            TemperatureValue other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(TemperatureValue)}"),
        };

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Value, Unit);

    /// <inheritdoc />
    public override string ToString()
        => $"{Value} '{Unit.UnitName}'";

    /// <summary>
    /// Determines whether two specified instances of <see cref="TemperatureValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(TemperatureValue left, TemperatureValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="TemperatureValue"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(TemperatureValue left, TemperatureValue right)
        => !left.Equals(right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <(TemperatureValue left, TemperatureValue right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >(TemperatureValue left, TemperatureValue right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is less than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator <=(TemperatureValue left, TemperatureValue right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter.
    /// </summary>
    /// <param name="left">The left power value.</param>
    /// <param name="right">The right power value.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> parameter is greater than or equal to the <paramref name="right"/> parameter; otherwise, <c>false</c>.</returns>
    public static bool operator >=(TemperatureValue left, TemperatureValue right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Adds two objects of <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TemperatureValue operator +(TemperatureValue left, TemperatureValue right)
    {
        right = right.To(left.Unit);

        return new TemperatureValue(left.Value + right.Value, left.Unit);
    }

    /// <summary>
    /// Subtracts two objects of <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TemperatureValue operator -(TemperatureValue left, TemperatureValue right)
    {
        right = right.To(left.Unit);

        return new TemperatureValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static TemperatureValue operator -(TemperatureValue value)
        => new TemperatureValue(-value.Value, value.Unit);

    private TemperatureValue ToBase()
        => new TemperatureValue(new NumberValue(Unit.ToBase(Value.Number)), TemperatureUnit.Celsius);

    /// <summary>
    /// Convert to the <paramref name="newUnit"/> unit.
    /// </summary>
    /// <param name="newUnit">The new unit.</param>
    /// <returns>The value in the new unit.</returns>
    public TemperatureValue To(TemperatureUnit newUnit)
    {
        var inBase = Unit.ToBase(Value.Number);
        var converted = newUnit.FromBase(inBase);

        return new TemperatureValue(new NumberValue(converted), newUnit);
    }

    /// <summary>
    /// Converts the current object to celsius.
    /// </summary>
    /// <returns>The power value which is converted to celsius.</returns>
    public TemperatureValue ToCelsius()
        => To(TemperatureUnit.Celsius);

    /// <summary>
    /// Converts the current object to fahrenheit.
    /// </summary>
    /// <returns>The power value which is converted to fahrenheit.</returns>
    public TemperatureValue ToFahrenheit()
        => To(TemperatureUnit.Fahrenheit);

    /// <summary>
    /// Converts the current object to kelvin.
    /// </summary>
    /// <returns>The power value which is converted to kelvin.</returns>
    public TemperatureValue ToKelvin()
        => To(TemperatureUnit.Kelvin);

    /// <summary>
    /// Returns the absolute value of a specified temperature value.
    /// </summary>
    /// <param name="value">The temperature value.</param>
    /// <returns>The temperature value, <c>x</c>, that such that 0 ≤ <c>x</c> ≤ <c>MaxValue</c>.</returns>
    public static TemperatureValue Abs(TemperatureValue value)
        => new TemperatureValue(NumberValue.Abs(value.Value), value.Unit);

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified temperature value.
    /// </summary>
    /// <param name="value">The temperature value.</param>
    /// <returns>The smallest integral value.</returns>
    public static TemperatureValue Ceiling(TemperatureValue value)
        => new TemperatureValue(NumberValue.Ceiling(value.Value), value.Unit);

    /// <summary>
    /// Returns the largest integral value less than or equal to the specified temperature value number.
    /// </summary>
    /// <param name="value">The temperature value.</param>
    /// <returns>The largest integral value.</returns>
    public static TemperatureValue Floor(TemperatureValue value)
        => new TemperatureValue(NumberValue.Floor(value.Value), value.Unit);

    /// <summary>
    /// Calculates the integral part of a specified temperature value number.
    /// </summary>
    /// <param name="value">An temperature value to truncate.</param>
    /// <returns>The integral part of temperature value number.</returns>
    public static TemperatureValue Truncate(TemperatureValue value)
        => new TemperatureValue(NumberValue.Truncate(value.Value), value.Unit);

    /// <summary>
    /// Returns the fractional part of the temperature value number.
    /// </summary>
    /// <param name="value">The temperature value number.</param>
    /// <returns>The fractional part.</returns>
    public static TemperatureValue Frac(TemperatureValue value)
        => new TemperatureValue(NumberValue.Frac(value.Value), value.Unit);

    /// <summary>
    /// Rounds a double-precision floating-point value to a specified number of fractional digits,
    /// and uses the specified rounding convention for midpoint values.
    /// </summary>
    /// <param name="temperatureValue">The temperature number.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>The number nearest to <paramref name="temperatureValue"/> that has a number of fractional digits equal to <paramref name="digits"/>. If value has fewer fractional digits than <paramref name="digits"/>, <paramref name="temperatureValue"/> is returned unchanged.</returns>
    public static TemperatureValue Round(TemperatureValue temperatureValue, NumberValue digits)
        => new TemperatureValue(NumberValue.Round(temperatureValue.Value, digits), temperatureValue.Unit);

    /// <summary>
    /// Converts <see cref="TemperatureValue"/> to <see cref="Temperature"/>.
    /// </summary>
    /// <returns>The temperature number.</returns>
    public Temperature AsExpression()
        => new Temperature(this);

    /// <summary>
    /// Gets a value.
    /// </summary>
    public NumberValue Value { get; }

    /// <summary>
    /// Gets a unit.
    /// </summary>
    public TemperatureUnit Unit { get; }

    /// <summary>
    /// Gets an integer that indicates the sign of a double-precision floating-point number.
    /// </summary>
    public double Sign => Value.Sign;
}