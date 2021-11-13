// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The power unit converter.
/// </summary>
public class PowerConverter : IConverter<PowerValue>, IConverter<object>
{
    private readonly HashSet<string> units = new HashSet<string>
    {
        "w", "kw", "hp",
    };

    /// <inheritdoc />
    public PowerValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        return (value, unit) switch
        {
            (PowerValue powerValue, "w") => powerValue.ToWatt(),
            (PowerValue powerValue, "kw") => powerValue.ToKilowatt(),
            (PowerValue powerValue, "hp") => powerValue.ToHorsepower(),

            (NumberValue numberValue, "w") => PowerValue.Watt(numberValue),
            (NumberValue numberValue, "kw") => PowerValue.Kilowatt(numberValue),
            (NumberValue numberValue, "hp") => PowerValue.Horsepower(numberValue),

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