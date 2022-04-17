// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Multiplication operator.
/// </summary>
public class Mul : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mul"/> class.
    /// </summary>
    /// <param name="left">The first (left) operand.</param>
    /// <param name="right">The second (right) operand.</param>
    public Mul(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Mul"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    internal Mul(ImmutableArray<IExpression> arguments)
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
            (NumberValue left, NumberValue right) => left * right,

            (NumberValue left, AngleValue right) => left * right,
            (AngleValue left, NumberValue right) => left * right,

            (NumberValue left, PowerValue right) => left * right,
            (PowerValue left, NumberValue right) => left * right,

            (NumberValue left, TemperatureValue right) => left * right,
            (TemperatureValue left, NumberValue right) => left * right,

            (NumberValue left, MassValue right) => left * right,
            (MassValue left, NumberValue right) => left * right,

            (NumberValue left, LengthValue right) => left * right,
            (LengthValue left, NumberValue right) => left * right,

            (NumberValue left, TimeValue right) => left * right,
            (TimeValue left, NumberValue right) => left * right,

            (NumberValue left, AreaValue right) => left * right,
            (AreaValue left, NumberValue right) => left * right,

            (NumberValue left, VolumeValue right) => left * right,
            (VolumeValue left, NumberValue right) => left * right,

            (NumberValue left, Complex right) => left * right,
            (Complex left, NumberValue right) => left * right,
            (Complex left, Complex right) => left * right,

            (Vector left, Vector right) => left.Mul(right, parameters),
            (NumberValue left, Vector right) => right.Mul(left, parameters),
            (Vector left, NumberValue right) => left.Mul(right, parameters),

            (Matrix left, Matrix right) => left.Mul(right, parameters),
            (NumberValue left, Matrix right) => right.Mul(left, parameters),
            (Matrix left, NumberValue right) => left.Mul(right, parameters),

            (Matrix left, Vector right) => left.Mul(right, parameters),
            (Vector left, Matrix right) => left.Mul(right, parameters),

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
        => new Mul(left ?? Left, right ?? Right);
}