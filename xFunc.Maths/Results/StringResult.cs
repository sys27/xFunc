// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the string result.
/// </summary>
public class StringResult : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringResult"/> class.
    /// </summary>
    /// <param name="str">The string representation of result.</param>
    public StringResult(string str)
        => Result = str ?? throw new ArgumentNullException(nameof(str));

    /// <inheritdoc />
    public override string ToString() => Result;

    /// <inheritdoc cref="IResult.Result" />
    public string Result { get; }

    /// <inheritdoc />
    object IResult.Result => Result;
}