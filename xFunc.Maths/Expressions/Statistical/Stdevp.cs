// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Statistical;

/// <summary>
/// Represents the STDEVP function.
/// </summary>
/// <seealso cref="DifferentParametersExpression" />
[FunctionName("stdevp")]
public class Stdevp : StatisticalExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Stdevp"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Stdevp(IEnumerable<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Stdevp"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Stdevp(ImmutableArray<IExpression> arguments)
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

        var variance = sum / numbers.Length;

        return Math.Sqrt(variance);
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
        => new Stdevp(arguments ?? Arguments);
}