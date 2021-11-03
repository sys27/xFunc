// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using xFunc.UnitConverters.Resources;

namespace xFunc.UnitConverters
{
    /// <summary>
    /// Represents the mass converter.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MassConverter : Converter<MassUnits>
    {
        private static readonly Lazy<IDictionary<object, string>> units;

        static MassConverter()
        {
            BaseUnit = MassUnits.Kilograms;

            RegisterConversion(MassUnits.Milligrams, m => m * 1000000, m => m / 1000000);
            RegisterConversion(MassUnits.Grams, m => m * 1000, m => m / 1000);
            RegisterConversion(MassUnits.Slugs, m => m / 14.593903, m => m * 14.593903);
            RegisterConversion(MassUnits.Pounds, m => m * 2.20462262184878, m => m / 2.20462262184878);
            RegisterConversion(MassUnits.Tonne, m => m / 1000, m => m * 1000);

            units = new Lazy<IDictionary<object, string>>(() => new Dictionary<object, string>
            {
                { MassUnits.Milligrams, Resource.Milligrams },
                { MassUnits.Grams, Resource.Grams },
                { MassUnits.Kilograms, Resource.Kilograms },
                { MassUnits.Slugs, Resource.Slugs },
                { MassUnits.Pounds, Resource.Pounds },
                { MassUnits.Tonne, Resource.Tonne }
            });
        }

        /// <summary>
        /// Gets the name of this converter.
        /// </summary>
        /// <value>
        /// The name of this converter.
        /// </value>
        public override string Name => Resource.MassConverterName;

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public override IDictionary<object, string> Units => units.Value;
    }
}