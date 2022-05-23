// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The temperature unit converter.
/// </summary>
public class TemperatureConverter : IConverter<TemperatureValue>, IConverter<object>
{
    /// <inheritdoc />
    public TemperatureValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!TemperatureUnit.FromName(unit, out var temperatureUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            TemperatureValue temperatureValue => temperatureValue.To(temperatureUnit),
            NumberValue numberValue => new TemperatureValue(numberValue, temperatureUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(string unit)
        => TemperatureUnit.Names.Contains(unit.ToLower());
}