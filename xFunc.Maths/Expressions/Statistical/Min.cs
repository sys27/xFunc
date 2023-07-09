// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Statistical;

/// <summary>
/// Represent the Min function.
/// </summary>
/// <seealso cref="DifferentParametersExpression" />
public class Min : StatisticalExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Min"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Min(IEnumerable<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Min"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public Min(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override object ExecuteInternal(VectorValue vector)
        => vector.Min();

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
        => new Min(arguments ?? Arguments);
}