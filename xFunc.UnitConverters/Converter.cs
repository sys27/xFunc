// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace xFunc.UnitConverters
{
    /// <summary>
    /// The base class for converters.
    /// </summary>
    /// <typeparam name="TUnit">The type that represents units (eg. enum).</typeparam>
    [ExcludeFromCodeCoverage]
    public abstract class Converter<TUnit> : IConverter
    {
        /// <summary>
        /// The base unit for this convertor.
        /// </summary>
        protected static TUnit BaseUnit;

        /// <summary>
        /// Dictionary of functions to convert from the base unit type into a specific type.
        /// </summary>
        protected static readonly Dictionary<TUnit, Func<double, double>> convTo = new Dictionary<TUnit, Func<double, double>>();

        /// <summary>
        /// Dictionary of functions to convert from the specified type into the base unit type.
        /// </summary>
        protected static readonly Dictionary<TUnit, Func<double, double>> convFrom = new Dictionary<TUnit, Func<double, double>>();

        double IConverter.Convert(double value, object from, object to)
        {
            return Convert(value, (TUnit) from, (TUnit) to);
        }

        /// <summary>
        /// Converts a value from one unit type to another.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="from">The unit type the provided value is in.</param>
        /// <param name="to">The unit type to convert the value to.</param>
        /// <returns>
        /// The converted value.
        /// </returns>
        public double Convert(double value, TUnit from, TUnit to)
        {
            if (from.Equals(to))
                return value;

            var valueInBaseUnit = from.Equals(BaseUnit) ? value : convFrom[from](value);

            return to.Equals(BaseUnit) ? valueInBaseUnit : convTo[to](valueInBaseUnit);
        }

        /// <summary>
        /// Registers functions for converting to/from a unit.
        /// </summary>
        /// <param name="unit">The type of unit to convert to/from, from the base unit.</param>
        /// <param name="conversionTo">A function to convert from the base unit.</param>
        /// <param name="conversionFrom">A function to convert to the base unit.</param>
        protected static void RegisterConversion(TUnit unit, Func<double, double> conversionTo, Func<double, double> conversionFrom)
        {
            convTo.Add(unit, conversionTo);
            convFrom.Add(unit, conversionFrom);
        }

        /// <summary>
        /// Gets the name of this converter.
        /// </summary>
        /// <value>
        /// The name of this converter.
        /// </value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public abstract IDictionary<object, string> Units { get; }
    }
}