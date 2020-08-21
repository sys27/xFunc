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
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class WhileTests : BaseParserTests
    {
        [Fact]
        public void WhileTest()
        {
            var tokens = Builder()
                .While()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(1)
                .Comma()
                .Number(1)
                .Operation(OperatorToken.Equal)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new While(
                new Define(Variable.X, new Add(Variable.X, new Number(1))),
                new Equal(new Number(1), new Number(1)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void WhileMissingOpenParen()
        {
            var tokens = Builder()
                .While()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(1)
                .Comma()
                .Number(1)
                .Operation(OperatorToken.Equal)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void WhileMissingBody()
        {
            var tokens = Builder()
                .While()
                .OpenParenthesis()
                .Comma()
                .Number(1)
                .Operation(OperatorToken.Equal)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void WhileMissingBodyComma()
        {
            var tokens = Builder()
                .While()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(1)
                .Number(1)
                .Operation(OperatorToken.Equal)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void WhileMissingCondition()
        {
            var tokens = Builder()
                .While()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(1)
                .Comma()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void WhileMissingCloseParen()
        {
            var tokens = Builder()
                .While()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(1)
                .Comma()
                .Number(1)
                .Operation(OperatorToken.Equal)
                .Number(1)
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}