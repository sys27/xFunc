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
using xFunc.Maths.Expressions.Hyperbolic;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class HyperbolicTests : BaseParserTests
    {
        [Fact]
        public void SinhTest()
        {
            var tokens = Builder()
                .Id("sinh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ShTest()
        {
            var tokens = Builder()
                .Id("sh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sinh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CoshTest()
        {
            var tokens = Builder()
                .Id("cosh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ChTest()
        {
            var tokens = Builder()
                .Id("ch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Cosh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TanhTest()
        {
            var tokens = Builder()
                .Id("tanh")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ThTest()
        {
            var tokens = Builder()
                .Id("th")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Tanh(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CothTest()
        {
            var tokens = Builder()
                .Id("coth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Coth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CthTest()
        {
            var tokens = Builder()
                .Id("cth")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Coth(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SechTest()
        {
            var tokens = Builder()
                .Id("sech")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sech(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void CschTest()
        {
            var tokens = Builder()
                .Id("csch")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Csch(new Number(2));

            Assert.Equal(expected, exp);
        }
    }
}