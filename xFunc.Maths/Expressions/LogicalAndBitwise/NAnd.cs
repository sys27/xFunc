// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.LogicalAndBitwise;

/// <summary>
/// Represents a bitwise NAND operator.
/// </summary>
public class NAnd : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NAnd"/> class.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <seealso cref="IExpression"/>
    public NAnd(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var left = Left.Execute(parameters);
        var right = Right.Execute(parameters);

        return (left, right) switch
        {
            (bool leftBool, bool rightBool) => !(leftBool & rightBool),
            _ => throw new ResultIsNotSupportedException(this, left, right),
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
        => new NAnd(left ?? Left, right ?? Right);
}