// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Domains;

/// <summary>
/// The builder for <see cref="DomainRange"/>.
/// </summary>
public class DomainBuilder
{
    private readonly DomainRange[] ranges;
    private int index;

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainBuilder"/> class.
    /// </summary>
    /// <param name="rangeCount">The amount of ranges to build.</param>
    public DomainBuilder(int rangeCount)
    {
        ranges = new DomainRange[rangeCount];
        index = 0;
    }

    /// <summary>
    /// Adds a range to the domain.
    /// </summary>
    /// <param name="configuration">The delegate to configure the domain range.</param>
    /// <returns>The builder.</returns>
    /// <exception cref="InvalidOperationException">The amount of added ranges exceeded the amount specified on the builder creation.</exception>
    public DomainBuilder AddRange(Action<DomainRangeBuilder> configuration)
    {
        if (index >= ranges.Length)
            throw new InvalidOperationException(Resource.DomainRangeExceeded);

        var builder = new DomainRangeBuilder();
        configuration(builder);
        ranges[index++] = builder.Build();

        return this;
    }

    /// <summary>
    /// Builds the domain of the function.
    /// </summary>
    /// <returns>The domain of the function.</returns>
    public Domain Build()
        => new Domain(ranges);
}