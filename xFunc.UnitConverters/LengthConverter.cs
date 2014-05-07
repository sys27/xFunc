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

    public class LengthConverter : Converter<LengthUnits>
    {

        static LengthConverter()
        {
            BaseUnit = LengthUnits.Metres;

            RegisterConversion(LengthUnits.Nanometres, t => 0, t => 0);
            RegisterConversion(LengthUnits.Micrometers, t => 0, t => 0);
            RegisterConversion(LengthUnits.Millimeters, t => 0, t => 0);
            RegisterConversion(LengthUnits.Centimeters, t => 0, t => 0);
            RegisterConversion(LengthUnits.Kilometers, t => 0, t => 0);
            RegisterConversion(LengthUnits.Inches, t => 0, t => 0);
            RegisterConversion(LengthUnits.Feet, t => 0, t => 0);
            RegisterConversion(LengthUnits.Yards, t => 0, t => 0);
            RegisterConversion(LengthUnits.Miles, t => 0, t => 0);
            RegisterConversion(LengthUnits.NauticalMiles, t => 0, t => 0);
            RegisterConversion(LengthUnits.Fathoms, t => 0, t => 0);
            RegisterConversion(LengthUnits.Chains, t => 0, t => 0);
            RegisterConversion(LengthUnits.Rod, t => 0, t => 0);
            RegisterConversion(LengthUnits.AstronomicalUnits, t => 0, t => 0);
            RegisterConversion(LengthUnits.LigthYears, t => 0, t => 0);
            RegisterConversion(LengthUnits.Parsecs, t => 0, t => 0);
        }

    }

}
