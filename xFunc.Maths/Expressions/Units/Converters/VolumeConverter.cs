// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The volume unit converter.
/// </summary>
public class VolumeConverter : IConverter<VolumeValue>, IConverter<object>
{
    /// <inheritdoc />
    public VolumeValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!VolumeUnit.FromName(unit, out var volumeUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            VolumeValue volumeValue => volumeValue.To(volumeUnit),
            NumberValue numberValue => new VolumeValue(numberValue, volumeUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(string unit)
        => VolumeUnit.Names.Contains(unit.ToLower());
}