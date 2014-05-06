using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.UnitConverters
{

    public abstract class Converter<TUnit>
    {

        protected static TUnit BaseUnit;
        protected static Dictionary<TUnit, Func<double, double>> convTo = new Dictionary<TUnit, Func<double, double>>();
        protected static Dictionary<TUnit, Func<double, double>> convFrom = new Dictionary<TUnit, Func<double, double>>();

        public double Convert(double value, TUnit from, TUnit to)
        {
            if (from.Equals(to))
                return value;

            var valueInBaseUnit = from.Equals(BaseUnit) ? value : convFrom[from](value);

            return to.Equals(BaseUnit) ? valueInBaseUnit : convTo[to](valueInBaseUnit);
        }

        protected static void RegisterConversion(TUnit unit, Func<double, double> conversionTo, Func<double, double> conversionFrom)
        {
            convTo.Add(unit, conversionTo);
            convFrom.Add(unit, conversionFrom);
        }

    }

}
