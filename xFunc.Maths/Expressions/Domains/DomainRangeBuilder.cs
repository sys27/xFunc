// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Domains;

/// <summary>
/// The builder of <see cref="DomainRange"/>.
/// </summary>
public class DomainRangeBuilder
{
    private NumberValue start;
    private bool isStartInclusive;
    private NumberValue end;
    private bool isEndInclusive;

    /// <summary>
    /// Configures the start of the range.
    /// </summary>
    /// <param name="value">The start of the range.</param>
    /// <returns>The builder.</returns>
    public DomainRangeBuilder Start(NumberValue value)
    {
        start = value;
        isStartInclusive = false;

        return this;
    }

    /// <summary>
    /// Configures the start (inclusive) of the range.
    /// </summary>
    /// <param name="value">The start of the range.</param>
    /// <returns>The builder.</returns>
    public DomainRangeBuilder StartInclusive(NumberValue value)
    {
        start = value;
        isStartInclusive = true;

        return this;
    }

    /// <summary>
    /// Configures the end of the range.
    /// </summary>
    /// <param name="value">The end of the range.</param>
    /// <returns>The builder.</returns>
    public DomainRangeBuilder End(NumberValue value)
    {
        end = value;
        isEndInclusive = false;

        return this;
    }

    /// <summary>
    /// Configures the end (inclusive) of the range.
    /// </summary>
    /// <param name="value">The end of the range.</param>
    /// <returns>The builder.</returns>
    public DomainRangeBuilder EndInclusive(NumberValue value)
    {
        end = value;
        isEndInclusive = true;

        return this;
    }

    /// <summary>
    /// Builds the domain range.
    /// </summary>
    /// <returns>The domain range.</returns>
    public DomainRange Build()
        => new DomainRange(start, isStartInclusive, end, isEndInclusive);
}