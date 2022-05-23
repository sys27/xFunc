// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.AreaUnits;

/// <summary>
/// Represents a area number.
/// </summary>
public class Area : Unit<AreaValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Area"/> class.
    /// </summary>
    /// <param name="value">A area value.</param>
    public Area(AreaValue value)
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