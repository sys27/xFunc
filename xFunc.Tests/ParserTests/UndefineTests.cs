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
    public class UndefineTests : BaseParserTests
    {
        [Fact]
        public void UndefParseTest()
        {
            var tokens = Builder()
                .Undef()
                .OpenParenthesis()
                .Id("f")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Undefine(new UserFunction("f", new IExpression[] { Variable.X }));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UndefMissingOpenParen()
        {
            var tokens = Builder()
                .Undef()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void UndefMissingKey()
        {
            var tokens = Builder()
                .Undef()
                .OpenParenthesis()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void UndefMissingCloseParen()
        {
            var tokens = Builder()
                .Undef()
                .OpenParenthesis()
                .VariableX()
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}