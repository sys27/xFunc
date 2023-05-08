// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Statistical;

/// <summary>
/// Represents the VARP function.
/// </summary>
/// <seealso cref="DifferentParametersExpression" />
[FunctionName("varp")]
public class Varp : StatisticalExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Varp"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Varp(IEnumerable<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Varp"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Varp(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override double ExecuteInternal(double[] numbers)
    {
        var avg = numbers.Average();
        var sum = 0.0;
        foreach (var number in numbers)
            sum += Math.Pow(number - avg, 2);

        return sum / numbers.Length;
    }

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        => analyzer.Analyze(this);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    protected override TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
        => analyzer.Analyze(this, context);

    /// <inheritdoc />
    public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
        => new Varp(arguments ?? Arguments);
}