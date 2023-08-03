// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Trigonometric;

/// <summary>
/// Represents teh Arcsine function.
/// </summary>
public class Arcsin : InverseTrigonometricExpression
{
    private static readonly Lazy<Domain> domain = new Lazy<Domain>(
        () => new DomainBuilder(1)
            .AddRange(r => r.StartInclusive(-NumberValue.One).EndInclusive(NumberValue.One))
            .Build());

    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new Arcsin(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="Arcsin"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    public Arcsin(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Arcsin"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Arcsin(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override AngleValue ExecuteInternal(NumberValue radian)
        => AngleValue.Asin(radian);

    /// <inheritdoc />
    protected override Complex ExecuteComplex(Complex complex)
        => Complex.Asin(complex);

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
        => new Arcsin(argument ?? Argument);

    /// <summary>
    /// Gets the domain of the function.
    /// </summary>
    public static Domain Domain => domain.Value;
}