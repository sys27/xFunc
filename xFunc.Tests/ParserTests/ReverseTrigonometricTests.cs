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
    public class ReverseTrigonometricTests : BaseParserTests
    {
        [Fact]
        public void ArcsinTest()
        {
            var tokens = Builder()
                .Id("arcsin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsin(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccosTest()
        {
            var tokens = Builder()
                .Id("arccos")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccos(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArctanTest()
        {
            var tokens = Builder()
                .Id("arctan")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arctan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArctgTest()
        {
            var tokens = Builder()
                .Id("arctg")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arctan(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccotTest()
        {
            var tokens = Builder()
                .Id("arccot")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcctgTest()
        {
            var tokens = Builder()
                .Id("arcctg")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccot(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArcsecTest()
        {
            var tokens = Builder()
                .Id("arcsec")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arcsec(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccscTest()
        {
            var tokens = Builder()
                .Id("arccsc")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccsc(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ArccosecTest()
        {
            var tokens = Builder()
                .Id("arccosec")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Arccsc(new Number(2));

            Assert.Equal(expected, exp);
        }
    }
}