// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents a function in code.
/// </summary>
public class DelegateExpression : IExpression
{
    private readonly Func<ExpressionParameters?, object> func;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateExpression"/> class.
    /// </summary>
    /// <param name="func">The delegate of function.</param>
    public DelegateExpression(Func<ExpressionParameters?, object> func)
        => this.func = func;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        return func.Equals(((DelegateExpression)obj).func);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    public object Execute() => func(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
        => func(parameters);

    /// <inheritdoc />
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this, context);
    }
}