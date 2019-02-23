// Copyright 2012-2019 Dmitry Kischenko
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
    
    public class TemperatureTest
    {

        private TemperatureConverter conv = new TemperatureConverter();

        [Fact]
        public void ConvertToSame()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromCToF()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Fahrenheit);

            Assert.Equal(53.6, value);
        }

        [Fact]
        public void FromFToC()
        {
            var value = conv.Convert(53.6, TemperatureUnits.Fahrenheit, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromCToK()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Kelvin);

            Assert.Equal(285.15, value);
        }

        [Fact]
        public void FromKToC()
        {
            var value = conv.Convert(285.15, TemperatureUnits.Kelvin, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromCToR()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Rankine);

            Assert.Equal(513.27, value);
        }

        [Fact]
        public void FromRToC()
        {
            var value = conv.Convert(513.27, TemperatureUnits.Rankine, TemperatureUnits.Celsius);

            Assert.Equal(12.0, value, 4);
        }

        [Fact]
        public void FromCToDe()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Delisle);

            Assert.Equal(132, value);
        }

        [Fact]
        public void FromDeToC()
        {
            var value = conv.Convert(132, TemperatureUnits.Delisle, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromCToN()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Newton);

            Assert.Equal(3.96, value);
        }

        [Fact]
        public void FromNToC()
        {
            var value = conv.Convert(3.96, TemperatureUnits.Newton, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromCToRe()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Réaumur);

            Assert.Equal(9.6, value, 4);
        }

        [Fact]
        public void FromReToC()
        {
            var value = conv.Convert(9.6, TemperatureUnits.Réaumur, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromCToRo()
        {
            var value = conv.Convert(12, TemperatureUnits.Celsius, TemperatureUnits.Rømer);

            Assert.Equal(13.8, value);
        }

        [Fact]
        public void FromRoToC()
        {
            var value = conv.Convert(13.8, TemperatureUnits.Rømer, TemperatureUnits.Celsius);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromFToDe()
        {
            var value = conv.Convert(-146.8, TemperatureUnits.Fahrenheit, TemperatureUnits.Delisle);

            Assert.Equal(299, value);
        }

        [Fact]
        public void FromDeToF()
        {
            var value = conv.Convert(299, TemperatureUnits.Delisle, TemperatureUnits.Fahrenheit);

            Assert.Equal(-146.8, value, 4);
        }

    }

}
