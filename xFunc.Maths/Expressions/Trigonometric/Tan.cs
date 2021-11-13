// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Trigonometric;

/// <summary>
/// Represents the Tangent function.
/// </summary>
public class Tan : TrigonometricExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tan"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    public Tan(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tan"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Tan(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    /// <seealso cref="ExpressionParameters" />
    protected override NumberValue ExecuteInternal(AngleValue angleValue)
        => AngleValue.Tan(angleValue);

    /// <inheritdoc />
    protected override Complex ExecuteComplex(Complex complex)
        => Complex.Tan(complex);

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
        => new Tan(argument ?? Argument);
}