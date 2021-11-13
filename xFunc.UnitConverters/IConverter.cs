// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.UnitConverters;

/// <summary>
/// The base interface for all converters.
/// </summary>
public interface IConverter
{
    /// <summary>
    /// Converts a value from one unit type to another.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="from">The unit type the provided value is in.</param>
    /// <param name="to">The unit type to convert the value to.</param>
    /// <returns>
    /// The converted value.
    /// </returns>
    double Convert(double value, object from, object to);

    /// <summary>
    /// Gets the name of this converter.
    /// </summary>
    /// <value>
    /// The name of this converter.
    /// </value>
    string Name { get; }

    /// <summary>
    /// Gets the units.
    /// </summary>
    /// <value>
    /// The units.
    /// </value>
    IDictionary<object, string> Units { get; }
}