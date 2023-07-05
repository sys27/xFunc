// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The interface for unit converter.
/// </summary>
/// <typeparam name="TValue">The type of return value of converter.</typeparam>
public interface IConverter<out TValue>
{
    /// <summary>
    /// Converts <paramref name="value"/> to specified <paramref name="unit"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="unit">The unit to convert to.</param>
    /// <returns>The converter value.</returns>
    TValue Convert(object value, string unit);

    /// <summary>
    /// Determines whether the current converter can convert to specified unit.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="unit">The unit to convert to.</param>
    /// <returns>
    /// <c>true</c> if converter can convert using <paramref name="unit"/>; otherwise, <c>false</c>.
    /// </returns>
    bool CanConvertTo(object value, string unit);
}