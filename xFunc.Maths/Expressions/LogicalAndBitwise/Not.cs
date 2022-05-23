// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.LogicalAndBitwise;

/// <summary>
/// Represents a bitwise NOT operator.
/// </summary>
public class Not : UnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Not"/> class.
    /// </summary>
    /// <param name="expression">The argument of function.</param>
    /// <seealso cref="IExpression"/>
    public Not(IExpression expression)
        : base(expression)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        var arg = Argument.Execute(parameters);

        return arg switch
        {
            bool boolean => !boolean,
            NumberValue number => ~number,
            _ => throw new ResultIsNotSupportedException(this, arg),
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
        => new Not(argument ?? Argument);
}