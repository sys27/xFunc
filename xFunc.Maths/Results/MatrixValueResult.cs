// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the matrix result.
/// </summary>
public class MatrixValueResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MatrixValueResult"/> class.
    /// </summary>
    /// <param name="value">The representation of result.</param>
    public MatrixValueResult(MatrixValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public MatrixValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}