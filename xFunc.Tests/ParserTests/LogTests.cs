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
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class LogTests : BaseParserTests
    {
        [Fact]
        public void ParseLog()
        {
            var tokens = Builder()
                .Id("log")
                .OpenParenthesis()
                .Number(9)
                .Comma()
                .Number(3)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Log(new Number(9), new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseLogWithOneParam()
        {
            var tokens = Builder()
                .Id("log")
                .OpenParenthesis()
                .Number(9)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void Log2Test()
        {
            var tokens = Builder()
                .Id("lb")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lb(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LbTest()
        {
            var tokens = Builder()
                .Id("log2")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lb(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LgTest()
        {
            var tokens = Builder()
                .Id("lg")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Lg(new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void LnTest()
        {
            var tokens = Builder()
                .Id("ln")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Ln(new Number(2));

            Assert.Equal(expected, exp);
        }
    }
}