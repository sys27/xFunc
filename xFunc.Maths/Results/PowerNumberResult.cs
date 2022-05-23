// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents a power number result.
/// </summary>
public class PowerNumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PowerNumberResult"/> class.
    /// </summary>
    /// <param name="value">The numerical representation of result.</param>
    public PowerNumberResult(PowerValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public PowerValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}