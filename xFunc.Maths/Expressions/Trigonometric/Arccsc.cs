// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;

namespace xFunc.Maths.Expressions.Trigonometric;

/// <summary>
/// Represents the Arccosecant function.
/// </summary>
public class Arccsc : InverseTrigonometricExpression
{
    private static readonly Lazy<Domain> domain = new Lazy<Domain>(
        () => new DomainBuilder(2)
            .AddRange(r => r.Start(NumberValue.NegativeInfinity).EndInclusive(-NumberValue.One))
            .AddRange(r => r.StartInclusive(NumberValue.One).End(NumberValue.PositiveInfinity))
            .Build());

    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new Arccsc(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="Arccsc"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    public Arccsc(IExpression expression)
        : base(expression)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Arccsc"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal Arccsc(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    protected override AngleValue ExecuteInternal(NumberValue radian)
        => AngleValue.Acsc(radian);

    /// <inheritdoc />
    protected override Complex ExecuteComplex(Complex complex)
        => ComplexExtensions.Acsc(complex);

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
        => new Arccsc(argument ?? Argument);

    /// <summary>
    /// Gets the domain of the function.
    /// </summary>
    public static Domain Domain => domain.Value;
}