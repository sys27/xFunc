// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The length unit converter.
/// </summary>
public class LengthConverter : IConverter<LengthValue>, IConverter<object>
{
    /// <inheritdoc />
    public LengthValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!LengthUnit.FromName(unit, out var lengthUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            LengthValue lengthValue => lengthValue.To(lengthUnit),
            NumberValue numberValue => new LengthValue(numberValue, lengthUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(object value, string unit)
        => value is LengthValue or NumberValue && LengthUnit.Names.Contains(unit.ToLower());
}