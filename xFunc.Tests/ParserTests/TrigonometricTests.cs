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
    public class TrigonometricTests : BaseParserTests
    {
        [Fact]
        public void SinTest()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sin(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CosTest()
        {
            var tokens = Builder()
                .Id("cos")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cos(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanTest()
        {
            var tokens = Builder()
                .Id("tan")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tan(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TgTest()
        {
            var tokens = Builder()
                .Id("tg")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tan(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CotTest()
        {
            var tokens = Builder()
                .Id("cot")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cot(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CtgTest()
        {
            var tokens = Builder()
                .Id("ctg")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cot(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SecTest()
        {
            var tokens = Builder()
                .Id("sec")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sec(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CscTest()
        {
            var tokens = Builder()
                .Id("csc")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csc(Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CosecTest()
        {
            var tokens = Builder()
                .Id("cosec")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csc(Number.Two);

            Assert.Equal(expected, exp);
        }
    }
}