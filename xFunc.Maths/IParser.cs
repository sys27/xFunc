// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

/// <summary>
/// The interface for parser.
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parses the specified function.
    /// </summary>
    /// <param name="expression">The string that contains the functions and operators.</param>
    /// <returns>The parsed expression.</returns>
    IExpression Parse(string expression);
}