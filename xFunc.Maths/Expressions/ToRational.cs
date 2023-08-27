// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the function to convert <see cref="NumberValue" /> to <see cref="RationalValue"/>.
/// </summary>
public class ToRational : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ToRational"/> class.
    /// </summary>
    /// <param name="argument">The expression.</param>
    public ToRational(IExpression argument)
        : base(argument)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ToRational"/> class.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    internal ToRational(ImmutableArray<IExpression> arguments)
        : base(arguments)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var result = Argument.Execute(parameters);

        return result switch
        {
            NumberValue number => new RationalValue(number, NumberValue.One),

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
        => new ToRational(argument ?? Argument);
}