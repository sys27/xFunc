// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Hyperbolic;

/// <summary>
/// Represents the Arcsch function.
/// </summary>
public class Arcsch : InverseHyperbolicExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new Arcsch(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="Arcsch"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    public Arcsch(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Arcsch"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Arcsch(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override AngleValue ExecuteInternal(NumberValue radian)
        => AngleValue.Acsch(radian);

    /// <inheritdoc />
    protected override Complex ExecuteComplex(Complex complex)
        => ComplexExtensions.Acsch(complex);

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        => analyzer.Analyze(this);

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
        => analyzer.Analyze(this, context);

    /// <inheritdoc />
    public override IExpression Clone(IExpression? argument = null)
        => new Arcsch(argument ?? Argument);
}