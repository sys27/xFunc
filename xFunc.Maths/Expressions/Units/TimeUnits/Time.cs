// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.TimeUnits;

/// <summary>
/// Represents a time number.
/// </summary>
public class Time : Unit<TimeValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Time"/> class.
    /// </summary>
    /// <param name="value">A time value.</param>
    public Time(TimeValue value)
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