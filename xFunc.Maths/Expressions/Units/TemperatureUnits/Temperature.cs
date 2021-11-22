// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.TemperatureUnits;

/// <summary>
/// Represents an temperature number.
/// </summary>
public class Temperature : Unit<TemperatureValue>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Temperature"/> class.
    /// </summary>
    /// <param name="value">A temperature number.</param>
    public Temperature(TemperatureValue value)
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