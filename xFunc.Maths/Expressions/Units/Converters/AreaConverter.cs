// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The area unit converter.
/// </summary>
public class AreaConverter : IConverter<AreaValue>, IConverter<object>
{
    /// <inheritdoc />
    public AreaValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!AreaUnit.FromName(unit, out var areaUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            AreaValue areaValue => areaValue.To(areaUnit),
            NumberValue numberValue => new AreaValue(numberValue, areaUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(string unit)
        => AreaUnit.Names.Contains(unit.ToLower());
}