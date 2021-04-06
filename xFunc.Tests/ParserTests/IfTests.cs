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
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class IfTests : BaseParserTests
    {
        [Fact]
        public void IfThenElseTest()
        {
            var expected = new If(
                new ConditionalAnd(
                    new Equal(Variable.X, Number.Zero),
                    new NotEqual(Variable.Y, Number.Zero)),
                Number.Two,
                new Number(8)
            );

            ParseTest("if(x == 0 && y != 0, 2, 8)", expected);
        }

        [Fact]
        public void IfThenElseAsExpressionTest()
        {
            var expected = new Add(
                Number.One,
                new If(
                    new Equal(Variable.X, Number.Zero),
                    Number.Two,
                    new Number(8)
                )
            );

            ParseTest("1 + if(x == 0, 2, 8)", expected);
        }

        [Fact]
        public void IfThenTest()
        {
            var expected = new If(
                new ConditionalAnd(
                    new Equal(Variable.X, Number.Zero),
                    new NotEqual(Variable.Y, Number.Zero)
                ),
                Number.Two
            );

            ParseTest("if(x == 0 && y != 0, 2)", expected);
        }

        [Theory]
        [InlineData("if x == 0 && y != 0, 2, 8)")]
        [InlineData("if(, 2, 8)")]
        [InlineData("if(x == 0 && y != 0 2, 8)")]
        [InlineData("if(x == 0 && y != 0, , 8)")]
        [InlineData("if(x == 0 && y != 0, 2 8)")]
        [InlineData("if(x == 0 && y != 0, 2, )")]
        [InlineData("if(x == 0 && y != 0, 2, 8")]
        public void IfMissingPartsTest(string function)
            => ParseErrorTest(function);

        [Fact]
        public void TernaryTest()
        {
            var expected = new If(
                Bool.True,
                Number.One,
                new UnaryMinus(Number.One)
            );

            ParseTest("true ? 1 : -1", expected);
        }

        [Theory]
        [InlineData("true ? 1 :")]
        [InlineData("true ? : -1")]
        [InlineData("true ? 1")]
        public void TernaryMissingTest(string function)
            => ParseErrorTest(function);

        [Fact]
        public void TernaryAsExpressionTest()
        {
            var expected = new Sin(
                new If(
                    Bool.True,
                    Number.One,
                    new UnaryMinus(Number.One)
                )
            );

            ParseTest("sin(true ? 1 : -1)", expected);
        }
    }
}