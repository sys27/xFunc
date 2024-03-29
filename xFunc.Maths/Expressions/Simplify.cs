// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the Simplify function.
/// </summary>
public class Simplify : UnaryExpression
{
    private readonly ISimplifier simplifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="Simplify"/> class.
    /// </summary>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="expression">The argument of function.</param>
    /// <exception cref="ArgumentNullException"><paramref name="simplifier"/> is <c>null</c>.</exception>
    public Simplify(ISimplifier simplifier, IExpression expression)
        : base(expression)
        => this.simplifier = simplifier ??
                             throw new ArgumentNullException(nameof(simplifier));

    /// <summary>
    /// Initializes a new instance of the <see cref="Simplify"/> class.
    /// </summary>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Simplify(ISimplifier simplifier, ImmutableArray<IExpression> arguments)
        : base(arguments)
        => this.simplifier = simplifier;

    /// <summary>
    /// Executes this expression.
    /// </summary>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>
    /// A result of the execution.
    /// </returns>
    /// <seealso cref="ExpressionParameters" />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);
        if (result is not Lambda lambda)
            throw ExecutionException.For(this);

        var simplifiedExpression = lambda.Body
            .Analyze(simplifier)
            .ToLambda(lambda.Parameters);

        return simplifiedExpression;
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
    public override IExpression Clone(IExpression? argument = null)
        => new Simplify(simplifier, argument ?? Argument);
}