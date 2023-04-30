// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.AngleUnits;

/// <summary>
/// Specifies a measurement of angle.
/// </summary>
public class AngleUnit : IEquatable<AngleUnit>
{
    /// <summary>
    /// The radian (rad, radian, radians) unit.
    /// </summary>
    public static readonly AngleUnit Radian = new AngleUnit(1, "radian", "rad", "radians");

    /// <summary>
    /// The degree (deg, degree, degrees) unit.
    /// </summary>
    public static readonly AngleUnit Degree = new AngleUnit(Math.PI / 180, "degree", "deg", "degrees");

    /// <summary>
    /// The gradian (grad, gradian, gradians) unit.
    /// </summary>
    public static readonly AngleUnit Gradian = new AngleUnit(Math.PI / 200, "gradian", "grad", "gradians");

    private AngleUnit(double factor, params string[] unitNames)
    {
        Factor = factor;
        UnitNames = new HashSet<string>(unitNames);
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(AngleUnit left, AngleUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="AreaValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(AngleUnit left, AngleUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(AngleUnit other)
        => Factor.Equals(other.Factor) && UnitNames.SequenceEqual(other.UnitNames);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is AngleUnit other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Factor, UnitNames);

    /// <inheritdoc />
    public override string ToString()
        => UnitNames.First();

    /// <summary>
    /// Gets a factor of conversion from this unit to base unit.
    /// </summary>
    public double Factor { get; }

    /// <summary>
    /// Gets a short name of the unit.
    /// </summary>
    public IReadOnlyCollection<string> UnitNames { get; }

    private static readonly Lazy<IDictionary<string, AngleUnit>> AllUnits
        = new Lazy<IDictionary<string, AngleUnit>>(GetUnits);

    private static IDictionary<string, AngleUnit> GetUnits()
    {
        var units = new Dictionary<string, AngleUnit>(StringComparer.InvariantCultureIgnoreCase);

        AddUnits(units, Radian);
        AddUnits(units, Degree);
        AddUnits(units, Gradian);

        return units;

        static void AddUnits(IDictionary<string, AngleUnit> dictionary, AngleUnit unit)
        {
            foreach (var unitName in unit.UnitNames)
            {
                dictionary.Add(unitName, unit);
            }
        }
    }

    /// <summary>
    /// Gets all available unit names.
    /// </summary>
    public static IEnumerable<string> Names => AllUnits.Value.Keys;

    /// <summary>
    /// Gets all available units.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<AngleUnit> Units => AllUnits.Value.Values;

    /// <summary>
    /// Gets a unit by name.
    /// </summary>
    /// <param name="name">The name of unit.</param>
    /// <param name="unit">When this method returns, the value associated with the specified name, if the unit is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns><c>true</c> if area units contain an unit with the specified <paramref name="name"/>; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
    public static bool FromName(string name, out AngleUnit unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return AllUnits.Value.TryGetValue(name, out unit);
    }

    /// <summary>
    /// Gets a value indicating whether the current instance is <c>Degree</c>.
    /// </summary>
    public bool IsDegree
        => this == Degree;

    /// <summary>
    /// Gets a value indicating whether the current instance is <c>Radian</c>.
    /// </summary>
    public bool IsRadian
        => this == Radian;

    /// <summary>
    /// Gets a value indicating whether the current instance is <c>Gradian</c>.
    /// </summary>
    public bool IsGradian
        => this == Gradian;
}