using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
