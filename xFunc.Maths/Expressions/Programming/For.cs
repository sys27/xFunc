// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the "for" loop.
/// </summary>
public class For : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="For"/> class.
    /// </summary>
    /// <param name="body">The body of loop.</param>
    /// <param name="init">The initializer section.</param>
    /// <param name="cond">The condition section.</param>
    /// <param name="iter">The iterator section.</param>
    public For(IExpression body, IExpression init, IExpression cond, IExpression iter)
        : this(ImmutableArray.Create(body, init, cond, iter))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="For" /> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="arguments"/> is null.</exception>
    public For(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var nested = ExpressionParameters.CreateScoped(parameters);
        Initialization.Execute(nested);

        while (true)
        {
            var condition = Condition.Execute(nested);
            if (condition is not bool conditionResult)
                throw ExecutionException.For(this);

            if (!conditionResult)
                break;

            Body.Execute(nested);
            Iteration.Execute(nested);
        }

        return null!; // TODO:
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
        => new For(arguments ?? Arguments);

    /// <summary>
    /// Gets the body of loop.
    /// </summary>
    public IExpression Body => this[0];

    /// <summary>
    /// Gets the initializer section.
    /// </summary>
    public IExpression Initialization => this[1];

    /// <summary>
    /// Gets the condition section.
    /// </summary>
    public IExpression Condition => this[2];

    /// <summary>
    /// Gets the iterator section.
    /// </summary>
    public IExpression Iteration => this[3];

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 4;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => 4;
}