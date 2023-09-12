// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions;

/// <summary>
/// Represents the empty value (needed for functions that don't return any value).
/// </summary>
public sealed class EmptyValue
{
    private EmptyValue()
    {
    }

    /// <summary>
    /// Gets the single instance of <see cref="EmptyValue"/>.
    /// </summary>
    public static EmptyValue Instance { get; } = new EmptyValue();
}