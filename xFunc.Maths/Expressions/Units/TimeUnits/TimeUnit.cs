// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.TimeUnits;

/// <summary>
/// Represents a time unit.
/// </summary>
public readonly struct TimeUnit : IEquatable<TimeUnit>
{
    /// <summary>
    /// The second (s) unit.
    /// </summary>
    public static readonly TimeUnit Second = new TimeUnit(1.0, "s");

    /// <summary>
    /// The nanosecond (ns) unit.
    /// </summary>
    public static readonly TimeUnit Nanosecond = new TimeUnit(1e-9, "ns");

    /// <summary>
    /// The microsecond (μs) unit.
    /// </summary>
    public static readonly TimeUnit Microsecond = new TimeUnit(1e-6, "μs");

    /// <summary>
    /// The millisecond (ms) unit.
    /// </summary>
    public static readonly TimeUnit Millisecond = new TimeUnit(1e-3, "ms");

    /// <summary>
    /// The minute (min) unit.
    /// </summary>
    public static readonly TimeUnit Minute = new TimeUnit(60, "min");

    /// <summary>
    /// The hour (h) unit.
    /// </summary>
    public static readonly TimeUnit Hour = new TimeUnit(60 * 60, "h");

    /// <summary>
    /// The day (day) unit.
    /// </summary>
    public static readonly TimeUnit Day = new TimeUnit(24 * 60 * 60, "day");

    /// <summary>
    /// The week (week) unit.
    /// </summary>
    public static readonly TimeUnit Week = new TimeUnit(7 * 24 * 60 * 60, "week");

    /// <summary>
    /// The year (year) unit.
    /// </summary>
    public static readonly TimeUnit Year = new TimeUnit(365 * 24 * 60 * 60, "year");

    private TimeUnit(double factor, string unitName)
    {
        Factor = factor;
        UnitName = unitName;
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="TimeUnit"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(TimeUnit left, TimeUnit right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="TimeUnit"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(TimeUnit left, TimeUnit right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(TimeUnit other)
        => Factor.Equals(other.Factor) && UnitName == other.UnitName;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is TimeUnit other && Equals(other);

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

    private static readonly Lazy<IDictionary<string, TimeUnit>> AllUnits
        = new Lazy<IDictionary<string, TimeUnit>>(GetUnits);

    private static IDictionary<string, TimeUnit> GetUnits()
        => new Dictionary<string, TimeUnit>(StringComparer.InvariantCultureIgnoreCase)
        {
            { Second.UnitName, Second },
            { Nanosecond.UnitName, Nanosecond },
            { Microsecond.UnitName, Microsecond },
            { Millisecond.UnitName, Millisecond },
            { Minute.UnitName, Minute },
            { Hour.UnitName, Hour },
            { Day.UnitName, Day },
            { Week.UnitName, Week },
            { Year.UnitName, Year },
        };

    /// <summary>
    /// Gets all available unit names.
    /// </summary>
    public static IEnumerable<string> Names => AllUnits.Value.Keys;

    /// <summary>
    /// Gets all available units.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static IEnumerable<TimeUnit> Units => AllUnits.Value.Values;

    /// <summary>
    /// Gets a unit by name.
    /// </summary>
    /// <param name="name">The name of unit.</param>
    /// <param name="unit">When this method returns, the value associated with the specified name, if the unit is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns><c>true</c> if time units contain an unit with the specified <paramref name="name"/>; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> is <c>null</c>.</exception>
    public static bool FromName(string name, out TimeUnit unit)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        return AllUnits.Value.TryGetValue(name, out unit);
    }
}