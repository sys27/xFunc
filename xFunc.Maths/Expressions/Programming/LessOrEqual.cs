// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the "less than or equal" operator.
/// </summary>
public class LessOrEqual : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LessOrEqual"/> class.
    /// </summary>
    /// <param name="left">The left (first) operand.</param>
    /// <param name="right">The right (second) operand.</param>
    public LessOrEqual(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var leftResult = Left.Execute(parameters);
        var rightResult = Right.Execute(parameters);

        return (leftResult, rightResult) switch
        {
            (NumberValue left, NumberValue right) => left <= right,
            (AngleValue left, AngleValue right) => left <= right,
            (PowerValue left, PowerValue right) => left <= right,
            _ => throw new ResultIsNotSupportedException(this, leftResult, rightResult),
        };
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
    public override IExpression Clone(IExpression? left = null, IExpression? right = null)
        => new LessOrEqual(left ?? Left, right ?? Right);
}