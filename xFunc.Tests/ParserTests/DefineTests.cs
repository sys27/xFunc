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
    public class DefineTests : BaseParserTests
    {
        [Fact]
        public void DefTest()
        {
            var tokens = Builder()
                .Def()
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(new Variable("x"), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DefMissingOpenParen()
        {
            var tokens = Builder()
                .Def()
                .VariableX()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void DefMissingKey()
        {
            var tokens = Builder()
                .Def()
                .OpenParenthesis()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void DefMissingComma()
        {
            var tokens = Builder()
                .Def()
                .OpenParenthesis()
                .VariableX()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void DefMissingValue()
        {
            var tokens = Builder()
                .Def()
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void DefMissingCloseParen()
        {
            var tokens = Builder()
                .Def()
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}