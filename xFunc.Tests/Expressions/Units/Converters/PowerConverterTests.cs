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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.Converters;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions.Units.Converters
{
    public class PowerConverterTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        public void ConvertNull(object value, string unit)
        {
            var converter = new PowerConverter();

            Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
        }

        public static IEnumerable<object[]> GetConvertTestsData()
        {
            var power = PowerValue.Watt(10);

            yield return new object[] { power, "w", power.ToWatt() };
            yield return new object[] { power, "kw", power.ToKilowatt() };
            yield return new object[] { power, "hp", power.ToHorsepower() };

            var number = new NumberValue(10);

            yield return new object[] { number, "w", PowerValue.Watt(number) };
            yield return new object[] { number, "kw", PowerValue.Kilowatt(number) };
            yield return new object[] { number, "hp", PowerValue.Horsepower(number) };
        }

        [Theory]
        [MemberData(nameof(GetConvertTestsData))]
        public void ConvertTests(object value, string unit, object expected)
        {
            var converter = new PowerConverter();
            var result = converter.Convert(value, unit);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
        {
            yield return new object[] { PowerValue.Watt(10), "xxx" };
            yield return new object[] { new NumberValue(10), "xxx" };
        }

        [Theory]
        [MemberData(nameof(GetConvertUnsupportedUnitData))]
        public void ConvertUnsupportedUnit(object value, string unit)
        {
            var converter = new PowerConverter();

            Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
        }

        [Fact]
        public void ConvertUnsupportedValue()
        {
            var converter = new PowerConverter();

            Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "hp"));
        }
    }
}