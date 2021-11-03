// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using xFunc.UnitConverters.Resources;

namespace xFunc.UnitConverters
{
    /// <summary>
    /// Represents the temperature converter.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TemperatureConverter : Converter<TemperatureUnits>
    {
        private static readonly Lazy<IDictionary<object, string>> units;

        static TemperatureConverter()
        {
            BaseUnit = TemperatureUnits.Celsius;
            RegisterConversion(TemperatureUnits.Fahrenheit, t => t * (9.0 / 5) + 32, t => (t - 32) * 5.0 / 9);
            RegisterConversion(TemperatureUnits.Kelvin, t => t + 273.15, t => t - 273.15);
            RegisterConversion(TemperatureUnits.Rankine, t => (t + 273.15) * (9.0 / 5), t => (t - 491.67) * (5.0 / 9));
            RegisterConversion(TemperatureUnits.Delisle, t => (100 - t) * (3.0 / 2), t => 100 - t * (2.0 / 3));
            RegisterConversion(TemperatureUnits.Newton, t => t * (0.33), t => t * (100.0 / 33));
            RegisterConversion(TemperatureUnits.Réaumur, t => t * (4.0 / 5), t => t * (5.0 / 4));
            RegisterConversion(TemperatureUnits.Rømer, t => t * (21.0 / 40) + 7.5, t => (t - 7.5) * (40.0 / 21));

            units = new Lazy<IDictionary<object, string>>(() => new Dictionary<object, string>
            {
                { TemperatureUnits.Celsius, Resource.Celsius },
                { TemperatureUnits.Fahrenheit, Resource.Fahrenheit },
                { TemperatureUnits.Kelvin, Resource.Kelvin },
                { TemperatureUnits.Rankine, Resource.Rankine },
                { TemperatureUnits.Delisle, Resource.Delisle },
                { TemperatureUnits.Newton, Resource.Newton },
                { TemperatureUnits.Réaumur, Resource.Réaumur },
                { TemperatureUnits.Rømer, Resource.Rømer },
            });
        }

        /// <summary>
        /// Gets the name of this converter.
        /// </summary>
        /// <value>
        /// The name of this converter.
        /// </value>
        public override string Name => Resource.TemperatureConverterName;

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public override IDictionary<object, string> Units => units.Value;
    }
}