// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the vector result.
/// </summary>
public class VectorValueResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VectorValueResult"/> class.
    /// </summary>
    /// <param name="value">The representation of result.</param>
    public VectorValueResult(VectorValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public VectorValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}