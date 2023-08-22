// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths;

/// <summary>
/// The interface for parser.
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parses the specified <paramref name="expression"/>.
    /// </summary>
    /// <param name="expression">The string expression.</param>
    /// <returns>The parsed expression.</returns>
    /// <seealso cref="IExpression"/>
    IExpression Parse(string expression);
}