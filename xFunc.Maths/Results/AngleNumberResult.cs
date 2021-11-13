// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents an angle number result.
/// </summary>
public class AngleNumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AngleNumberResult"/> class.
    /// </summary>
    /// <param name="value">The numerical representation of result.</param>
    public AngleNumberResult(AngleValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public AngleValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}