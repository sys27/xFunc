using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.UnitConverters
{

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

    }

}
