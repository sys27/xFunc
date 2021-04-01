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

using xFunc.Maths.Expressions.Units.PowerUnits;
using xFunc.Maths.Results;
using Xunit;

namespace xFunc.Tests.Results
{
    public class PowerNumberResultTest
    {
        [Fact]
        public void ResultTest()
        {
            var power = PowerValue.Watt(10);
            var result = new PowerNumberResult(power);

            Assert.Equal(power, result.Result);
        }

        [Fact]
        public void IResultTest()
        {
            var power = PowerValue.Watt(10);
            var result = new PowerNumberResult(power) as IResult;

            Assert.Equal(power, result.Result);
        }

        [Fact]
        public void ToStringTest()
        {
            var power = PowerValue.Watt(10);
            var result = new PowerNumberResult(power);

            Assert.Equal("10 W", result.ToString());
        }
    }
}