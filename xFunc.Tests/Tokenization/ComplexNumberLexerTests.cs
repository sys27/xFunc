// Copyright 2012-2018 Dmitry Kischenko
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
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using xFunc.Maths;
using xFunc.Maths.Tokenization;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization
{

    public class ComplexNumberLexerTests : BaseLexerTests
    {

        [Fact]
        public void ComplexNumberTest()
        {
            var tokens = lexer.Tokenize("3 + 2i");
            var expected = Builder()
                .Number(3)
                .Operation(Operations.Addition)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberNegativeTest()
        {
            var tokens = lexer.Tokenize("3 - 2i");
            var expected = Builder()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsTest()
        {
            var tokens = lexer.Tokenize("-3 - 2i");
            var expected = Builder()
                .Operation(Operations.UnaryMinus)
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyRePartTest()
        {
            var tokens = lexer.Tokenize("3 + 0i");
            var expected = Builder()
                .Number(3)
                .Operation(Operations.Addition)
                .Number(0)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyImPartTest()
        {
            var tokens = lexer.Tokenize("2i");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyImPartNegativeTest()
        {
            var tokens = lexer.Tokenize("-2i");
            var expected = Builder()
                .Operation(Operations.UnaryMinus)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyITest()
        {
            var tokens = lexer.Tokenize("i");
            var expected = Builder()
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexEmptyImPartTest()
        {
            var tokens = lexer.Tokenize("2 + i");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Addition)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexWithVarTest1()
        {
            var tokens = lexer.Tokenize("x - 2i");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexWithVarTest2()
        {
            var tokens = lexer.Tokenize("x + 3 - 2i");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Addition)
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexFromPolarTest()
        {
            var tokens = lexer.Tokenize("10+45°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(10, 45 * Math.PI / 180))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImTest()
        {
            var tokens = lexer.Tokenize("im(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Im)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImaginaryTest()
        {
            var tokens = lexer.Tokenize("imaginary(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Im)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ReTest()
        {
            var tokens = lexer.Tokenize("re(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Re)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void RealTest()
        {
            var tokens = lexer.Tokenize("real(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Re)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = lexer.Tokenize("phase(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Phase)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = lexer.Tokenize("conjugate(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Conjugate)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = lexer.Tokenize("reciprocal(3 - 2i)");
            var expected = Builder()
                .Function(Functions.Reciprocal)
                .OpenBracket()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .CloseBracket()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPowNumberTest()
        {
            var tokens = lexer.Tokenize("3 - 2i ^ 2");
            var expected = Builder()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Operation(Operations.Exponentiation)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexIPowNumberTest()
        {
            var tokens = lexer.Tokenize("i ^ 2");
            var expected = Builder()
                .ComplexNumber(new Complex(0, 1))
                .Operation(Operations.Exponentiation)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPowComplexTest()
        {
            var tokens = lexer.Tokenize("3 - 2i ^ 2 + 3i");
            var expected = Builder()
                .Number(3)
                .Operation(Operations.Subtraction)
                .Number(2)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Operation(Operations.Exponentiation)
                .Number(2)
                .Operation(Operations.Addition)
                .Number(3)
                .Operation(Operations.Multiplication)
                .ComplexNumber(Complex.ImaginaryOne)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarPosPosTest()
        {
            var tokens = lexer.Tokenize("2.3 + 7.1°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(2.3, 7.1 * Math.PI / 180))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarPosNegTest()
        {
            var tokens = lexer.Tokenize("2.3 - 7.1°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(2.3, -7.1 * Math.PI / 180))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarNegNegTest()
        {
            var tokens = lexer.Tokenize("-2.3 - 7.1°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(-2.3, -7.1 * Math.PI / 180))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarPhaseTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("7.1°"));
        }

        [Fact]
        public void ComplexPolarNegPhaseTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("-7.1°"));
        }

        [Fact]
        public void ComplexPolarMagnitudeTest()
        {
            var tokens = lexer.Tokenize("2.3 + 0°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(2.3, 0))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarNegMagnitudeTest()
        {
            var tokens = lexer.Tokenize("-2.3 + 0°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(-2.3, 0))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarTest()
        {
            var tokens = lexer.Tokenize("10 ∠ 45°");
            var expected = Builder()
                .ComplexNumber(Complex.FromPolarCoordinates(10, 45 * Math.PI / 180))
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarInvalidTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("x°"));
        }

    }

}