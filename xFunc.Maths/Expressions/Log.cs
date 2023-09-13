// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Logarithm function.
/// </summary>
public class Log : BinaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(
        new[] { Variable.X.Name, Variable.Y.Name },
        new Log(Variable.X, Variable.Y));

    /// <summary>
    /// Initializes a new instance of the <see cref="Log"/> class.
    /// </summary>
    /// <param name="base">The right operand.</param>
    /// <param name="arg">The left operand.</param>
    /// <seealso cref="IExpression"/>
    public Log(IExpression @base, IExpression arg)
        : base(@base, arg)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Log"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    public Log(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var leftResult = Left.Execute(parameters);
        var rightResult = Right.Execute(parameters);

        return (leftResult, rightResult) switch
        {
            (NumberValue left, NumberValue right) => NumberValue.Log(right, left),
            (NumberValue left, Complex complex) => NumberValue.Log(complex, left),
            (NumberValue left, RationalValue right) => RationalValue.Log(right, left),
            _ => throw ExecutionException.For(this),
        };
    }

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult>(IAnalyzer<TResult> analyzer)
        => analyzer.Analyze(this);

    /// <inheritdoc />
    protected override TResult AnalyzeInternal<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
        => analyzer.Analyze(this, context);

    /// <inheritdoc />
    public override IExpression Clone(IExpression? left = null, IExpression? right = null)
        => new Log(left ?? Left, right ?? Right);
}