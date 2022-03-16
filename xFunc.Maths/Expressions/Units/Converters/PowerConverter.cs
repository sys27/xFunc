// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The power unit converter.
/// </summary>
public class PowerConverter : IConverter<PowerValue>, IConverter<object>
{
    /// <inheritdoc />
    public PowerValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!PowerUnit.FromName(unit, out var powerUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            PowerValue powerValue => powerValue.To(powerUnit),
            NumberValue numberValue => new PowerValue(numberValue, powerUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(string unit)
        => PowerUnit.Names.Contains(unit.ToLower());
}