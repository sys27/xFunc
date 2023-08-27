// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents an Addition operator.
/// </summary>
public class Add : BinaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(
        new[] { Variable.X.Name, Variable.Y.Name },
        new Add(Variable.X, Variable.Y));

    /// <summary>
    /// Initializes a new instance of the <see cref="Add"/> class.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <seealso cref="IExpression"/>
    public Add(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Add"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    internal Add(ImmutableArray<IExpression> arguments)
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
            (NumberValue left, NumberValue right) => left + right,

            (NumberValue left, AngleValue right) => left + right,
            (AngleValue left, NumberValue right) => left + right,
            (AngleValue left, AngleValue right) => left + right,

            (NumberValue left, PowerValue right) => left + right,
            (PowerValue left, NumberValue right) => left + right,
            (PowerValue left, PowerValue right) => left + right,

            (NumberValue left, TemperatureValue right) => left + right,
            (TemperatureValue left, NumberValue right) => left + right,
            (TemperatureValue left, TemperatureValue right) => left + right,

            (NumberValue left, MassValue right) => left + right,
            (MassValue left, NumberValue right) => left + right,
            (MassValue left, MassValue right) => left + right,

            (NumberValue left, LengthValue right) => left + right,
            (LengthValue left, NumberValue right) => left + right,
            (LengthValue left, LengthValue right) => left + right,

            (NumberValue left, TimeValue right) => left + right,
            (TimeValue left, NumberValue right) => left + right,
            (TimeValue left, TimeValue right) => left + right,

            (NumberValue left, AreaValue right) => left + right,
            (AreaValue left, NumberValue right) => left + right,
            (AreaValue left, AreaValue right) => left + right,

            (NumberValue left, VolumeValue right) => left + right,
            (VolumeValue left, NumberValue right) => left + right,
            (VolumeValue left, VolumeValue right) => left + right,

            (NumberValue left, Complex right) => left + right,
            (Complex left, NumberValue right) => left + right,
            (Complex left, Complex right) => left + right,

            (NumberValue left, RationalValue right) => left + right,
            (RationalValue left, NumberValue right) => left + right,
            (RationalValue left, RationalValue right) => left + right,

            (VectorValue left, VectorValue right) => left + right,
            (MatrixValue left, MatrixValue right) => left + right,

            (string left, var right) => left + right,
            (var left, string right) => left + right,

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
        => new Add(left ?? Left, right ?? Right);
}