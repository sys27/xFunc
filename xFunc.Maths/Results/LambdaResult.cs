// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the function result.
/// </summary>
public class LambdaResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LambdaResult"/> class.
    /// </summary>
    /// <param name="value">The numerical representation of result.</param>
    public LambdaResult(Lambda value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public Lambda Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}