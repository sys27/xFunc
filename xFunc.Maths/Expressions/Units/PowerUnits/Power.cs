// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.PowerUnits;

/// <summary>
/// Represents a power number.
/// </summary>
public class Power : Unit<PowerValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Power"/> class.
    /// </summary>
    /// <param name="value">A power number.</param>
    public Power(PowerValue value)
        : base(value)
    {
    }

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        => analyzer.Analyze(this);

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
        => analyzer.Analyze(this, context);
}