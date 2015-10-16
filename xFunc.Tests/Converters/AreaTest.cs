// Copyright 2012-2015 Dmitry Kischenko
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
using xFunc.UnitConverters;
using Xunit;

namespace xFunc.Tests.Converters
{
    
    public class AreaTest
    {

        private AreaConverter conv = new AreaConverter();

        [Fact]
        public void FromMToMilli()
        {
            var value = conv.Convert(1, AreaUnits.SquareMetres, AreaUnits.SquareMillimeters);

            Assert.Equal(1000000, value);
        }

        [Fact]
        public void FromMilliToM()
        {
            var value = conv.Convert(1000000, AreaUnits.SquareMillimeters, AreaUnits.SquareMetres);

            Assert.Equal(1, value);
        }

        [Fact]
        public void FromMToCenti()
        {
            var value = conv.Convert(1, AreaUnits.SquareMetres, AreaUnits.SquareCentimeters);

            Assert.Equal(10000, value);
        }

        [Fact]
        public void FromCentiToM()
        {
            var value = conv.Convert(10000, AreaUnits.SquareCentimeters, AreaUnits.SquareMetres);

            Assert.Equal(1, value);
        }

        [Fact]
        public void FromMToKilo()
        {
            var value = conv.Convert(140000, AreaUnits.SquareMetres, AreaUnits.SquareKilometers);

            Assert.Equal(0.14, value);
        }

        [Fact]
        public void FromKiloToM()
        {
            var value = conv.Convert(0.14, AreaUnits.SquareKilometers, AreaUnits.SquareMetres);

            Assert.Equal(140000, value);
        }

        [Fact]
        public void FromMToHa()
        {
            var value = conv.Convert(10000, AreaUnits.SquareMetres, AreaUnits.Hectares);

            Assert.Equal(1, value);
        }

        [Fact]
        public void FromHaToM()
        {
            var value = conv.Convert(1, AreaUnits.Hectares, AreaUnits.SquareMetres);

            Assert.Equal(10000, value);
        }

        [Fact]
        public void FromMToInch()
        {
            var value = conv.Convert(15, AreaUnits.SquareMetres, AreaUnits.SquareInches);

            Assert.Equal(23250.0465, value, 4);
        }

        [Fact]
        public void FromInchToM()
        {
            var value = conv.Convert(23250.0465, AreaUnits.SquareInches, AreaUnits.SquareMetres);

            Assert.Equal(15, value, 4);
        }

        [Fact]
        public void FromMToFoot()
        {
            var value = conv.Convert(15, AreaUnits.SquareMetres, AreaUnits.SquareFeet);

            Assert.Equal(161.458665, value);
        }

        [Fact]
        public void FromFootToM()
        {
            var value = conv.Convert(161.458665, AreaUnits.SquareFeet, AreaUnits.SquareMetres);

            Assert.Equal(15, value);
        }

        [Fact]
        public void FromMToYard()
        {
            var value = conv.Convert(15, AreaUnits.SquareMetres, AreaUnits.SquareYards);

            Assert.Equal(17.93985, value);
        }

        [Fact]
        public void FromYardToM()
        {
            var value = conv.Convert(17.93985, AreaUnits.SquareYards, AreaUnits.SquareMetres);

            Assert.Equal(15, value, 4);
        }

        [Fact]
        public void FromMToAcre()
        {
            var value = conv.Convert(15000, AreaUnits.SquareMetres, AreaUnits.Acres);

            Assert.Equal(3.706580715, value, 4);
        }

        [Fact]
        public void FromAcreToM()
        {
            var value = conv.Convert(3.706580715, AreaUnits.Acres, AreaUnits.SquareMetres);

            Assert.Equal(15000, value, 4);
        }

        [Fact]
        public void FromMToMi()
        {
            var value = conv.Convert(15000, AreaUnits.SquareMetres, AreaUnits.SquareMiles);

            Assert.Equal(38849821655.04, value);
        }

        [Fact]
        public void FromMiToM()
        {
            var value = conv.Convert(38849821655.04, AreaUnits.SquareMiles, AreaUnits.SquareMetres);

            Assert.Equal(15000, value);
        }

    }

}
