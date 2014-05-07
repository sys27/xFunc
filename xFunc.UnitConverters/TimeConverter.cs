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

    public class TimeConverter : Converter<TimeUnits>
    {

        static TimeConverter()
        {
            BaseUnit = TimeUnits.Seconds;

            RegisterConversion(TimeUnits.Microseconds, t => t * 1000000, t => t * 0.000001);
            RegisterConversion(TimeUnits.Milliseconds, t => t * 1000, t => t * 0.001);
            RegisterConversion(TimeUnits.Minutes, t => t / 60, t => t * 60);
            RegisterConversion(TimeUnits.Hours, t => t / 3600, t => t * 3600);
            RegisterConversion(TimeUnits.Days, t => t / 86400, t => t * 86400);
            RegisterConversion(TimeUnits.Weeks, t => t / 604800, t => t * 604800);
            RegisterConversion(TimeUnits.Years, t => t / 31536000, t => t * 31536000);
        }

    }

}
