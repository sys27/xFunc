// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace xFunc.Maths.Expressions.Programming;

/// <summary>
/// Represents the "/=" operator.
/// </summary>
public class DivAssign : VariableBinaryExpression
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivAssign" /> class.
    /// </summary>
    /// <param name="variable">The variable.</param>
    /// <param name="exp">The expression.</param>
    public DivAssign(Variable variable, IExpression exp)
        : base(variable, exp)
    {
    }

    /// <inheritdoc />
    protected override object Execute(NumberValue variableValue, NumberValue value)
        => variableValue / value;

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
    public override IExpression Clone(Variable? variable = null, IExpression? value = null)
        => new DivAssign(variable ?? Variable, value ?? Value);
}