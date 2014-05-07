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
