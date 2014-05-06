using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.UnitConverters
{

    public class TemperatureConverter : Converter<TemperatureUnits>
    {

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
        }

    }

}
