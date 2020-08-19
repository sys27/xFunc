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
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class AssignTests : BaseParserTests
    {
        [Fact]
        public void ParseDefine()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Number(3)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(new Variable("x"), new Number(3));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ParseDefineWithOneParam()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ParseDefineFirstParamIsNotVar()
        {
            var tokens = Builder()
                .Number(5)
                .Operation(OperatorToken.Assign)
                .Number(3)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void DefineComplexParserTest()
        {
            var tokens = Builder()
                .Id("aaa")
                .Operation(OperatorToken.Assign)
                .Number(3)
                .Operation(OperatorToken.Plus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(
                new Variable("aaa"),
                new Add(
                    new Number(3),
                    new Mul(
                        new Number(2),
                        new Variable("i")
                    )
                ));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DefineUserFuncTest()
        {
            var tokens = Builder()
                .Id("func")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .VariableY()
                .CloseParenthesis()
                .Operation(OperatorToken.Assign)
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(
                new UserFunction("func", new IExpression[] { new Variable("x"), new Variable("y") }),
                new Sin(new Variable("x")));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusAssignTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Assign)
                .Operation(OperatorToken.Minus)
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new Define(
                new Variable("x"),
                new UnaryMinus(new Sin(new Number(2)))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AddAssign()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.AddAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new AddAssign(new Variable("x"), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void MulAssign()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.MulAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new MulAssign(new Variable("x"), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void SubAssign()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.SubAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new SubAssign(new Variable("x"), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void DivAssign()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.DivAssign)
                .Number(2)
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new DivAssign(new Variable("x"), new Number(2));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusAddAssignTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.AddAssign)
                .Operation(OperatorToken.Minus)
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new AddAssign(
                new Variable("x"),
                new UnaryMinus(new Sin(new Number(2)))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusSubAssignTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.SubAssign)
                .Operation(OperatorToken.Minus)
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new SubAssign(
                new Variable("x"),
                new UnaryMinus(new Sin(new Number(2)))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusMulAssignTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.MulAssign)
                .Operation(OperatorToken.Minus)
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new MulAssign(
                new Variable("x"),
                new UnaryMinus(new Sin(new Number(2)))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void UnaryMinusDivAssignTest()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.DivAssign)
                .Operation(OperatorToken.Minus)
                .Id("sin")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            var exp = parser.Parse(tokens);
            var expected = new DivAssign(
                new Variable("x"),
                new UnaryMinus(new Sin(new Number(2)))
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void AddAssignMissingValue()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.AddAssign)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void MulAssignMissingValue()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.MulAssign)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void SubAssignMissingValue()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.SubAssign)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void DivAssignMissingValue()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.DivAssign)
                .Tokens;

            ParseErrorTest(tokens);
        }
    }
}