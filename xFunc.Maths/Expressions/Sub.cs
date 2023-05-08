// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Subtraction operator.
/// </summary>
[FunctionName("sub")]
public class Sub : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sub"/> class.
    /// </summary>
    /// <param name="left">The minuend.</param>
    /// <param name="right">The subtrahend.</param>
    public Sub(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sub"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    internal Sub(ImmutableArray<IExpression> arguments)
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
            (NumberValue left, NumberValue right) => left - right,

            (NumberValue left, AngleValue right) => left - right,
            (AngleValue left, NumberValue right) => left - right,
            (AngleValue left, AngleValue right) => left - right,

            (NumberValue left, PowerValue right) => left - right,
            (PowerValue left, NumberValue right) => left - right,
            (PowerValue left, PowerValue right) => left - right,

            (NumberValue left, TemperatureValue right) => left - right,
            (TemperatureValue left, NumberValue right) => left - right,
            (TemperatureValue left, TemperatureValue right) => left - right,

            (NumberValue left, MassValue right) => left - right,
            (MassValue left, NumberValue right) => left - right,
            (MassValue left, MassValue right) => left - right,

            (NumberValue left, LengthValue right) => left - right,
            (LengthValue left, NumberValue right) => left - right,
            (LengthValue left, LengthValue right) => left - right,

            (NumberValue left, TimeValue right) => left - right,
            (TimeValue left, NumberValue right) => left - right,
            (TimeValue left, TimeValue right) => left - right,

            (NumberValue left, AreaValue right) => left - right,
            (AreaValue left, NumberValue right) => left - right,
            (AreaValue left, AreaValue right) => left - right,

            (NumberValue left, VolumeValue right) => left - right,
            (VolumeValue left, NumberValue right) => left - right,
            (VolumeValue left, VolumeValue right) => left - right,

            (NumberValue left, Complex right) => left - right,
            (Complex left, NumberValue right) => left - right,
            (Complex left, Complex right) => left - right,

            (Vector left, Vector right) => left.Sub(right, parameters),
            (Matrix left, Matrix right) => left.Sub(right, parameters),

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
        => new Sub(left ?? Left, right ?? Right);
}