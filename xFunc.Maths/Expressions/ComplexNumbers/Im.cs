// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace xFunc.Maths.Expressions.ComplexNumbers;

/// <summary>
/// Represent the function which returns the imaginary part of complex number.
/// </summary>
/// <seealso cref="UnaryExpression" />
public class Im : UnaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new Im(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="Im"/> class.
    /// </summary>
    /// <param name="argument">The expression.</param>
    public Im(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Im"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Im(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            Complex complex => new NumberValue(complex.Imaginary),
            _ => throw new ResultIsNotSupportedException(this, result),
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
    public override IExpression Clone(IExpression? argument = null)
        => new Im(argument ?? Argument);
}