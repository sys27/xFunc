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

using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class AngleTests : BaseParserTests
    {
        [Fact]
        public void AngleDegree()
        {
            var tokens = Builder()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = AngleValue.Degree(1).AsExpression();

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AngleDegreeSymbol()
        {
            var tokens = Builder()
                .Number(1)
                .Symbol(SymbolToken.Degree)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = AngleValue.Degree(1).AsExpression();

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AngleRadian()
        {
            var tokens = Builder()
                .Number(1)
                .Keyword(KeywordToken.Radian)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = AngleValue.Radian(1).AsExpression();

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AngleGradian()
        {
            var tokens = Builder()
                .Number(1)
                .Keyword(KeywordToken.Gradian)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = AngleValue.Gradian(1).AsExpression();

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToDegTest()
        {
            var tokens = Builder()
                .Id("todeg")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToDegree(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToDegreeTest()
        {
            var tokens = Builder()
                .Id("todegree")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToDegree(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToRadTest()
        {
            var tokens = Builder()
                .Id("torad")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToRadian(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToRadianTest()
        {
            var tokens = Builder()
                .Id("toradian")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToRadian(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToGradTest()
        {
            var tokens = Builder()
                .Id("tograd")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToGradian(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToGradianTest()
        {
            var tokens = Builder()
                .Id("togradian")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToGradian(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ToNumberTest()
        {
            var tokens = Builder()
                .Id("tonumber")
                .OpenParenthesis()
                .Number(1)
                .Keyword(KeywordToken.Degree)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new ToNumber(AngleValue.Degree(1).AsExpression());

            Assert.Equal(expected, exp);
        }
    }
}