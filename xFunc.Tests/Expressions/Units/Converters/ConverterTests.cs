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
using xFunc.Maths.Expressions.Units.AngleUnits;
using xFunc.Maths.Expressions.Units.Converters;
using xFunc.Maths.Expressions.Units.PowerUnits;
using Xunit;

namespace xFunc.Tests.Expressions.Units.Converters
{
    public class ConverterTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        public void ConvertNull(object value, string unit)
        {
            var converter = new Converter();

            Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
        }

        public static IEnumerable<object[]> GetConvertTestsData()
        {
            var angle = AngleValue.Radian(90);

            yield return new object[] { angle, "rad", angle.ToRadian() };

            var power = PowerValue.Watt(10);

            yield return new object[] { power, "w", power.ToWatt() };
        }

        [Theory]
        [MemberData(nameof(GetConvertTestsData))]
        public void ConvertTests(object value, string unit, object expected)
        {
            var converter = new Converter();
            var result = converter.Convert(value, unit);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertUnsupportedUnit()
        {
            var converter = new Converter();

            Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(new NumberValue(10), "xxx"));
        }
    }
}