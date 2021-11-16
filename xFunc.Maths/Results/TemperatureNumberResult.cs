// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents a temperature number result.
/// </summary>
public class TemperatureNumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TemperatureNumberResult"/> class.
    /// </summary>
    /// <param name="value">The numerical representation of result.</param>
    public TemperatureNumberResult(TemperatureValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public TemperatureValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}