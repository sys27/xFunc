// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The time unit converter.
/// </summary>
public class TimeConverter : IConverter<TimeValue>, IConverter<object>
{
    /// <inheritdoc />
    public TimeValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        if (!TimeUnit.FromName(unit, out var timeUnit))
            throw new UnitIsNotSupportedException(unit);

        return value switch
        {
            TimeValue timeValue => timeValue.To(timeUnit),
            NumberValue numberValue => new TimeValue(numberValue, timeUnit),
            _ => throw new ValueIsNotSupportedException(value),
        };
    }

    /// <inheritdoc/>
    object IConverter<object>.Convert(object value, string unit)
        => Convert(value, unit);

    /// <inheritdoc cref="IConverter{TValue}.CanConvertTo" />
    public bool CanConvertTo(string unit)
        => TimeUnit.Names.Contains(unit.ToLower());
}