// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents a time number result.
/// </summary>
public class TimeNumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeNumberResult"/> class.
    /// </summary>
    /// <param name="value">The numerical representation of result.</param>
    public TimeNumberResult(TimeValue value) => Result = value;

    /// <inheritdoc />
    public override string ToString() => Result.ToString();

    /// <inheritdoc cref="IResult.Result" />
    public TimeValue Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}