// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Exponentiation operator.
/// </summary>
[FunctionName("pow")]
public class Pow : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pow"/> class.
    /// </summary>
    /// <param name="base">The base.</param>
    /// <param name="exponent">The exponent.</param>
    public Pow(IExpression @base, IExpression exponent)
        : base(@base, exponent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pow"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    internal Pow(ImmutableArray<IExpression> arguments)
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
            (NumberValue left, NumberValue right) => NumberValue.Pow(left, right),
            (NumberValue left, Complex right) => Complex.Pow(left.Number, right),
            (Complex left, NumberValue right) => NumberValue.Pow(left, right),
            (Complex left, Complex right) => Complex.Pow(left, right),
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
        => new Pow(left ?? Left, right ?? Right);
}