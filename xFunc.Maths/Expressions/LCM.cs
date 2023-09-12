// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents a least common multiple.
/// </summary>
public class LCM : DifferentParametersExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LCM"/> class.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    public LCM(IExpression[] args)
        : base(args)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LCM"/> class.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    public LCM(ImmutableArray<IExpression> args)
        : base(args)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LCM"/> class.
    /// </summary>
    /// <param name="first">The first operand.</param>
    /// <param name="second">The second operand.</param>
    public LCM(IExpression first, IExpression second)
        : this(ImmutableArray.Create(first, second))
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var lcm = new NumberValue(1.0);
        foreach (var argument in Arguments)
        {
            var result = argument.Execute(parameters);

            lcm = result switch
            {
                NumberValue number => NumberValue.LCM(lcm, number),
                _ => throw ExecutionException.For(this),
            };
        }

        return lcm;
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
        => new LCM(arguments ?? Arguments);

    /// <summary>
    /// Gets the minimum count of parameters.
    /// </summary>
    public override int? MinParametersCount => 2;

    /// <summary>
    /// Gets the maximum count of parameters. <c>null</c> - Infinity.
    /// </summary>
    public override int? MaxParametersCount => null;
}