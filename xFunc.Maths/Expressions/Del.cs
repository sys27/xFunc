// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
        this.differentiator = differentiator ?? throw new ArgumentNullException(nameof(differentiator));
        this.simplifier = simplifier ?? throw new ArgumentNullException(nameof(simplifier));
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
        var result = Argument.Execute(parameters);
        if (result is not Lambda lambda)
            throw ExecutionException.For(this);

        var body = lambda.Body;
        var variables = new HashSet<Variable>();
        GetAllVariables(body, variables);

        var vectorItems = new IExpression[variables.Count];
        var i = 0;

        foreach (var variable in variables)
        {
            vectorItems[i] = body
                .Analyze(differentiator, new DifferentiatorContext(variable))
                .Analyze(simplifier);

            i++;
        }

        var resultLambda = new Vector(Unsafe.As<IExpression[], ImmutableArray<IExpression>>(ref vectorItems))
            .ToLambda(lambda.Parameters);

        return resultLambda;
    }

    private static void GetAllVariables(IExpression expression, HashSet<Variable> collection)
    {
        if (expression is UnaryExpression un)
        {
            GetAllVariables(un.Argument, collection);
        }
        else if (expression is BinaryExpression bin)
        {
            GetAllVariables(bin.Left, collection);
            GetAllVariables(bin.Right, collection);
        }
        else if (expression is DifferentParametersExpression diff)
        {
            foreach (var exp in diff.Arguments)
                GetAllVariables(exp, collection);
        }
        else if (expression is Variable variable)
        {
            collection.Add(variable);
        }
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