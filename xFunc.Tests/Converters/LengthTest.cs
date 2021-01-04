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
    public class LengthTest
    {
        private readonly LengthConverter conv = new LengthConverter();

        [Fact]
        public void ConvertToSame()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Metres);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromMtoNano()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Nanometres);

            Assert.Equal(12000000000, value);
        }

        [Fact]
        public void FromNanotoM()
        {
            var value = conv.Convert(12000000000, LengthUnits.Nanometres, LengthUnits.Metres);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromMtoMicro()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Micrometers);

            Assert.Equal(12000000, value);
        }

        [Fact]
        public void FromMicroToM()
        {
            var value = conv.Convert(12000000, LengthUnits.Micrometers, LengthUnits.Metres);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromMtoMilli()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Millimeters);

            Assert.Equal(12000, value);
        }

        [Fact]
        public void FromMilliToM()
        {
            var value = conv.Convert(12000, LengthUnits.Millimeters, LengthUnits.Metres);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromMtoCenti()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Centimeters);

            Assert.Equal(1200, value);
        }

        [Fact]
        public void FromCentiToM()
        {
            var value = conv.Convert(1200, LengthUnits.Centimeters, LengthUnits.Metres);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromMtoKilo()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Kilometers);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromKiloToM()
        {
            var value = conv.Convert(12, LengthUnits.Kilometers, LengthUnits.Metres);

            Assert.Equal(12000, value);
        }

        [Fact]
        public void FromMtoInch()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Inches);

            Assert.Equal(472.44094488189, value, 4);
        }

        [Fact]
        public void FromInchToM()
        {
            var value = conv.Convert(472.44094488189, LengthUnits.Inches, LengthUnits.Metres);

            Assert.Equal(12, value, 4);
        }

        [Fact]
        public void FromMtoFoot()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Feet);

            Assert.Equal(39.3700787401575, value, 4);
        }

        [Fact]
        public void FromFootToM()
        {
            var value = conv.Convert(39.3700787401575, LengthUnits.Feet, LengthUnits.Metres);

            Assert.Equal(12, value, 4);
        }

        [Fact]
        public void FromMtoYard()
        {
            var value = conv.Convert(12, LengthUnits.Metres, LengthUnits.Yards);

            Assert.Equal(13.1233595800525, value, 4);
        }

        [Fact]
        public void FromYardToM()
        {
            var value = conv.Convert(13.1233595800525, LengthUnits.Yards, LengthUnits.Metres);

            Assert.Equal(12, value, 4);
        }

        [Fact]
        public void FromMtoMile()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Miles);

            Assert.Equal(7.45645430684801, value, 4);
        }

        [Fact]
        public void FromMileToM()
        {
            var value = conv.Convert(7.45645430684801, LengthUnits.Miles, LengthUnits.Metres);

            Assert.Equal(12000, value, 4);
        }

        [Fact]
        public void FromMtoNauticalMile()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.NauticalMiles);

            Assert.Equal(6.47948164146868, value, 4);
        }

        [Fact]
        public void FromNauticalMileToM()
        {
            var value = conv.Convert(6.47948164146868, LengthUnits.NauticalMiles, LengthUnits.Metres);

            Assert.Equal(12000, value, 4);
        }

        [Fact]
        public void FromMtoFathom()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Fathoms);

            Assert.Equal(6561.67979002625, value, 4);
        }

        [Fact]
        public void FromFathomToM()
        {
            var value = conv.Convert(6561.67979002625, LengthUnits.Fathoms, LengthUnits.Metres);

            Assert.Equal(12000, value, 4);
        }

        [Fact]
        public void FromMtoChain()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Chains);

            Assert.Equal(596.516344547841, value, 4);
        }

        [Fact]
        public void FromChainToM()
        {
            var value = conv.Convert(596.516344547841, LengthUnits.Chains, LengthUnits.Metres);

            Assert.Equal(12000, value, 4);
        }

        [Fact]
        public void FromMtoRod()
        {
            var value = conv.Convert(12000, LengthUnits.Metres, LengthUnits.Rods);

            Assert.Equal(2386.065384, value, 4);
        }

        [Fact]
        public void FromRodToM()
        {
            var value = conv.Convert(2386.065384, LengthUnits.Rods, LengthUnits.Metres);

            Assert.Equal(12000, value, 4);
        }

        [Fact]
        public void FromMtoAu()
        {
            var value = conv.Convert(224396806050, LengthUnits.Metres, LengthUnits.AstronomicalUnits);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromAuToM()
        {
            var value = conv.Convert(1.5, LengthUnits.AstronomicalUnits, LengthUnits.Metres);

            Assert.Equal(224396806050, value, 4);
        }

        [Fact]
        public void FromMtoLY()
        {
            var value = conv.Convert(14190792600000000, LengthUnits.Metres, LengthUnits.LightYears);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromLYToM()
        {
            var value = conv.Convert(1.5, LengthUnits.LightYears, LengthUnits.Metres);

            Assert.Equal(14190792600000000, value, 4);
        }

        [Fact]
        public void FromMtoP()
        {
            var value = conv.Convert(46285163700000000, LengthUnits.Metres, LengthUnits.Parsecs);

            Assert.Equal(1.5, value, 4);
        }

        [Fact]
        public void FromPToM()
        {
            var value = conv.Convert(1.5, LengthUnits.Parsecs, LengthUnits.Metres);

            Assert.Equal(46285163700000000, value, 4);
        }
    }
}