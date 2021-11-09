// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Results;

/// <summary>
/// Represents the result of calculation.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets the result.
    /// </summary>
    object Result { get; }
}