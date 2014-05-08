// Copyright 2012-2014 Dmitry Kischenko
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
    /// Represents the mass converter.
    /// </summary>
    public class MassConverter : Converter<MassUnits>
    {

        static MassConverter()
        {
            BaseUnit = MassUnits.Kilograms;

            RegisterConversion(MassUnits.Milligrams, m => m * 1000000, m => m / 1000000);
            RegisterConversion(MassUnits.Grams, m => m * 1000, m => m / 1000);
            RegisterConversion(MassUnits.Slugs, m => m / 14.593903, m => m * 14.593903);
            RegisterConversion(MassUnits.Pounds, m => m * 2.20462262184878, m => m / 2.20462262184878);
            RegisterConversion(MassUnits.Tonne, m => m / 1000, m => m * 1000);
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
                return Resource.MassConverterName;
            }
        }

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public override IDictionary<MassUnits, string> Units
        {
            get { throw new NotImplementedException(); }
        }

    }

}
