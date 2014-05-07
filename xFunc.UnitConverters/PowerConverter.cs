// Copyright 2014 Dmitry Kischenko
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

    public class PowerConverter : Converter<PowerUnits>
    {

        static PowerConverter()
        {
            BaseUnit = PowerUnits.Watts;

            RegisterConversion(PowerUnits.Kilowatts, p => p / 1000, p => p * 1000);
            RegisterConversion(PowerUnits.Horsepower, p => p / 745.69987158227022, p => p * 745.69987158227022);
        }

    }

}
