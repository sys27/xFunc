// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the nth root operator.
/// </summary>
public class Root : BinaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(
        new[] { Variable.X.Name, Variable.Y.Name },
        new Root(Variable.X, Variable.Y));

    /// <summary>
    /// Initializes a new instance of the <see cref="Root"/> class.
    /// </summary>
    /// <param name="radicand">The radicand.</param>
    /// <param name="degree">The degree.</param>
    public Root(IExpression radicand, IExpression degree)
        : base(radicand, degree)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Root"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    internal Root(ImmutableArray<IExpression> arguments)
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
            (NumberValue number, NumberValue power) => NumberValue.Pow(number, 1 / power),
            _ => throw new ResultIsNotSupportedException(this, leftResult, rightResult),
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
        => new Root(left ?? Left, right ?? Right);
}