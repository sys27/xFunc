// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.LengthUnits;

/// <summary>
/// Represents a length number.
/// </summary>
public class Length : Unit<LengthValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Length"/> class.
    /// </summary>
    /// <param name="value">A length value.</param>
    public Length(LengthValue value)
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