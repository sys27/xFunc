// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the "if-else" statement.
/// </summary>
public class If : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="If"/> class.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="then">The "then" statement.</param>
    public If(IExpression condition, IExpression then)
        : this(ImmutableArray.Create(condition, then))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="If"/> class.
    /// </summary>
    /// <param name="condition">The condition.</param>
    /// <param name="then">The "then" statement.</param>
    /// <param name="else">The "else" statement.</param>
    public If(IExpression condition, IExpression then, IExpression @else)
        : this(ImmutableArray.Create(condition, then, @else))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="If"/> class.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    public If(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Condition.Execute(parameters);
        if (result is bool condition)
        {
            if (condition)
                return Then.Execute(parameters);

            return Else?.Execute(parameters) ?? new NumberValue(0.0);
        }

        throw ExecutionException.For(this);
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
        => new If(arguments ?? Arguments);

    /// <summary>
    /// Gets the condition.
    /// </summary>
    public IExpression Condition => this[0];

    /// <summary>
    /// Gets the "then" statement.
    /// </summary>
    public IExpression Then => this[1];

    /// <summary>
    /// Gets the "else" statement.
    /// </summary>
    public IExpression? Else => ParametersCount == 3 ? this[2] : null;

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 2;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => 3;
}