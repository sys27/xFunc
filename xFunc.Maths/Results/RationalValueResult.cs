// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the rational number result.
/// </summary>
public class RationalValueResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RationalValueResult"/> class.
    /// </summary>
    /// <param name="value">The rational number.</param>
    public RationalValueResult(RationalValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public RationalValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}