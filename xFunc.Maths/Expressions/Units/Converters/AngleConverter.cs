// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The angle unit converter.
/// </summary>
public class AngleConverter : IConverter<AngleValue>, IConverter<object>
{
    private readonly HashSet<string> units = new HashSet<string>
    {
        "rad", "radian", "radians",
        "deg", "degree", "degrees",
        "grad", "gradian", "gradians",
    };

    /// <inheritdoc />
    public AngleValue Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        return (value, unit) switch
        {
            (AngleValue angleValue, "rad" or "radian" or "radians")
                => angleValue.ToRadian(),
            (AngleValue angleValue, "deg" or "degree" or "degrees")
                => angleValue.ToDegree(),
            (AngleValue angleValue, "grad" or "gradian" or "gradians")
                => angleValue.ToGradian(),

            (NumberValue numberValue, "rad" or "radian" or "radians")
                => AngleValue.Radian(numberValue),
            (NumberValue numberValue, "deg" or "degree" or "degrees")
                => AngleValue.Degree(numberValue),
            (NumberValue numberValue, "grad" or "gradian" or "gradians")
                => AngleValue.Gradian(numberValue),

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