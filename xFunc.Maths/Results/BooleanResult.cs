// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the boolean result.
/// </summary>
public class BooleanResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanResult"/> class.
    /// </summary>
    /// <param name="value">The value of result.</param>
    public BooleanResult(bool value)
    {
        Result = value;
    }

    /// <inheritdoc />
    public override string ToString()
        => Result.ToString(CultureInfo.InvariantCulture);

    /// <inheritdoc cref="IResult.Result" />
#pragma warning disable SA1623
    public bool Result { get; }
#pragma warning restore SA1623

    /// <inheritdoc />
    object IResult.Result => Result;
}