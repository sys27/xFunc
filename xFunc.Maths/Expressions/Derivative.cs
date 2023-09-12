// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the 'deriv' function.
/// </summary>
public class Derivative : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Derivative" /> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="expression">The expression.</param>
    public Derivative(
        IDifferentiator differentiator,
        ISimplifier simplifier,
        IExpression expression)
        : this(differentiator, simplifier, ImmutableArray.Create(expression))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Derivative" /> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="variable">The variable.</param>
    public Derivative(
        IDifferentiator differentiator,
        ISimplifier simplifier,
        IExpression expression,
        Variable variable)
        : this(differentiator, simplifier, ImmutableArray.Create(expression, variable))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Derivative" /> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="variable">The variable.</param>
    /// <param name="point">The point of derivation.</param>
    public Derivative(
        IDifferentiator differentiator,
        ISimplifier simplifier,
        IExpression expression,
        Variable variable,
        Number point)
        : this(differentiator, simplifier, ImmutableArray.Create(expression, variable, point))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Derivative" /> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="args">The arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    internal Derivative(
        IDifferentiator differentiator,
        ISimplifier simplifier,
        ImmutableArray<IExpression> args)
        : base(args)
    {
        Differentiator = differentiator ?? throw new ArgumentNullException(nameof(differentiator));
        Simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Expression.Execute(parameters);
        if (result is not Lambda lambda)
            throw ExecutionException.For(this);

        var variable = Variable;
        var context = new DifferentiatorContext(variable);
        var derivative = lambda.Body.Analyze(Differentiator, context);

        var point = DerivativePoint;
        if (point is not null)
        {
            var nested = ExpressionParameters.CreateScoped(parameters);
            nested[variable.Name] = point.Value;

            return derivative.Execute(nested);
        }

        return derivative
            .Analyze(Simplifier)
            .ToLambda(lambda.Parameters);
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
    public override IExpression Clone(ImmutableArray<IExpression>? arguments = null)
        => new Derivative(Differentiator, Simplifier, arguments ?? Arguments);

    /// <summary>
    /// Gets the expression.
    /// </summary>
    public IExpression Expression => this[0];

    /// <summary>
    /// Gets the variable.
    /// </summary>
    public Variable Variable => ParametersCount >= 2 ? (Variable)this[1] : Variable.X;

    /// <summary>
    /// Gets the derivative point.
    /// </summary>
    public Number? DerivativePoint => ParametersCount >= 3 ? (Number)this[2] : null;

    /// <summary>
    /// Gets the simplifier.
    /// </summary>
    public ISimplifier Simplifier { get; }

    /// <summary>
    /// Gets the differentiator.
    /// </summary>
    public IDifferentiator Differentiator { get; }

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 1;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => 3;
}