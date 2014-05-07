using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
