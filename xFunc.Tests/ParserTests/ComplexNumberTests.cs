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

using System;
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.ParserTests
{
    public class ComplexNumberTests : BaseParserTests
    {
        [Fact]
        public void ComplexNumberTest()
        {
            var tokens = Builder()
                .Number(3)
                .Operation(OperatorToken.Plus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeTest()
        {
            var tokens = Builder()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Sub(
                new Number(3),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsTest()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Sub(
                new UnaryMinus(new Number(3)),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsWithoutMulTest()
        {
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Sub(
                new UnaryMinus(new Number(3)),
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyRePartTest()
        {
            var tokens = Builder()
                .Number(3)
                .Operation(OperatorToken.Plus)
                .Number(0)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(
                new Number(3),
                new Mul(
                    Number.Zero,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyImPartTest()
        {
            var tokens = Builder()
                .Number(0)
                .Operation(OperatorToken.Plus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(
                Number.Zero,
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexOnlyImPartNegativeTest()
        {
            var tokens = Builder()
                .Number(0)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Sub(
                Number.Zero,
                new Mul(
                    Number.Two,
                    new Variable("i")
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexWithVarTest1()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Minus)
                .OpenParenthesis()
                .Number(0)
                .Operation(OperatorToken.Plus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Sub(
                Variable.X,
                new Add(
                    Number.Zero,
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexWithVarTest2()
        {
            var tokens = Builder()
                .VariableX()
                .Operation(OperatorToken.Plus)
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Add(
                Variable.X,
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexFromPolarTest()
        {
            var complex = Complex.FromPolarCoordinates(10, 45 * Math.PI / 180);
            var tokens = Builder()
                .Number(10)
                .Angle()
                .Number(45 * Math.PI / 180)
                .Degree()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(complex);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexFromPolarNegativePhaseTest()
        {
            var complex = Complex.FromPolarCoordinates(10, -7.1);
            var tokens = Builder()
                .Number(10)
                .Angle()
                .Operation(OperatorToken.Minus)
                .Number(7.1)
                .Degree()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new ComplexNumber(complex);

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexFromPolarNegativeMagnitudeTest()
        {
            var complex = Complex.FromPolarCoordinates(10, 7.1);
            var tokens = Builder()
                .Operation(OperatorToken.Minus)
                .Number(10)
                .Angle()
                .Number(7.1)
                .Degree()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new UnaryMinus(new ComplexNumber(complex));

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ComplexFromPolarMissingPhaseTest()
        {
            var tokens = Builder()
                .Number(10)
                .Angle()
                .Degree()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ComplexFromPolarMissingDegreeTest()
        {
            var tokens = Builder()
                .Number(10)
                .Angle()
                .Number(45 * Math.PI / 180)
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ComplexPolarPhaseVariableExceptionTest()
        {
            var tokens = Builder()
                .VariableX()
                .Degree()
                .Tokens;

            ParseErrorTest(tokens);
        }

        [Fact]
        public void ImTest()
        {
            var tokens = Builder()
                .Id("im")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Im(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ImaginaryTest()
        {
            var tokens = Builder()
                .Id("imaginary")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Im(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReTest()
        {
            var tokens = Builder()
                .Id("re")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Re(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void RealTest()
        {
            var tokens = Builder()
                .Id("real")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Re(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = Builder()
                .Id("phase")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Phase(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = Builder()
                .Id("conjugate")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Conjugate(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = Builder()
                .Id("reciprocal")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Operation(OperatorToken.Multiplication)
                .Id("i")
                .CloseParenthesis()
                .Tokens;
            var exp = parser.Parse(tokens);
            var expected = new Reciprocal(
                new Sub(
                    new Number(3),
                    new Mul(
                        Number.Two,
                        new Variable("i")
                    )
                )
            );

            Assert.Equal(expected, exp);
        }
    }
}