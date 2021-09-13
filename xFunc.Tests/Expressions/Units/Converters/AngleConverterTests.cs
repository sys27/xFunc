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
using Xunit;

namespace xFunc.Tests.Expressions.Units.Converters
{
    public class AngleConverterTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(1, null)]
        public void ConvertNull(object value, string unit)
        {
            var converter = new AngleConverter();

            Assert.Throws<ArgumentNullException>(() => converter.Convert(value, unit));
        }

        public static IEnumerable<object[]> GetConvertTestsData()
        {
            var angle = AngleValue.Degree(90);

            yield return new object[] { angle, "rad", angle.ToRadian() };
            yield return new object[] { angle, "radian", angle.ToRadian() };
            yield return new object[] { angle, "radians", angle.ToRadian() };
            yield return new object[] { angle, "deg", angle.ToDegree() };
            yield return new object[] { angle, "degree", angle.ToDegree() };
            yield return new object[] { angle, "degrees", angle.ToDegree() };
            yield return new object[] { angle, "grad", angle.ToGradian() };
            yield return new object[] { angle, "gradian", angle.ToGradian() };
            yield return new object[] { angle, "gradians", angle.ToGradian() };

            var number = new NumberValue(10);

            yield return new object[] { number, "rad", AngleValue.Radian(number) };
            yield return new object[] { number, "radian", AngleValue.Radian(number) };
            yield return new object[] { number, "radians", AngleValue.Radian(number) };
            yield return new object[] { number, "deg", AngleValue.Degree(number) };
            yield return new object[] { number, "degree", AngleValue.Degree(number) };
            yield return new object[] { number, "degrees", AngleValue.Degree(number) };
            yield return new object[] { number, "grad", AngleValue.Gradian(number) };
            yield return new object[] { number, "gradian", AngleValue.Gradian(number) };
            yield return new object[] { number, "gradians", AngleValue.Gradian(number) };
        }

        [Theory]
        [MemberData(nameof(GetConvertTestsData))]
        public void ConvertTests(object value, string unit, object expected)
        {
            var converter = new AngleConverter();
            var result = converter.Convert(value, unit);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> GetConvertUnsupportedUnitData()
        {
            yield return new object[] { AngleValue.Degree(90), "xxx" };
            yield return new object[] { new NumberValue(10), "xxx" };
        }

        [Theory]
        [MemberData(nameof(GetConvertUnsupportedUnitData))]
        public void ConvertUnsupportedUnit(object value, string unit)
        {
            var converter = new AngleConverter();

            Assert.Throws<UnitIsNotSupportedException>(() => converter.Convert(value, unit));
        }

        [Fact]
        public void ConvertUnsupportedValue()
        {
            var converter = new AngleConverter();

            Assert.Throws<ValueIsNotSupportedException>(() => converter.Convert(1, "deg"));
        }
    }
}