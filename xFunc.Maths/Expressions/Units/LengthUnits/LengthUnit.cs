// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.LengthUnits;

/// <summary>
/// Represents a length unit.
/// </summary>
public readonly struct LengthUnit : IEquatable<LengthUnit>
{
    /// <summary>
    /// The meter (m) unit.
    /// </summary>
    public static readonly LengthUnit Meter = new LengthUnit(1.0, "m");

    /// <summary>
    /// The nanometer (nm) unit.
    /// </summary>
    public static readonly LengthUnit Nanometer = new LengthUnit(0.000000001, "nm");

    /// <summary>
    /// The micrometer (µm) unit.
    /// </summary>
    public static readonly LengthUnit Micrometer = new LengthUnit(0.000001, "µm");

    /// <summary>
    /// The millimeter (mm) unit.
    /// </summary>
    public static readonly LengthUnit Millimeter = new LengthUnit(0.001, "mm");

    /// <summary>
    /// The centimeter (cm) unit.
    /// </summary>
    public static readonly LengthUnit Centimeter = new LengthUnit(0.01, "cm");

    /// <summary>
    /// The decimeter (dm) unit.
    /// </summary>
    public static readonly LengthUnit Decimeter = new LengthUnit(0.1, "dm");

    /// <summary>
    /// The kilometer (km) unit.
    /// </summary>
    public static readonly LengthUnit Kilometer = new LengthUnit(1000, "km");

    /// <summary>
    /// The inch (in) unit.
    /// </summary>
    public static readonly LengthUnit Inch = new LengthUnit(0.0254, "in");

    /// <summary>
    /// The foot (ft) unit.
    /// </summary>
    public static readonly LengthUnit Foot = new LengthUnit(0.3048, "ft");

    /// <summary>
    /// The yard (yd) unit.
    /// </summary>
    public static readonly LengthUnit Yard = new LengthUnit(0.9144, "yd");

    /// <summary>
    /// The mile (mi) unit.
    /// </summary>
    public static readonly LengthUnit Mile = new LengthUnit(1609.344, "mi");

    /// <summary>
    /// The nautical mile (nmi) unit.
    /// </summary>
    public static readonly LengthUnit NauticalMile = new LengthUnit(1852, "nmi");

    /// <summary>
    /// The chain (ch) unit.
    /// </summary>
    public static readonly LengthUnit Chain = new LengthUnit(20.1168, "ch");

    /// <summary>
    /// The rod (rd) unit.
    /// </summary>
    public static readonly LengthUnit Rod = new LengthUnit(5.0292, "rd");

    /// <summary>
    /// The astronomical unit (au) unit.
    /// </summary>
    public static readonly LengthUnit AstronomicalUnit = new LengthUnit(149597870691, "au");

    /// <summary>
    /// The light year (ly) unit.
    /// </summary>
    public static readonly LengthUnit LightYear = new LengthUnit(9460730472580044, "ly");

    /// <summary>
    /// The parsec (pc) unit.
    /// </summary>
    public static readonly LengthUnit Parsec = new LengthUnit(30856775812799588, "pc");

    private LengthUnit(double factor, string unitName)
    {
        Factor = factor;
        UnitName = unitName;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="LengthValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(LengthUnit left, LengthUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="LengthValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(LengthUnit left, LengthUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(LengthUnit other)
        => Factor.Equals(other.Factor) && UnitName == other.UnitName;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is LengthUnit other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Factor, UnitName);

    /// <inheritdoc />
    public override string ToString()
        => UnitName;

    /// <summary>
    /// Maps the current length unit to area unit.
    /// </summary>
    /// <returns>The area unit.</returns>
    /// <seealso cref="AreaUnit" />
    /// <exception cref="InvalidOperationException">Cannot convert the current LengthUnit to an AreaUnit.</exception>
    public AreaUnit ToAreaUnit()
    {
        if (this == Meter)
            return AreaUnit.Meter;

        if (this == Millimeter)
            return AreaUnit.Millimeter;

        if (this == Centimeter)
            return AreaUnit.Centimeter;

        if (this == Kilometer)
            return AreaUnit.Kilometer;

        if (this == Inch)
            return AreaUnit.Inch;

        if (this == Foot)
            return AreaUnit.Foot;

        if (this == Yard)
            return AreaUnit.Yard;

        if (this == Mile)
            return AreaUnit.Mile;

        throw new InvalidOperationException($"Cannot convert '{UnitName}' to an AreaUnit.");
    }

    /// <summary>
    /// Gets a factor of conversion from this unit to base unit.
    /// </summary>
    public double Factor { get; }

    /// <summary>
    /// Gets a short name of the unit.
    /// </summary>
    public string UnitName { get; }

    private static readonly Lazy<IDictionary<string, LengthUnit>> AllUnits
        = new Lazy<IDictionary<string, LengthUnit>>(GetUnits);

    private static IDictionary<string, LengthUnit> GetUnits()
        => new Dictionary<string, LengthUnit>(StringComparer.InvariantCultureIgnoreCase)
        {
            { Meter.UnitName, Meter },
            { Nanometer.UnitName, Nanometer },
            { Micrometer.UnitName, Micrometer },
            { Millimeter.UnitName, Millimeter },
            { Centimeter.UnitName, Centimeter },
            { Decimeter.UnitName, Decimeter },
            { Kilometer.UnitName, Kilometer },
            { Inch.UnitName, Inch },
            { Foot.UnitName, Foot },
            { Yard.UnitName, Yard },
            { Mile.UnitName, Mile },
            { NauticalMile.UnitName, NauticalMile },
            { Chain.UnitName, Chain },
            { Rod.UnitName, Rod },
            { AstronomicalUnit.UnitName, AstronomicalUnit },
            { LightYear.UnitName, LightYear },
            { Parsec.UnitName, Parsec },
        };

    /// <summary>
    /// Gets all available unit names.
    /// </summary>
    public static IEnumerable<string> Names => AllUnits.Value.Keys;

    /// <summary>
    /// Gets all available units.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<LengthUnit> Units => AllUnits.Value.Values;

    /// <summary>
    /// Gets a unit by name.
    /// </summary>
    /// <param name="name">The name of unit.</param>
    /// <param name="unit">When this method returns, the value associated with the specified name, if the unit is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns><c>true</c> if length units contain an unit with the specified <paramref name="name"/>; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
    public static bool FromName(string name, out LengthUnit unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return AllUnits.Value.TryGetValue(name, out unit);
    }
}