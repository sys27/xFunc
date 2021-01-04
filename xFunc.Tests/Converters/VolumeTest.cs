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

using xFunc.UnitConverters;
using Xunit;

namespace xFunc.Tests.Converters
{
    public class VolumeTest
    {
        private readonly VolumeConverter conv = new VolumeConverter();

        [Fact]
        public void ConvertToSame()
        {
            var value = conv.Convert(0.032, VolumeUnits.CubicMeters, VolumeUnits.CubicMeters);

            Assert.Equal(0.032, value, 4);
        }

        [Fact]
        public void FromMtoCenti()
        {
            var value = conv.Convert(0.032, VolumeUnits.CubicMeters, VolumeUnits.CubicCentimeters);

            Assert.Equal(32000, value, 4);
        }

        [Fact]
        public void FromCentiToM()
        {
            var value = conv.Convert(32000, VolumeUnits.CubicCentimeters, VolumeUnits.CubicMeters);

            Assert.Equal(0.032, value);
        }

        [Fact]
        public void FromMtoLitre()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.Litres);

            Assert.Equal(1500, value, 4);
        }

        [Fact]
        public void FromLitreToM()
        {
            var value = conv.Convert(1500, VolumeUnits.Litres, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value);
        }

        [Fact]
        public void FromMtoInch()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.CubicInches);

            Assert.Equal(91535.61, value, 4);
        }

        [Fact]
        public void FromInchToM()
        {
            var value = conv.Convert(91535.61, VolumeUnits.CubicInches, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value);
        }

        [Fact]
        public void FromMtoPtUS()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.PintsUS);

            Assert.Equal(3170.06462829778, value, 4);
        }

        [Fact]
        public void FromPtUSToM()
        {
            var value = conv.Convert(3170.06462829778, VolumeUnits.PintsUS, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromMtoPtUK()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.PintsUK);

            Assert.Equal(2639.63097958905, value, 4);
        }

        [Fact]
        public void FromPtUKToM()
        {
            var value = conv.Convert(2639.63097958905, VolumeUnits.PintsUK, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromMtoGalUS()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.GallonsUS);

            Assert.Equal(396.258078537223, value, 4);
        }

        [Fact]
        public void FromGalUSToM()
        {
            var value = conv.Convert(396.258078537223, VolumeUnits.GallonsUS, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromMtoGalUK()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.GallonsUK);

            Assert.Equal(329.953872448632, value, 4);
        }

        [Fact]
        public void FromGalUKToM()
        {
            var value = conv.Convert(329.953872448632, VolumeUnits.GallonsUK, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromMtoFoot()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.CubicFeet);

            Assert.Equal(52.9720000822329, value, 4);
        }

        [Fact]
        public void FromFootToM()
        {
            var value = conv.Convert(52.9720000822329, VolumeUnits.CubicFeet, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromMtoYard()
        {
            var value = conv.Convert(1.5, VolumeUnits.CubicMeters, VolumeUnits.CubicYards);

            Assert.Equal(1.96192592897159, value, 4);
        }

        [Fact]
        public void FromYardToM()
        {
            var value = conv.Convert(1.96192592897159, VolumeUnits.CubicYards, VolumeUnits.CubicMeters);

            Assert.Equal(1.5, value, 4);
        }
    }
}