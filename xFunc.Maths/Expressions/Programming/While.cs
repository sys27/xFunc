// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the "while" loop.
/// </summary>
public class While : BinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="While"/> class.
    /// </summary>
    /// <param name="body">The body of while loop.</param>
    /// <param name="condition">The condition of loop.</param>
    public While(IExpression body, IExpression condition)
        : base(body, condition)
    {
    }

    /// <inheritdoc />
    public override object Execute(ExpressionParameters? parameters)
    {
        while ((bool)Right.Execute(parameters))
            Left.Execute(parameters);

        return double.NaN;
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
    public override IExpression Clone(IExpression? left = null, IExpression? right = null)
        => new While(left ?? Left, right ?? Right);
}