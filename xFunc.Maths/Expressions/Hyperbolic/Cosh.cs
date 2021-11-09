// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Hyperbolic;

/// <summary>
/// Represents the Hyperbolic cosine function.
/// </summary>
public class Cosh : HyperbolicExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Cosh"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    public Cosh(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cosh"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Cosh(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override NumberValue ExecuteInternal(AngleValue angleValue)
        => AngleValue.Cosh(angleValue);

    /// <inheritdoc />
    protected override Complex ExecuteComplex(Complex complex)
        => Complex.Cosh(complex);

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
        => new Cosh(argument ?? Argument);
}