// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Globalization;

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the numerical result.
/// </summary>
public class NumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NumberResult"/> class.
    /// </summary>
    /// <param name="number">The numerical representation of result.</param>
    public NumberResult(double number) => Result = number;

    /// <inheritdoc />
    public override string ToString() => Result.ToString(CultureInfo.InvariantCulture);

    /// <inheritdoc cref="IResult.Result" />
    public double Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}