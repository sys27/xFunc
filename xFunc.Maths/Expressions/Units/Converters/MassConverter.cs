// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The mass unit converter.
/// </summary>
public class MassConverter : IConverter<MassValue>, IConverter<object>
{
    private readonly HashSet<string> units = new HashSet<string>
    {
        "mg", "g", "kg", "t", "oz", "lb",
    };

    /// <inheritdoc />
    public MassValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        return (value, unit) switch
        {
            (MassValue massValue, "mg") => massValue.ToMilligram(),
            (MassValue massValue, "g") => massValue.ToGram(),
            (MassValue massValue, "kg") => massValue.ToKilogram(),
            (MassValue massValue, "t") => massValue.ToTonne(),
            (MassValue massValue, "oz") => massValue.ToOunce(),
            (MassValue massValue, "lb") => massValue.ToPound(),

            (NumberValue numberValue, "mg") => MassValue.Milligram(numberValue),
            (NumberValue numberValue, "g") => MassValue.Gram(numberValue),
            (NumberValue numberValue, "kg") => MassValue.Kilogram(numberValue),
            (NumberValue numberValue, "t") => MassValue.Tonne(numberValue),
            (NumberValue numberValue, "oz") => MassValue.Ounce(numberValue),
            (NumberValue numberValue, "lb") => MassValue.Pound(numberValue),

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