// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The angle unit converter.
/// </summary>
public class AngleConverter : IConverter<AngleValue>, IConverter<object>
{
    /// <inheritdoc />
    public AngleValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!AngleUnit.FromName(unit, out var angleUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            AngleValue angleValue => angleValue.To(angleUnit),
            NumberValue numberValue => new AngleValue(numberValue, angleUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(object value, string unit)
        => value is AngleValue or NumberValue && AngleUnit.Names.Contains(unit.ToLower());
}