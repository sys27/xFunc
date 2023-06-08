// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the result in the expression form.
/// </summary>
public class ExpressionResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionResult"/> class.
    /// </summary>
    /// <param name="exp">The expression.</param>
    public ExpressionResult(IExpression exp)
        => Result = exp ?? throw new ArgumentNullException(nameof(exp));

    /// <inheritdoc />
    public override string ToString() => Result.ToString()!;

    /// <inheritdoc cref="IResult.Result" />
    public IExpression Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}