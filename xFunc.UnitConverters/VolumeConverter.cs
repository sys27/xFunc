// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using xFunc.UnitConverters.Resources;

namespace xFunc.UnitConverters
{
    /// <summary>
    /// Represents the volume converter.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VolumeConverter : Converter<VolumeUnits>
    {
        private static readonly Lazy<IDictionary<object, string>> units;

        static VolumeConverter()
        {
            BaseUnit = VolumeUnits.CubicMeters;

            RegisterConversion(VolumeUnits.CubicCentimeters, v => v / 0.000001, v => v * 0.000001);
            RegisterConversion(VolumeUnits.Litres, v => v * 1000, v => v / 1000);
            RegisterConversion(VolumeUnits.CubicInches, v => v * 61023.74, v => v / 61023.74);
            RegisterConversion(VolumeUnits.PintsUS, v => v * 2113.37641886519, v => v / 2113.37641886519);
            RegisterConversion(VolumeUnits.PintsUK, v => v * 1759.7539863927, v => v / 1759.7539863927);
            RegisterConversion(VolumeUnits.GallonsUS, v => v * 264.172052358148, v => v / 264.172052358148);
            RegisterConversion(VolumeUnits.GallonsUK, v => v * 219.969248299088, v => v / 219.969248299088);
            RegisterConversion(VolumeUnits.CubicFeet, v => v * 35.3146667214886, v => v / 35.3146667214886);
            RegisterConversion(VolumeUnits.CubicYards, v => v * 1.30795061931439, v => v / 1.30795061931439);

            units = new Lazy<IDictionary<object, string>>(() => new Dictionary<object, string>
            {
                { VolumeUnits.CubicCentimeters, Resource.CubicCentimeters },
                { VolumeUnits.CubicMeters, Resource.CubicMeters },
                { VolumeUnits.Litres, Resource.Litres },
                { VolumeUnits.CubicInches, Resource.CubicInches },
                { VolumeUnits.PintsUS, Resource.PintsUS },
                { VolumeUnits.PintsUK, Resource.PintsUK },
                { VolumeUnits.GallonsUS, Resource.GallonsUS },
                { VolumeUnits.GallonsUK, Resource.GallonsUK },
                { VolumeUnits.CubicFeet, Resource.CubicFeet },
                { VolumeUnits.CubicYards, Resource.CubicYards }
            });
        }

        /// <summary>
        /// Gets the name of this converter.
        /// </summary>
        /// <value>
        /// The name of this converter.
        /// </value>
        public override string Name => Resource.VolumeConverterName;

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public override IDictionary<object, string> Units => units.Value;
    }
}