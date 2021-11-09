// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using static xFunc.Maths.ThrowHelpers;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the "Del" operator.
/// </summary>
/// <seealso cref="UnaryExpression" />
public class Del : UnaryExpression
{
    private readonly IDifferentiator differentiator;
    private readonly ISimplifier simplifier;

    /// <summary>
    /// Initializes a new instance of the <see cref="Del"/> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="expression">The expression.</param>
    public Del(IDifferentiator differentiator, ISimplifier simplifier, IExpression expression)
        : base(expression)
    {
        if (differentiator is null)
            ArgNull(ExceptionArgument.differentiator);
        if (simplifier is null)
            ArgNull(ExceptionArgument.simplifier);

        this.differentiator = differentiator;
        this.simplifier = simplifier;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Del"/> class.
    /// </summary>
    /// <param name="differentiator">The differentiator.</param>
    /// <param name="simplifier">The simplifier.</param>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Del(
        IDifferentiator differentiator,
        ISimplifier simplifier,
        ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
        this.differentiator = differentiator;
        this.simplifier = simplifier;
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var context = new DifferentiatorContext(parameters);

        var variables = Helpers.GetAllVariables(Argument).ToList();
        var vector = ImmutableArray.CreateBuilder<IExpression>(variables.Count);

        foreach (var variable in variables)
        {
            context.Variable = variable;

            vector.Add(Argument
                .Analyze(differentiator, context)
                .Analyze(simplifier));
        }

        return new Vector(vector.ToImmutableArray());
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
        => new Del(differentiator, simplifier, argument ?? Argument);
}