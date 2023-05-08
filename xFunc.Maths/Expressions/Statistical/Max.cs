// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Statistical;

/// <summary>
/// Represent the Max function.
/// </summary>
/// <seealso cref="DifferentParametersExpression" />
[FunctionName("max")]
public class Max : StatisticalExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Max"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Max(IEnumerable<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Max"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Max(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override double ExecuteInternal(double[] numbers)
        => numbers.Max();

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
        => new Max(arguments ?? Arguments);
}