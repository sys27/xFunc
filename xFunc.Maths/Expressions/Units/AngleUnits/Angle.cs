// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.AngleUnits;

/// <summary>
/// Represents an angle number.
/// </summary>
public class Angle : Unit<AngleValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Angle"/> class.
    /// </summary>
    /// <param name="value">An angle.</param>
    public Angle(AngleValue value)
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