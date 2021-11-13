// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Trigonometric;

/// <summary>
/// Represents the Sine function.
/// </summary>
public class Sin : TrigonometricExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sin"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    public Sin(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sin"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Sin(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    /// <seealso cref="ExpressionParameters" />
    protected override NumberValue ExecuteInternal(AngleValue angleValue)
        => AngleValue.Sin(angleValue);

    /// <inheritdoc />
    protected override Complex ExecuteComplex(Complex complex)
        => Complex.Sin(complex);

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
        => new Sin(argument ?? Argument);
}