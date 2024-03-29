// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Represents a dot product of vectors.
/// </summary>
public class DotProduct : BinaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(
        new[] { Variable.X.Name, Variable.Y.Name },
        new DotProduct(Variable.X, Variable.Y));

    /// <summary>
    /// Initializes a new instance of the <see cref="DotProduct"/> class.
    /// </summary>
    /// <param name="left">The left (first) operand.</param>
    /// <param name="right">The right (second) operand.</param>
    public DotProduct(IExpression left, IExpression right)
        : base(left, right)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DotProduct"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <seealso cref="IExpression"/>
    public DotProduct(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var left = Left.Execute(parameters);
        var right = Right.Execute(parameters);

        return (left, right) switch
        {
            (VectorValue leftVector, VectorValue rightVector) => leftVector * rightVector,
            _ => throw ExecutionException.For(this),
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
        => new DotProduct(left ?? Left, right ?? Right);
}