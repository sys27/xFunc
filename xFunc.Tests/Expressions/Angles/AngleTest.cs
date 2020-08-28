// Copyright 2012-2020 Dmytro Kyshchenko
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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using Xunit;

namespace xFunc.Tests.Expressions.Angles
{
    public class AngleTest
    {
        [Fact]
        public void EqualTest()
        {
            var angle1 = Angle.Degree(10);
            var angle2 = Angle.Degree(10);

            Assert.True(angle1.Equals(angle2));
        }

        [Fact]
        public void EqualOperatorTest()
        {
            var angle1 = Angle.Degree(10);
            var angle2 = Angle.Degree(10);

            Assert.True(angle1 == angle2);
        }

        [Fact]
        public void NotEqualTest()
        {
            var angle1 = Angle.Degree(10);
            var angle2 = Angle.Degree(12);

            Assert.True(angle1 != angle2);
        }

        [Fact]
        public void ValueNotEqualTest()
        {
            var angle1 = Angle.Degree(10);
            var angle2 = Angle.Degree(12);

            Assert.False(angle1.Equals(angle2));
        }

        [Fact]
        public void UnitNotEqualTest2()
        {
            var angle1 = Angle.Degree(10);
            var angle2 = Angle.Radian(10);

            Assert.False(angle1.Equals(angle2));
        }

        [Fact]
        public void EqualDiffTypeTest()
        {
            var angle1 = Angle.Degree(10);
            var angle2 = 3;

            Assert.False(angle1.Equals(angle2));
        }

        [Fact]
        public void ToStringDegreeTest()
        {
            var angle = Angle.Degree(10);

            Assert.Equal("10 degree", angle.ToString());
        }

        [Fact]
        public void ToStringRadianTest()
        {
            var angle = Angle.Radian(10);

            Assert.Equal("10 radian", angle.ToString());
        }

        [Fact]
        public void ToStringGradianTest()
        {
            var angle = Angle.Gradian(10);

            Assert.Equal("10 gradian", angle.ToString());
        }

        [Fact]
        public void DegreeToDegreeTest()
        {
            var angle = Angle.Degree(10);
            var actual = angle.To(AngleUnit.Degree);
            var expected = Angle.Degree(10);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DegreeToRadianTest()
        {
            var angle = Angle.Degree(10);
            var actual = angle.To(AngleUnit.Radian);
            var expected = Angle.Radian(10 * Math.PI / 180);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DegreeToGradianTest()
        {
            var angle = Angle.Degree(10);
            var actual = angle.To(AngleUnit.Gradian);
            var expected = Angle.Gradian(10 / 0.9);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RadianToDegreeTest()
        {
            var angle = Angle.Radian(10);
            var actual = angle.To(AngleUnit.Degree);
            var expected = Angle.Degree(10 * 180 / Math.PI);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RadianToRadianTest()
        {
            var angle = Angle.Radian(10);
            var actual = angle.To(AngleUnit.Radian);
            var expected = Angle.Radian(10);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RadianToGradianTest()
        {
            var angle = Angle.Radian(10);
            var actual = angle.To(AngleUnit.Gradian);
            var expected = Angle.Gradian(10 * 200 / Math.PI);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GradianToDegreeTest()
        {
            var angle = Angle.Gradian(10);
            var actual = angle.To(AngleUnit.Degree);
            var expected = Angle.Degree(10 * 0.9);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GradianToRadianTest()
        {
            var angle = Angle.Gradian(10);
            var actual = angle.To(AngleUnit.Radian);
            var expected = Angle.Radian(10 * Math.PI / 200);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GradianToGradianTest()
        {
            var angle = Angle.Gradian(10);
            var actual = angle.To(AngleUnit.Gradian);
            var expected = Angle.Gradian(10);

            Assert.Equal(expected, actual);
        }
    }
}