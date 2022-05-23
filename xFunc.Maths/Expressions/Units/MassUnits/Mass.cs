// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.MassUnits;

/// <summary>
/// Represents a mass number.
/// </summary>
public class Mass : Unit<MassValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mass"/> class.
    /// </summary>
    /// <param name="value">A mass value.</param>
    public Mass(MassValue value)
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