// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the decrement operator.
/// </summary>
public class Dec : VariableUnaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Dec"/> class.
    /// </summary>
    /// <param name="argument">The variable.</param>
    public Dec(Variable argument)
        : base(argument)
    {
    }

    /// <inheritdoc />
    protected override object Execute(NumberValue number) => number - 1;

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
    public override IExpression Clone(Variable? variable = null)
        => new Dec(variable ?? Variable);
}