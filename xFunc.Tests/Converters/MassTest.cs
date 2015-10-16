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
    
    public class MassTest
    {

        private MassConverter conv = new MassConverter();

        [Fact]
        public void FromKiloToMilli()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Milligrams);

            Assert.Equal(12000000, value);
        }

        [Fact]
        public void FromMilliToKilo()
        {
            var value = conv.Convert(12000000, MassUnits.Milligrams, MassUnits.Kilograms);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromKiloToG()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Grams);

            Assert.Equal(12000, value);
        }

        [Fact]
        public void FromGToKilo()
        {
            var value = conv.Convert(12000, MassUnits.Grams, MassUnits.Kilograms);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromKiloToSlug()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Slugs);

            Assert.Equal(0.822261186743533, value, 4);
        }

        [Fact]
        public void FromSlugToKilo()
        {
            var value = conv.Convert(0.822261186743533, MassUnits.Slugs, MassUnits.Kilograms);

            Assert.Equal(12, value, 4);
        }

        [Fact]
        public void FromKiloToLb()
        {
            var value = conv.Convert(12, MassUnits.Kilograms, MassUnits.Pounds);

            Assert.Equal(26.4554714621853, value, 4);
        }

        [Fact]
        public void FromLbToKilo()
        {
            var value = conv.Convert(26.4554714621853, MassUnits.Pounds, MassUnits.Kilograms);

            Assert.Equal(12, value, 4);
        }

        [Fact]
        public void FromKiloToTonne()
        {
            var value = conv.Convert(12000, MassUnits.Kilograms, MassUnits.Tonne);

            Assert.Equal(12, value, 4);
        }

        [Fact]
        public void FromTonneToKilo()
        {
            var value = conv.Convert(12, MassUnits.Tonne, MassUnits.Kilograms);

            Assert.Equal(12000, value, 4);
        }

    }

}
