// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.AreaUnits;

/// <summary>
/// Represents a area unit.
/// </summary>
public class AreaUnit : IEquatable<AreaUnit>
{
    /// <summary>
    /// The meter (m²) unit.
    /// </summary>
    public static readonly AreaUnit Meter = new AreaUnit(1.0, "m^2");

    /// <summary>
    /// The millimeter (mm²) unit.
    /// </summary>
    public static readonly AreaUnit Millimeter = new AreaUnit(0.000001, "mm^2");

    /// <summary>
    /// The centimeter (cm²) unit.
    /// </summary>
    public static readonly AreaUnit Centimeter = new AreaUnit(0.0001, "cm^2");

    /// <summary>
    /// The kilometer (km²) unit.
    /// </summary>
    public static readonly AreaUnit Kilometer = new AreaUnit(1000000, "km^2");

    /// <summary>
    /// The inch (in²) unit.
    /// </summary>
    public static readonly AreaUnit Inch = new AreaUnit(0.00064516, "in^2");

    /// <summary>
    /// The foot (ft²) unit.
    /// </summary>
    public static readonly AreaUnit Foot = new AreaUnit(0.09290304, "ft^2");

    /// <summary>
    /// The yard (yd²) unit.
    /// </summary>
    public static readonly AreaUnit Yard = new AreaUnit(0.83612736, "yd^2");

    /// <summary>
    /// The mile (mi²) unit.
    /// </summary>
    public static readonly AreaUnit Mile = new AreaUnit(2589988.110336, "mi^2");

    /// <summary>
    /// The hectare (ha) unit.
    /// </summary>
    public static readonly AreaUnit Hectare = new AreaUnit(10000.0, "ha");

    /// <summary>
    /// The acre (ac) unit.
    /// </summary>
    public static readonly AreaUnit Acre = new AreaUnit(4046.8564224, "ac");

    private AreaUnit(double factor, string unitName)
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
    public static bool operator ==(AreaUnit left, AreaUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(AreaUnit left, AreaUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(AreaUnit? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Factor.Equals(other.Factor) && UnitName == other.UnitName;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((AreaUnit)obj);
    }

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Factor, UnitName);

    /// <inheritdoc />
    public override string ToString()
        => UnitName;

    /// <summary>
    /// Maps the current area unit to volume unit.
    /// </summary>
    /// <returns>The volume unit.</returns>
    /// <seealso cref="AreaUnit" />
    /// <exception cref="InvalidOperationException">Cannot convert the current AreaUnit to an VolumeUnit.</exception>
    public VolumeUnit ToVolumeUnit()
    {
        if (this == Meter)
            return VolumeUnit.Meter;

        if (this == Centimeter)
            return VolumeUnit.Centimeter;

        if (this == Inch)
            return VolumeUnit.Inch;

        if (this == Foot)
            return VolumeUnit.Foot;

        if (this == Yard)
            return VolumeUnit.Yard;

        throw new InvalidOperationException($"Cannot convert '{UnitName}' to an VolumeUnit.");
    }

    /// <summary>
    /// Gets a factor of conversion from this unit to base unit.
    /// </summary>
    public double Factor { get; }

    /// <summary>
    /// Gets a short name of the unit.
    /// </summary>
    public string UnitName { get; }

    private static readonly Lazy<IDictionary<string, AreaUnit>> AllUnits
        = new Lazy<IDictionary<string, AreaUnit>>(GetUnits);

    private static IDictionary<string, AreaUnit> GetUnits()
        => new Dictionary<string, AreaUnit>(StringComparer.InvariantCultureIgnoreCase)
        {
            { Meter.UnitName, Meter },
            { Millimeter.UnitName, Millimeter },
            { Centimeter.UnitName, Centimeter },
            { Kilometer.UnitName, Kilometer },
            { Inch.UnitName, Inch },
            { Foot.UnitName, Foot },
            { Yard.UnitName, Yard },
            { Mile.UnitName, Mile },
            { Acre.UnitName, Acre },
            { Hectare.UnitName, Hectare },
        };

    /// <summary>
    /// Gets all available unit names.
    /// </summary>
    public static IEnumerable<string> Names => AllUnits.Value.Keys;

    /// <summary>
    /// Gets all available units.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<AreaUnit> Units => AllUnits.Value.Values;

    /// <summary>
    /// Gets a unit by name.
    /// </summary>
    /// <param name="name">The name of unit.</param>
    /// <param name="unit">When this method returns, the value associated with the specified name, if the unit is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns><c>true</c> if area units contain an unit with the specified <paramref name="name"/>; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
    public static bool FromName(string name, [NotNullWhen(true)] out AreaUnit? unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return AllUnits.Value.TryGetValue(name, out unit);
    }
}