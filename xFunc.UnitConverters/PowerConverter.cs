// Copyright 2012-2017 Dmitry Kischenko
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
    /// Represents the power converter.
    /// </summary>
    public class PowerConverter : Converter<PowerUnits>
    {

        private static readonly Lazy<IDictionary<object, string>> units;

        static PowerConverter()
        {
            BaseUnit = PowerUnits.Watts;

            RegisterConversion(PowerUnits.Kilowatts, p => p / 1000, p => p * 1000);
            RegisterConversion(PowerUnits.Horsepower, p => p / 745.69987158227022, p => p * 745.69987158227022);

            units = new Lazy<IDictionary<object, string>>(() => new Dictionary<object, string>
            {
                { PowerUnits.Watts, Resource.Watts },
                { PowerUnits.Kilowatts, Resource.Kilowatts },
                { PowerUnits.Horsepower, Resource.Horsepower }
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
                return Resource.PowerConverterName;
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
