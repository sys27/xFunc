// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Hyperbolic;

/// <summary>
/// The base class for hyperbolic functions.
/// </summary>
/// <seealso cref="UnaryExpression" />
public abstract class HyperbolicExpression : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HyperbolicExpression" /> class.
    /// </summary>
    /// <param name="argument">The expression.</param>
    protected HyperbolicExpression(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HyperbolicExpression"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal HyperbolicExpression(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <summary>
    /// Calculates this mathematical expression (using radian).
    /// </summary>
    /// <param name="angleValue">The angle.</param>
    /// <returns>
    /// A result of the calculation.
    /// </returns>
    /// <seealso cref="ExpressionParameters" />
    protected abstract NumberValue ExecuteInternal(AngleValue angleValue);

    /// <summary>
    /// Executes this expression.
    /// </summary>
    /// <param name="complex">The calculation result of argument.</param>
    /// <returns>
    /// A result of the execution.
    /// </returns>
    /// <seealso cref="ExpressionParameters" />
    protected abstract Complex ExecuteComplex(Complex complex);

    /// <inheritdoc />
    public override object Execute(IExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => ExecuteInternal(AngleValue.Degree(number)),
            AngleValue angle => ExecuteInternal(angle),
            Complex complex => ExecuteComplex(complex),
            _ => throw new ResultIsNotSupportedException(this, result),
        };
    }
}