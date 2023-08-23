// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Parameters;

/// <summary>
/// Contains types of parameter.
/// </summary>
public enum ParameterType
{
    /// <summary>
    /// The normal parameter.
    /// </summary>
    Normal,

    /// <summary>
    /// The read-only parameter. It can be added/removed, but it can't be changed.
    /// </summary>
    ReadOnly,

    /// <summary>
    /// The constant parameter. It can be added, but can't be changed or removed.
    /// </summary>
    Constant,
}