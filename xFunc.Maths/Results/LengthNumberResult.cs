// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents a length number result.
/// </summary>
public class LengthNumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LengthNumberResult"/> class.
    /// </summary>
    /// <param name="value">The numerical representation of result.</param>
    public LengthNumberResult(LengthValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public LengthValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}