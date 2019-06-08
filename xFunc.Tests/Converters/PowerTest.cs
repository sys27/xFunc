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
using xFunc.UnitConverters;
using Xunit;

namespace xFunc.Tests.Converters
{

    public class PowerTest
    {

        private PowerConverter conv = new PowerConverter();

        [Fact]
        public void ConvertToSame()
        {
            var value = conv.Convert(12, PowerUnits.Watts, PowerUnits.Watts);

            Assert.Equal(12, value);
        }

        [Fact]
        public void FromWtoK()
        {
            var value = conv.Convert(14000, PowerUnits.Watts, PowerUnits.Kilowatts);

            Assert.Equal(14, value);
        }

        [Fact]
        public void FromKtoW()
        {
            var value = conv.Convert(14, PowerUnits.Kilowatts, PowerUnits.Watts);

            Assert.Equal(14000, value);
        }

        [Fact]
        public void FromWtoHP()
        {
            var value = conv.Convert(14000, PowerUnits.Watts, PowerUnits.Horsepower);

            Assert.Equal(18.7743092543304, value, 4);
        }

        [Fact]
        public void FromHPtoW()
        {
            var value = conv.Convert(18.7743092543304, PowerUnits.Horsepower, PowerUnits.Watts);

            Assert.Equal(14000, value, 4);
        }

    }

}
