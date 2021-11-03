// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using xFunc.UnitConverters.Resources;

namespace xFunc.UnitConverters
{
    /// <summary>
    /// Represents the power converter.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PowerConverter : Converter<PowerUnits>
    {
        private static readonly Lazy<IDictionary<object, string>> units;

        static PowerConverter()
        {
            BaseUnit = PowerUnits.Watts;

            RegisterConversion(PowerUnits.Kilowatts, p => p / 1000, p => p * 1000);
            RegisterConversion(PowerUnits.Horsepower, p => p / 745.69987158227022, p => p * 745.69987158227022);

            units = new Lazy<IDictionary<object, string>>(() => new Dictionary<object, string>
            {
                { PowerUnits.Watts, Resource.Watts },
                { PowerUnits.Kilowatts, Resource.Kilowatts },
                { PowerUnits.Horsepower, Resource.Horsepower }
            });
        }

        /// <summary>
        /// Gets the name of this converter.
        /// </summary>
        /// <value>
        /// The name of this converter.
        /// </value>
        public override string Name => Resource.PowerConverterName;

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public override IDictionary<object, string> Units => units.Value;
    }
}