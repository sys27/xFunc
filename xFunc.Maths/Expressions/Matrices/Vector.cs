// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Represents a vector expression.
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
        var values = new NumberValue[ParametersCount];

        for (var i = 0; i < values.Length; i++)
        {
            var result = this[i].Execute(parameters);
            if (result is not NumberValue number)
                throw ExecutionException.For(this);

            values[i] = number;
        }

        return Unsafe.As<NumberValue[], VectorValue>(ref values);
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
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 1;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => null;
}