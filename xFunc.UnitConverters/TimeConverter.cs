using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
