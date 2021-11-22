// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        => Value == other.Value && Unit == other.Unit;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is TemperatureValue other && Equals(other);

    /// <inheritdoc />
    public int CompareTo(TemperatureValue other)
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

        if (obj is TemperatureValue other)
            return CompareTo(other);

        throw new ArgumentException($"Object must be of type {nameof(TemperatureValue)}");
    }

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(Value, (int)Unit);

    /// <inheritdoc />
    public override string ToString() => Unit switch
    {
        TemperatureUnit.Celsius => $"{Value} °C",
        TemperatureUnit.Fahrenheit => $"{Value} °F",
        TemperatureUnit.Kelvin => $"{Value} K",
        _ => throw new InvalidOperationException(),
    };

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
        (left, right) = ToCommonUnits(left, right);

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
        (left, right) = ToCommonUnits(left, right);

        return new TemperatureValue(left.Value - right.Value, left.Unit);
    }

    /// <summary>
    /// Produces the negative of <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="value">The power value.</param>
    /// <returns>The negative of <paramref name="value"/>.</returns>
    public static TemperatureValue operator -(TemperatureValue value)
        => new TemperatureValue(-value.Value, value.Unit);

    /// <summary>
    /// Multiplies two objects of <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TemperatureValue operator *(TemperatureValue left, TemperatureValue right)
    {
        (left, right) = ToCommonUnits(left, right);

        return new TemperatureValue(left.Value * right.Value, left.Unit);
    }

    /// <summary>
    /// Divides two objects of <see cref="TemperatureValue"/>.
    /// </summary>
    /// <param name="left">The first object to divide.</param>
    /// <param name="right">The second object to divide.</param>
    /// <returns>An object that is the fraction of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TemperatureValue operator /(TemperatureValue left, TemperatureValue right)
    {
        (left, right) = ToCommonUnits(left, right);

        return new TemperatureValue(left.Value / right.Value, left.Unit);
    }

    private static (TemperatureValue Left, TemperatureValue Right) ToCommonUnits(TemperatureValue left, TemperatureValue right)
    {
        var commonUnit = GetCommonUnit(left.Unit, right.Unit);

        return (left.To(commonUnit), right.To(commonUnit));
    }

    private static TemperatureUnit GetCommonUnit(TemperatureUnit left, TemperatureUnit right)
        => (left, right) switch
        {
            _ when left == right => left,

            (TemperatureUnit.Fahrenheit, TemperatureUnit.Kelvin) or
                (TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit)
                => TemperatureUnit.Fahrenheit,

            _ => TemperatureUnit.Celsius,
        };

    /// <summary>
    /// Converts the current object to the specified <paramref name="unit"/>.
    /// </summary>
    /// <param name="unit">The unit to convert to.</param>
    /// <returns>The power value which is converted to the specified <paramref name="unit"/>.</returns>
    public TemperatureValue To(TemperatureUnit unit) => unit switch
    {
        TemperatureUnit.Celsius => ToCelsius(),
        TemperatureUnit.Fahrenheit => ToFahrenheit(),
        TemperatureUnit.Kelvin => ToKelvin(),
        _ => throw new ArgumentOutOfRangeException(nameof(unit)),
    };

    /// <summary>
    /// Converts the current object to celsius.
    /// </summary>
    /// <returns>The power value which is converted to celsius.</returns>
    public TemperatureValue ToCelsius() => Unit switch
    {
        TemperatureUnit.Celsius => this,
        TemperatureUnit.Fahrenheit => Celsius((Value - 32) * 5.0 / 9.0),
        TemperatureUnit.Kelvin => Celsius(Value - 273.15),
        _ => throw new InvalidOperationException(),
    };

    /// <summary>
    /// Converts the current object to fahrenheit.
    /// </summary>
    /// <returns>The power value which is converted to fahrenheit.</returns>
    public TemperatureValue ToFahrenheit() => Unit switch
    {
        TemperatureUnit.Celsius => Fahrenheit(Value * 9.0 / 5.0 + 32),
        TemperatureUnit.Fahrenheit => this,
        TemperatureUnit.Kelvin => Fahrenheit((Value - 273.15) * 9.0 / 5.0 + 32),
        _ => throw new InvalidOperationException(),
    };

    /// <summary>
    /// Converts the current object to kelvin.
    /// </summary>
    /// <returns>The power value which is converted to kelvin.</returns>
    public TemperatureValue ToKelvin() => Unit switch
    {
        TemperatureUnit.Celsius => Kelvin(Value + 273.15),
        TemperatureUnit.Fahrenheit => Kelvin((Value - 32) * 5.0 / 9.0 + 273.15),
        TemperatureUnit.Kelvin => this,
        _ => throw new InvalidOperationException(),
    };

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