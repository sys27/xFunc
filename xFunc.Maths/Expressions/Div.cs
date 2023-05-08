// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Division operator.
/// </summary>
[FunctionName("div")]
public class Div : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Div"/> class.
    /// </summary>
    /// <param name="left">The first (left) operand.</param>
    /// <param name="right">The second (right) operand.</param>
    public Div(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Div"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    internal Div(ImmutableArray<IExpression> arguments)
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
            (NumberValue left, NumberValue right) => left / right,

            (AngleValue left, NumberValue right) => left / right,
            (PowerValue left, NumberValue right) => left / right,
            (TemperatureValue left, NumberValue right) => left / right,
            (MassValue left, NumberValue right) => left / right,
            (LengthValue left, NumberValue right) => left / right,
            (TimeValue left, NumberValue right) => left / right,
            (AreaValue left, NumberValue right) => left / right,
            (VolumeValue left, NumberValue right) => left / right,

            (NumberValue left, Complex right) => left / right,
            (Complex left, NumberValue right) => left / right,
            (Complex left, Complex right) => left / right,

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
        => new Div(left ?? Left, right ?? Right);
}