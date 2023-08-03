// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Domains;

/// <summary>
/// Represents a range of allowed values for the domain of function.
/// </summary>
public readonly struct DomainRange : IEquatable<DomainRange>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainRange"/> struct.
    /// </summary>
    /// <param name="start">The start of the range.</param>
    /// <param name="isStartInclusive">Determines whether the <paramref name="start"/> parameter is inclusive.</param>
    /// <param name="end">The end of the range.</param>
    /// <param name="isEndInclusive">Determines whether the <paramref name="end"/> parameter is inclusive.</param>
    public DomainRange(NumberValue start, bool isStartInclusive, NumberValue end, bool isEndInclusive)
    {
        if ((start.IsInfinity && isStartInclusive) ||
            (end.IsInfinity && isEndInclusive))
            throw new ArgumentException(Resource.DomainRangeIsIncorrect);

        if (start >= end)
            throw new ArgumentException(Resource.DomainRangeStartIsGreaterThanEnd);

        Start = start;
        IsStartInclusive = isStartInclusive;
        End = end;
        IsEndInclusive = isEndInclusive;
    }

    /// <summary>
    /// Indicates whether <paramref name="left"/> range is equal to the <paramref name="right"/> range.
    /// </summary>
    /// <param name="left">The left range.</param>
    /// <param name="right">The right range.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> range is equal to the <paramref name="right"/> range; otherwise, <c>false</c>.</returns>
    public static bool operator ==(DomainRange left, DomainRange right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether <paramref name="left"/> range is not equal to the <paramref name="right"/> range.
    /// </summary>
    /// <param name="left">The left range.</param>
    /// <param name="right">The right range.</param>
    /// <returns><c>true</c> if the <paramref name="left"/> range is not equal to the <paramref name="right"/> range; otherwise, <c>false</c>.</returns>
    public static bool operator !=(DomainRange left, DomainRange right)
        => !left.Equals(right);

    /// <inheritdoc />
    public bool Equals(DomainRange other)
        => Start.Equals(other.Start) &&
           IsStartInclusive == other.IsStartInclusive &&
           End.Equals(other.End) &&
           IsEndInclusive == other.IsEndInclusive;

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is DomainRange other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(Start, IsStartInclusive, End, IsEndInclusive);

    /// <inheritdoc />
    public override string ToString()
    {
        var leftBracket = IsStartInclusive ? '[' : '(';
        var rightBracket = IsEndInclusive ? ']' : ')';

        return $"{leftBracket}{Start}; {End}{rightBracket}";
    }

    /// <summary>
    /// Determines whether the <paramref name="value"/> number is in the range of domain.
    /// </summary>
    /// <param name="value">The number.</param>
    /// <returns><c>true</c>, if the <paramref name="value"/> number is in the range of domain, otherwise, <c>false</c>.</returns>
    public bool IsInRange(NumberValue value)
        => CheckStart(value) && CheckEnd(value);

    private bool CheckStart(NumberValue value)
        => IsStartInclusive
            ? Start <= value
            : Start < value;

    private bool CheckEnd(NumberValue value)
        => IsEndInclusive
            ? value <= End
            : value < End;

    /// <summary>
    /// Gets the start of the range.
    /// </summary>
    public NumberValue Start { get; }

    /// <summary>
    /// Gets a value indicating whether the start is inclusive.
    /// </summary>
    public bool IsStartInclusive { get; }

    /// <summary>
    /// Gets the end of the range.
    /// </summary>
    public NumberValue End { get; }

    /// <summary>
    /// Gets a value indicating whether the end is inclusive.
    /// </summary>
    public bool IsEndInclusive { get; }
}