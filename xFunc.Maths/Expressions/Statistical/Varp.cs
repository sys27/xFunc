// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Statistical;

/// <summary>
/// Represents the VARP function.
/// </summary>
/// <seealso cref="DifferentParametersExpression" />
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
    protected override object ExecuteInternal(VectorValue vector)
    {
        var avg = vector.Average();
        var sum = NumberValue.Zero;

        foreach (var number in vector)
        {
            // because power = 2, we can cast directly to `NumberValue`
            sum += (NumberValue)NumberValue.Pow(number - avg, NumberValue.Two);
        }

        return sum / vector.Size;
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