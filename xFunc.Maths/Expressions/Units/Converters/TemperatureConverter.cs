// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The temperature unit converter.
/// </summary>
public class TemperatureConverter : IConverter<TemperatureValue>, IConverter<object>
{
    private readonly HashSet<string> units = new HashSet<string>
    {
        "°c", "°f", "k",
    };

    /// <inheritdoc />
    public TemperatureValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        return (value, unit) switch
        {
            (TemperatureValue temperatureValue, "°c") => temperatureValue.ToCelsius(),
            (TemperatureValue temperatureValue, "°f") => temperatureValue.ToFahrenheit(),
            (TemperatureValue temperatureValue, "k") => temperatureValue.ToKelvin(),

            (NumberValue numberValue, "°c") => TemperatureValue.Celsius(numberValue),
            (NumberValue numberValue, "°f") => TemperatureValue.Fahrenheit(numberValue),
            (NumberValue numberValue, "k") => TemperatureValue.Kelvin(numberValue),

            _ when CanConvertTo(unit) => throw new ValueIsNotSupportedException(value),
            _ => throw new UnitIsNotSupportedException(unit),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(string unit)
        => units.Contains(unit.ToLower());
}