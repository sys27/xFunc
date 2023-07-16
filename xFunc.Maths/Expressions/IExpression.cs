// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Defines methods to work with expressions.
/// </summary>
public interface IExpression
{
    /// <summary>
    /// Executes this expression. Don't use this method if your expression has variables or user-functions.
    /// </summary>
    /// <returns>A result of the execution.</returns>
    object Execute();

    /// <summary>
    /// Executes this expression.
    /// </summary>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>A result of the execution.</returns>
    /// <seealso cref="ExpressionParameters"/>
    object Execute(IExpressionParameters? parameters);

    /// <summary>
    /// Returns a <see cref="string" /> that represents this instance.
    /// </summary>
    /// <param name="formatter">The formatter.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    string ToString(IFormatter formatter);

    /// <summary>
    /// Analyzes the current expression.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="analyzer">The analyzer.</param>
    /// <returns>The analysis result.</returns>
    TResult Analyze<TResult>(IAnalyzer<TResult> analyzer);

    /// <summary>
    /// Analyzes the current expression.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
    /// <param name="analyzer">The analyzer.</param>
    /// <param name="context">The context.</param>
    /// <returns>The analysis result.</returns>
    TResult Analyze<TResult, TContext>(IAnalyzer<TResult, TContext> analyzer, TContext context);
}