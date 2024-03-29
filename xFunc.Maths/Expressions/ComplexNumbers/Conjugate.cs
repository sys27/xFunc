// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace xFunc.Maths.Expressions.ComplexNumbers;

/// <summary>
/// Represents the Conjugate function.
/// </summary>
/// <seealso cref="UnaryExpression" />
public class Conjugate : UnaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new Conjugate(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="Conjugate"/> class.
    /// </summary>
    /// <param name="argument">The expression.</param>
    public Conjugate(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Conjugate"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Conjugate(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            Complex complex => Complex.Conjugate(complex),
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
    public override IExpression Clone(IExpression? argument = null)
        => new Conjugate(argument ?? Argument);
}