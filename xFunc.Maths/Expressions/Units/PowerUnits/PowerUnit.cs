// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.PowerUnits;

/// <summary>
/// Represents a power unit.
/// </summary>
public readonly struct PowerUnit : IEquatable<PowerUnit>
{
    /// <summary>
    /// Watt (W).
    /// </summary>
    public static readonly PowerUnit Watt = new PowerUnit(1.0, "W");

    /// <summary>
    /// Kilowatt (kW).
    /// </summary>
    public static readonly PowerUnit Kilowatt = new PowerUnit(1000.0, "kW");

    /// <summary>
    /// Horsepower (hp).
    /// </summary>
    public static readonly PowerUnit Horsepower = new PowerUnit(745.69987158227022, "hp");

    private PowerUnit(double factor, string unitName)
    {
        Factor = factor;
        UnitName = unitName;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="PowerUnit"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(PowerUnit left, PowerUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="PowerUnit"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(PowerUnit left, PowerUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(PowerUnit other)
        => Factor.Equals(other.Factor) && UnitName == other.UnitName;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is PowerUnit other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Factor, UnitName);

    /// <inheritdoc />
    public override string ToString()
        => UnitName;

    /// <summary>
    /// Gets a factor of conversion from this unit to base unit.
    /// </summary>
    public double Factor { get; }

    /// <summary>
    /// Gets a short name of the unit.
    /// </summary>
    public string UnitName { get; }
}