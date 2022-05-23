// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Maths.Expressions.Units.Converters;

/// <summary>
/// The unit converter.
/// </summary>
public class Converter : IConverter
{
    private readonly IConverter<object>[] converters;

    /// <summary>
    /// Initializes a new instance of the <see cref="Converter"/> class.
    /// </summary>
    public Converter()
        => converters = new IConverter<object>[]
        {
            new AngleConverter(),
            new PowerConverter(),
            new TemperatureConverter(),
            new MassConverter(),
            new AreaConverter(),
            new LengthConverter(),
            new TimeConverter(),
            new VolumeConverter(),
        };

    /// <inheritdoc />
    public object Convert(object value, string unit)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentNullException(nameof(unit));

        unit = unit.ToLower();

        foreach (var converter in converters)
            if (converter.CanConvertTo(unit))
                return converter.Convert(value, unit);

        throw new UnitIsNotSupportedException(unit);
    }
}