// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The mass unit converter.
/// </summary>
public class MassConverter : IConverter<MassValue>, IConverter<object>
{
    /// <inheritdoc />
    public MassValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!MassUnit.FromName(unit, out var massUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            MassValue massValue => massValue.To(massUnit),
            NumberValue numberValue => new MassValue(numberValue, massUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(object value, string unit)
        => value is MassValue or NumberValue && MassUnit.Names.Contains(unit.ToLower());
}