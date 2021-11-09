// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the numerical result.
/// </summary>
public class ComplexNumberResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComplexNumberResult"/> class.
    /// </summary>
    /// <param name="complex">The numerical representation of result.</param>
    public ComplexNumberResult(Complex complex) => Result = complex;

    /// <inheritdoc />
    public override string ToString() => Result.Format();

    /// <inheritdoc cref="IResult.Result" />
    public Complex Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}