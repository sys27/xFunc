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
    public class ForTests : BaseParserTests
    {
        [Fact]
        public void ForTest()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new For(
                Number.Two,
                new Define(Variable.X, Number.Zero),
                new LessThan(Variable.X, new Number(10)),
                new Define(Variable.X, new Add(Variable.X, Number.One)));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IncForTest()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new For(
                Number.Two,
                new Define(Variable.X, Number.Zero),
                new LessThan(Variable.X, new Number(10)),
                new Inc(Variable.X));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ForMissingOpenParen()
        {
            var tokens = Builder()
                .For()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingBody()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingBodyComma()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingInit()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingInitComma()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Id("z")
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingCondition()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingConditionComma()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .VariableX()
                .Operation(OperatorToken.Increment)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingIter()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ForMissingCloseParen()
        {
            var tokens = Builder()
                .For()
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(0)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.LessThan)
                .Number(10)
                .Comma()
                .VariableX()
                .Operation(OperatorToken.Increment)
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}