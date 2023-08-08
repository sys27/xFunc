// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Domains;

/// <summary>
/// Represents a domain of function.
/// </summary>
public readonly struct Domain : IEquatable<Domain>
{
    private readonly DomainRange[] ranges;

    /// <summary>
    /// Initializes a new instance of the <see cref="Domain"/> struct.
    /// </summary>
    /// <param name="ranges">The array of ranges.</param>
    public Domain(DomainRange[] ranges)
    {
        if (ranges is null || ranges.Length == 0)
            throw new ArgumentNullException(nameof(ranges));

        for (var i = 0; i < ranges.Length - 1; i++)
        {
            var left = ranges[i];
            var right = ranges[i + 1];

            if (right.Start < left.Start && right.End < left.End)
                throw new ArgumentException(Resource.DomainIsInvalid);
        }

        this.ranges = ranges;
    }

    /// <summary>
    /// Indicates whether <paramref name="left"/> domain is equal to the <paramref name="right"/> domain.
    /// </summary>
    /// <param name="left">The left domain.</param>
    /// <param name="right">The right domain.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> domain is equal to the <paramref name="right"/> domain; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Domain left, Domain right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> domain is not equal to the <paramref name="right"/> domain.
    /// </summary>
    /// <param name="left">The left domain.</param>
    /// <param name="right">The right domain.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> domain is not equal to the <paramref name="right"/> domain; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Domain left, Domain right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(Domain other)
        => ranges.SequenceEqual(other.ranges);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is Domain other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => ranges.GetHashCode();

    /// <inheritdoc />
    public override string ToString()
        => string.Join(" ∪ ", ranges);

    /// <summary>
    /// Determines whether the <paramref name="value"/> number is in the domain of the function.
    /// </summary>
    /// <param name="value">The number.</param>
    /// <returns><c>true</c>, if the <paramref name="value"/> number is in the domain, otherwise, <c>false</c>.</returns>
    public bool IsInRange(NumberValue value)
    {
        foreach (var range in ranges)
            if (range.IsInRange(value))
                return true;

        return false;
    }
}