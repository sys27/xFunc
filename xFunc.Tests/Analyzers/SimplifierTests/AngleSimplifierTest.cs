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

namespace xFunc.Tests.Analyzers.SimplifierTests
{
    public class AngleSimplifierTest : BaseSimplifierTest
    {
        [Fact]
        public void ToDegreeNumber()
        {
            var exp = new ToDegree(new Number(10));
            var expected = Angle.Degree(10).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void DegreeToDegree()
        {
            var exp = new ToDegree(Angle.Degree(10).AsExpression());
            var expected = Angle.Degree(10).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void RadianToDegree()
        {
            var exp = new ToDegree(Angle.Radian(Math.PI).AsExpression());
            var expected = Angle.Degree(180).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ToRadianNumber()
        {
            var exp = new ToRadian(new Number(10));
            var expected = Angle.Radian(10).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void RadianToRadian()
        {
            var exp = new ToRadian(Angle.Radian(10).AsExpression());
            var expected = Angle.Radian(10).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void DegreeToRadian()
        {
            var exp = new ToRadian(Angle.Degree(180).AsExpression());
            var expected = Angle.Radian(Math.PI).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ToGradianNumber()
        {
            var exp = new ToGradian(new Number(10));
            var expected = Angle.Gradian(10).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void GradianToGradian()
        {
            var exp = new ToGradian(Angle.Gradian(10).AsExpression());
            var expected = Angle.Gradian(10).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void DegreeToGradian()
        {
            var exp = new ToGradian(Angle.Degree(180).AsExpression());
            var expected = Angle.Gradian(200).AsExpression();

            SimpleTest(exp, expected);
        }

        [Fact]
        public void ToNumberTest()
        {
            var exp = new ToNumber(Angle.Degree(10).AsExpression());
            var expected = new Number(10);

            SimpleTest(exp, expected);
        }
    }
}