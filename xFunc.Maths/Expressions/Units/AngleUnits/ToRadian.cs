// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Units.AngleUnits;

/// <summary>
/// Represents the 'toradian' function.
/// </summary>
public class ToRadian : UnaryExpression
{
    /// <summary>
    /// Gets the lambda for the current expression.
    /// </summary>
    internal static Lambda Lambda { get; } = new Lambda(new[] { Variable.X.Name }, new ToRadian(Variable.X));

    /// <summary>
    /// Initializes a new instance of the <see cref="ToRadian"/> class.
    /// </summary>
    /// <param name="argument">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    public ToRadian(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToRadian"/> class.
    /// </summary>
    /// <param name="arguments">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    internal ToRadian(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => AngleValue.Radian(number),
            AngleValue angle => angle.ToRadian(),
            _ => throw new ResultIsNotSupportedException(this, result),
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
        => new ToRadian(argument ?? Argument);
}