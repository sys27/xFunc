// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.TemperatureUnits;

/// <summary>
/// Represents a temperature unit.
/// </summary>
public readonly struct TemperatureUnit : IEquatable<TemperatureUnit>
{
    /// <summary>
    /// Celsius (°C).
    /// </summary>
    public static readonly TemperatureUnit Celsius = new TemperatureUnit(x => x, x => x, "°C");

    /// <summary>
    /// Fahrenheit (°F).
    /// </summary>
    public static readonly TemperatureUnit Fahrenheit = new TemperatureUnit(
        x => (x - 32) * 5.0 / 9.0, x => x * 9.0 / 5.0 + 32, "°F");

    /// <summary>
    /// Kelvin (K).
    /// </summary>
    public static readonly TemperatureUnit Kelvin = new TemperatureUnit(
        x => x - 273.15, x => x + 273.15, "K");

    private TemperatureUnit(Func<double, double> toBase, Func<double, double> fromBase, string unitName)
    {
        ToBase = toBase;
        FromBase = fromBase;
        UnitName = unitName;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="TemperatureUnit"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(TemperatureUnit left, TemperatureUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="TemperatureUnit"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(TemperatureUnit left, TemperatureUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(TemperatureUnit other)
        => ToBase.Equals(other.ToBase) &&
           FromBase.Equals(other.FromBase) &&
           UnitName == other.UnitName;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is TemperatureUnit other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(ToBase, FromBase, UnitName);

    /// <inheritdoc />
    public override string ToString()
        => UnitName;

    /// <summary>
    /// Gets the conversion function from the current unit to the base unit.
    /// </summary>
    public Func<double, double> ToBase { get; }

    /// <summary>
    /// Gets the conversion function from the base unit to the current unit.
    /// </summary>
    public Func<double, double> FromBase { get; }

    /// <summary>
    /// Gets a short name of the unit.
    /// </summary>
    public string UnitName { get; }
}