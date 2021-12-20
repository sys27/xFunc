// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.MassUnits;

/// <summary>
/// Represents a mass unit.
/// </summary>
public readonly struct MassUnit : IEquatable<MassUnit>
{
    /// <summary>
    /// The kilogram (kg) unit.
    /// </summary>
    public static readonly MassUnit Kilogram = new MassUnit(1.0, "kg");

    /// <summary>
    /// The milligram (mg) unit.
    /// </summary>
    public static readonly MassUnit Milligram = new MassUnit(0.000001, "mg");

    /// <summary>
    /// The gram (g) unit.
    /// </summary>
    public static readonly MassUnit Gram = new MassUnit(0.001, "g");

    /// <summary>
    /// The tonne (t) unit.
    /// </summary>
    public static readonly MassUnit Tonne = new MassUnit(1000, "t");

    /// <summary>
    /// The ounce (oz) unit.
    /// </summary>
    public static readonly MassUnit Ounce = new MassUnit(0.0283495231, "oz");

    /// <summary>
    /// The pound (lb) unit.
    /// </summary>
    public static readonly MassUnit Pound = new MassUnit(0.45359237, "lb");

    private MassUnit(double factor, string unitName)
    {
        Factor = factor;
        UnitName = unitName;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="MassValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MassUnit left, MassUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="MassValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MassUnit left, MassUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(MassUnit other)
        => Factor.Equals(other.Factor) && UnitName == other.UnitName;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is MassUnit other && Equals(other);

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