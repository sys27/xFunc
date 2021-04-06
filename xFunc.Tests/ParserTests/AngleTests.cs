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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Units;
using xFunc.Maths.Expressions.Units.AngleUnits;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class AngleTests : BaseParserTests
    {
        [Theory]
        [InlineData("1 deg")]
        [InlineData("1 degree")]
        [InlineData("1 degrees")]
        [InlineData("1°")]
        public void AngleDeg(string function)
            => ParseTest(function, AngleValue.Degree(1).AsExpression());

        [Theory]
        [InlineData("1 rad")]
        [InlineData("1 radian")]
        [InlineData("1 radians")]
        public void AngleRad(string function)
            => ParseTest(function, AngleValue.Radian(1).AsExpression());

        [Theory]
        [InlineData("1 grad")]
        [InlineData("1 gradian")]
        [InlineData("1 gradians")]
        public void AngleGrad(string function)
            => ParseTest(function, AngleValue.Gradian(1).AsExpression());

        [Theory]
        [InlineData("todeg(1 deg)")]
        [InlineData("todegree(1 deg)")]
        public void ToDegTest(string function)
            => ParseTest(function, new ToDegree(AngleValue.Degree(1).AsExpression()));

        [Theory]
        [InlineData("torad(1 deg)")]
        [InlineData("toradian(1 deg)")]
        public void ToRadTest(string function)
            => ParseTest(function, new ToRadian(AngleValue.Degree(1).AsExpression()));

        [Theory]
        [InlineData("tograd(1 deg)")]
        [InlineData("togradian(1 deg)")]
        public void ToGradTest(string function)
            => ParseTest(function, new ToGradian(AngleValue.Degree(1).AsExpression()));

        [Fact]
        public void ToNumberTest()
            => ParseTest("tonumber(1 deg)", new ToNumber(AngleValue.Degree(1).AsExpression()));
    }
}