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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class IfTests : BaseParserTests
    {
        [Fact]
        public void IfThenElseTest()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Operation(OperatorToken.ConditionalAnd)
                .VariableY()
                .Operation(OperatorToken.NotEqual)
                .Number(0)
                .Comma()
                .Number(2)
                .Comma()
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new If(
                new ConditionalAnd(
                    new Equal(Variable.X, Number.Zero),
                    new NotEqual(new Variable("y"), Number.Zero)),
                Number.Two,
                new Number(8));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IfThenTest()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Operation(OperatorToken.ConditionalAnd)
                .VariableY()
                .Operation(OperatorToken.NotEqual)
                .Number(0)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new If(
                new ConditionalAnd(
                    new Equal(Variable.X, Number.Zero),
                    new NotEqual(new Variable("y"), Number.Zero)),
                Number.Two);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void IfMissingOpenParen()
        {
            var tokens = Builder()
                .If()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Comma()
                .Number(2)
                .Comma()
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void IfMissingCondition()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .Comma()
                .Number(2)
                .Comma()
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void IfMissingConditionComma()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Number(2)
                .Comma()
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void IfMissingThen()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Comma()
                .Comma()
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void IfMissingThenComma()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Comma()
                .Number(2)
                .Number(8)
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void IfMissingElse()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Comma()
                .Number(2)
                .Comma()
                .CloseParenthesis()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void IfMissingClose()
        {
            var tokens = Builder()
                .If()
                .OpenParenthesis()
                .VariableX()
                .Operation(OperatorToken.Equal)
                .Number(0)
                .Comma()
                .Number(2)
                .Comma()
                .Number(8)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void TernaryTest()
        {
            var tokens = Builder()
                .True()
                .QuestionMark()
                .Number(1)
                .Colon()
                .Operation(OperatorToken.Minus)
                .Number(1)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new If(
                Bool.True,
                Number.One,
                new UnaryMinus(Number.One));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void TernaryElseMissingTest()
        {
            var tokens = Builder()
                .True()
                .QuestionMark()
                .Number(1)
                .Colon()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void TernaryIfMissingTest()
        {
            var tokens = Builder()
                .True()
                .QuestionMark()
                .Colon()
                .Operation(OperatorToken.Minus)
                .Number(1)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void TernaryColonMissingTest()
        {
            var tokens = Builder()
                .True()
                .QuestionMark()
                .Number(1)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void TernaryAsExpressionTest()
        {
            var tokens = Builder()
                .Id("sin")
                .OpenParenthesis()
                .True()
                .QuestionMark()
                .Number(1)
                .Colon()
                .Operation(OperatorToken.Minus)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Sin(
                new If(
                    Bool.True,
                    Number.One,
                    new UnaryMinus(Number.One)));

            Assert.Equal(expected, exp);
        }
    }
}