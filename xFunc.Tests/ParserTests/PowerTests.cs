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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class PowerTests : BaseParserTests
    {
        [Theory]
        [InlineData("pow(1, 2)")]
        [InlineData("1 ^ 2")]
        public void PowFuncTest(string function)
        {
            var expected = new Pow(Number.One, Number.Two);

            ParseTest(function, expected);
        }

        [Fact]
        public void PowRightAssociativityTest()
        {
            var expected = new Pow(Number.One, new Pow(Number.Two, new Number(3)));

            ParseTest("1 ^ 2 ^ 3", expected);
        }

        [Fact]
        public void PowUnaryMinusTest()
        {
            var expected = new UnaryMinus(new Pow(Number.One, Number.Two));

            ParseTest("-1 ^ 2", expected);
        }

        [Fact]
        public void PowerWithUnaryMinus()
        {
            var expected = new Pow(Number.Two, new UnaryMinus(Number.Two));

            ParseTest("2 ^ -2", expected);
        }

        [Fact]
        public void ImplicitMulAndPowerFunction()
        {
            var expected = new Mul(
                Number.Two,
                new Pow(new Sin(Variable.X), Number.Two)
            );

            ParseTest("2sin(x) ^ 2", expected);
        }

        [Fact]
        public void ImplicitMulAndPowerVariable()
        {
            var expected = new Mul(
                Number.Two,
                new Pow(Variable.X, Number.Two)
            );

            ParseTest("2x^2", expected);
        }

        [Fact]
        public void ImplicitNegativeNumberMulAndPowerVariable()
        {
            var expected = new Mul(
                new UnaryMinus(Number.Two),
                new Pow(Variable.X, Number.Two)
            );

            ParseTest("-2x^2", expected);
        }

        [Theory]
        [InlineData("2 ^")]
        [InlineData("2x ^")]
        public void PowErrorTest(string exp)
            => ParseErrorTest(exp);
    }
}