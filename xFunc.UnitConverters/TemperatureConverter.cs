// Copyright 2012-2016 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;
using System.Collections.Generic;
using xFunc.UnitConverters.Resources;

namespace xFunc.UnitConverters
{

    /// <summary>
    /// Represents the temperature converter.
    /// </summary>
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
        public override string Name
        {
            get
            {
                return Resource.TemperatureConverterName;
            }
        }

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public override IDictionary<object, string> Units
        {
            get
            {
                return units.Value;
            }
        }

    }

}
