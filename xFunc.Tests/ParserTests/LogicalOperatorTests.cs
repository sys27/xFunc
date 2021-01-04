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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class LogicalOperatorTests : BaseParserTests
    {
        [Theory]
        [InlineData("~2")]
        [InlineData("not(2)")]
        public void NotTest(string function)
            => ParseTest(function, new Not(Number.Two));

        [Theory]
        [InlineData("true & false")]
        [InlineData("true and false")]
        public void BoolConstTest(string function)
            => ParseTest(function, new And(Bool.True, Bool.False));

        [Fact]
        public void LogicAddPriorityTest()
        {
            var expected = new And(
                new GreaterThan(new Number(3), new Number(4)),
                new LessThan(Number.One, new Number(3))
            );

            ParseTest("3 > 4 & 1 < 3", expected);
        }

        [Theory]
        [InlineData("1 | 2")]
        [InlineData("1 or 2")]
        public void OrTest(string function)
            => ParseTest(function, new Or(Number.One, Number.Two));

        [Fact]
        public void XOrTest()
            => ParseTest("1 xor 2", new XOr(Number.One, Number.Two));

        [Fact]
        public void NOrTest()
            => ParseTest("true nor true", new NOr(Bool.True, Bool.True));

        [Fact]
        public void NAndTest()
            => ParseTest("true nand true", new NAnd(Bool.True, Bool.True));

        [Theory]
        [InlineData("true -> true")]
        [InlineData("true −> true")]
        [InlineData("true => true")]
        [InlineData("true impl true")]
        public void ImplicationTest(string function)
            => ParseTest(function, new Implication(Bool.True, Bool.True));

        [Theory]
        [InlineData("true <-> true")]
        [InlineData("true <−> true")]
        [InlineData("true <=> true")]
        [InlineData("true eq true")]
        public void EqualityTest(string function)
            => ParseTest(function, new Equality(Bool.True, Bool.True));

        [Fact]
        public void AndXOrOrPrecedenceTest()
        {
            var expected = new Or(
                new Variable("a"),
                new XOr(
                    new Variable("b"),
                    new And(
                        new Variable("c"),
                        new Variable("d")
                    )
                )
            );

            ParseTest("a | b xor c & d", expected);
        }
    }
}