// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions;

/// <summary>
/// Extension methods for <see cref="Lambda"/> and <see cref="LambdaExpression"/>.
/// </summary>
public static class LambdaExtensions
{
    /// <summary>
    /// Converts <paramref name="exp"/> to <see cref="Lambda"/> without parameters.
    /// </summary>
    /// <param name="exp">The expression to convert.</param>
    /// <returns>The lambda.</returns>
    public static Lambda ToLambda(this IExpression exp)
        => new Lambda(exp);

    /// <summary>
    /// Converts <paramref name="exp"/> to <see cref="Lambda"/> without parameters.
    /// </summary>
    /// <param name="exp">The expression to convert.</param>
    /// <param name="variables">The list of lambda parameters.</param>
    /// <returns>The lambda.</returns>
    public static Lambda ToLambda(this IExpression exp, params string[] variables)
        => new Lambda(variables, exp);

    /// <summary>
    /// Converts <paramref name="exp"/> to <see cref="Lambda"/> without parameters.
    /// </summary>
    /// <param name="exp">The expression to convert.</param>
    /// <param name="variables">The list of lambda parameters.</param>
    /// <returns>The lambda.</returns>
    public static Lambda ToLambda(this IExpression exp, ImmutableArray<string> variables)
        => new Lambda(variables, exp);

    /// <summary>
    /// Converts <paramref name="exp"/> to <see cref="LambdaExpression"/> without parameters.
    /// </summary>
    /// <param name="exp">The expression to convert.</param>
    /// <returns>The lambda expression.</returns>
    public static LambdaExpression ToLambdaExpression(this IExpression exp)
        => ToLambda(exp).AsExpression();

    /// <summary>
    /// Converts <paramref name="exp"/> to <see cref="LambdaExpression"/> without parameters.
    /// </summary>
    /// <param name="exp">The expression to convert.</param>
    /// <param name="variables">The list of lambda parameters.</param>
    /// <returns>The lambda expression.</returns>
    public static LambdaExpression ToLambdaExpression(this IExpression exp, params string[] variables)
        => ToLambda(exp, variables).AsExpression();
}