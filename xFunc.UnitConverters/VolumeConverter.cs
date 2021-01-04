// Copyright 2012-2021 Dmytro Kyshchenko
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