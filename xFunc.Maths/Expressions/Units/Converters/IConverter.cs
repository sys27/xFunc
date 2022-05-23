// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The interface for unit converter.
/// </summary>
public interface IConverter
{
    /// <summary>
    /// Converts <paramref name="value"/> to specified <paramref name="unit"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="unit">The unit to convert to.</param>
    /// <returns>The converter value.</returns>
    object Convert(object value, string unit);
}