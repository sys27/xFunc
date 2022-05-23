// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.VolumeUnits;

/// <summary>
/// Represents a area unit.
/// </summary>
public class VolumeUnit : IEquatable<VolumeUnit>
{
    /// <summary>
    /// The meter (m³) unit.
    /// </summary>
    public static readonly VolumeUnit Meter = new VolumeUnit(1.0, "m^3");

    /// <summary>
    /// The centimeter (cm³) unit.
    /// </summary>
    public static readonly VolumeUnit Centimeter = new VolumeUnit(0.000001, "cm^3");

    /// <summary>
    /// The Liter (l) unit.
    /// </summary>
    public static readonly VolumeUnit Liter = new VolumeUnit(0.001, "l");

    /// <summary>
    /// The inch (in³) unit.
    /// </summary>
    public static readonly VolumeUnit Inch = new VolumeUnit(0.0000163871, "in^3");

    /// <summary>
    /// The foot (ft³) unit.
    /// </summary>
    public static readonly VolumeUnit Foot = new VolumeUnit(0.0283168466, "ft^3");

    /// <summary>
    /// The yard (yd³) unit.
    /// </summary>
    public static readonly VolumeUnit Yard = new VolumeUnit(0.764554858, "yd^3");

    /// <summary>
    /// The gallon (gal) unit.
    /// </summary>
    public static readonly VolumeUnit Gallon = new VolumeUnit(0.0037854118, "gal");

    private VolumeUnit(double factor, string unitName)
    {
        Factor = factor;
        UnitName = unitName;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(VolumeUnit left, VolumeUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(VolumeUnit left, VolumeUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(VolumeUnit other)
        => Factor.Equals(other.Factor) && UnitName == other.UnitName;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is VolumeUnit other && Equals(other);

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

    private static readonly Lazy<IDictionary<string, VolumeUnit>> AllUnits
        = new Lazy<IDictionary<string, VolumeUnit>>(GetUnits);

    private static IDictionary<string, VolumeUnit> GetUnits()
        => new Dictionary<string, VolumeUnit>(StringComparer.InvariantCultureIgnoreCase)
        {
            { Meter.UnitName, Meter },
            { Centimeter.UnitName, Centimeter },
            { Liter.UnitName, Liter },
            { Inch.UnitName, Inch },
            { Foot.UnitName, Foot },
            { Yard.UnitName, Yard },
            { Gallon.UnitName, Gallon },
        };

    /// <summary>
    /// Gets all available unit names.
    /// </summary>
    public static IEnumerable<string> Names => AllUnits.Value.Keys;

    /// <summary>
    /// Gets all available units.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<VolumeUnit> Units => AllUnits.Value.Values;

    /// <summary>
    /// Gets a unit by name.
    /// </summary>
    /// <param name="name">The name of unit.</param>
    /// <param name="unit">When this method returns, the value associated with the specified name, if the unit is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns><c>true</c> if volume units contain an unit with the specified <paramref name="name"/>; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
    public static bool FromName(string name, out VolumeUnit unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return AllUnits.Value.TryGetValue(name, out unit);
    }
}