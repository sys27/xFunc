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

namespace xFunc.UnitConverters
{

    public class AreaConverter : Converter<AreaUnits>
    {

        static AreaConverter()
        {
            BaseUnit = AreaUnits.SquareMetres;

            RegisterConversion(AreaUnits.SquareMillimeters, t => t * 1000000, t => t / 1000000);
            RegisterConversion(AreaUnits.SquareCentimeters, t => t * 10000, t => t / 10000);
            RegisterConversion(AreaUnits.SquareKilometers, t => t / 1000000, t => t * 1000000);
            RegisterConversion(AreaUnits.Hectares, t => t / 10000, t => t * 10000);
            RegisterConversion(AreaUnits.SquareInches, t => t * 1550.0031, t => t / 1550.0031);
            RegisterConversion(AreaUnits.SquareFeet, t => t * 10.763911, t => t / 10.763911);
            RegisterConversion(AreaUnits.SquareYards, t => t * 1.195990, t => t / 1.195990);
            RegisterConversion(AreaUnits.Acres, t => t * 0.000247105381, t => t / 0.000247105381);
            RegisterConversion(AreaUnits.SquareMiles, t => t * 2589988.110336, t => t / 2589988.110336);
        }

    }

}
