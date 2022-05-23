// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Represents a vector.
/// </summary>
public class Vector : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector"/> class.
    /// </summary>
    /// <param name="args">The values of vector.</param>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    public Vector(IExpression[] args)
        : base(args)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector"/> class.
    /// </summary>
    /// <param name="args">The values of vector.</param>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    public Vector(ImmutableArray<IExpression> args)
        : base(args)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var args = ImmutableArray.CreateBuilder<IExpression>(ParametersCount);

        for (var i = 0; i < ParametersCount; i++)
        {
            if (this[i] is Number)
            {
                args.Add(this[i]);
            }
            else
            {
                var result = this[i].Execute(parameters);
                if (result is NumberValue number)
                    args.Add(new Number(number));
                else
                    throw new ResultIsNotSupportedException(this, result);
            }
        }

        return new Vector(args.ToImmutableArray());
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
    public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
        => new Vector(arguments ?? Arguments);

    /// <summary>
    /// Calculates current vector and returns it as an array.
    /// </summary>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>The array which represents current vector.</returns>
    internal NumberValue[] ToCalculatedArray(ExpressionParameters? parameters)
    {
        var results = new NumberValue[ParametersCount];

        for (var i = 0; i < ParametersCount; i++)
            results[i] = (NumberValue)this[i].Execute(parameters);

        return results;
    }

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 1;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => null;
}