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

using System.Linq;
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
                .Operation(OperatorToken.Plus)
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberNegativeTest()
        {
            var tokens = lexer.Tokenize("3 - 2i");
            var expected = Builder()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsTest()
        {
            var tokens = lexer.Tokenize("-3 - 2i");
            var expected = Builder()
                .Operation(OperatorToken.Minus)
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyRePartTest()
        {
            var tokens = lexer.Tokenize("3 + 0i");
            var expected = Builder()
                .Number(3)
                .Operation(OperatorToken.Plus)
                .Number(0)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyImPartTest()
        {
            var tokens = lexer.Tokenize("2i");
            var expected = Builder()
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyImPartNegativeTest()
        {
            var tokens = lexer.Tokenize("-2i");
            var expected = Builder()
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyITest()
        {
            var tokens = lexer.Tokenize("i");
            var expected = Builder()
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexEmptyImPartTest()
        {
            var tokens = lexer.Tokenize("2 + i");
            var expected = Builder()
                .Number(2)
                .Operation(OperatorToken.Plus)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexWithVarTest1()
        {
            var tokens = lexer.Tokenize("x - 2i");
            var expected = Builder()
                .VariableX()
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexWithVarTest2()
        {
            var tokens = lexer.Tokenize("x + 3 - 2i");
            var expected = Builder()
                .VariableX()
                .Operation(OperatorToken.Plus)
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImTest()
        {
            var tokens = lexer.Tokenize("im(3 - 2i)");
            var expected = Builder()
                .Id("im")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImaginaryTest()
        {
            var tokens = lexer.Tokenize("imaginary(3 - 2i)");
            var expected = Builder()
                .Id("imaginary")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ReTest()
        {
            var tokens = lexer.Tokenize("re(3 - 2i)");
            var expected = Builder()
                .Id("re")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void RealTest()
        {
            var tokens = lexer.Tokenize("real(3 - 2i)");
            var expected = Builder()
                .Id("real")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = lexer.Tokenize("phase(3 - 2i)");
            var expected = Builder()
                .Id("phase")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = lexer.Tokenize("conjugate(3 - 2i)");
            var expected = Builder()
                .Id("conjugate")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = lexer.Tokenize("reciprocal(3 - 2i)");
            var expected = Builder()
                .Id("reciprocal")
                .OpenParenthesis()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPowNumberTest()
        {
            var tokens = lexer.Tokenize("3 - 2i ^ 2");
            var expected = Builder()
                .Number(3)
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Operation(OperatorToken.Exponentiation)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexIPowNumberTest()
        {
            var tokens = lexer.Tokenize("i ^ 2");
            var expected = Builder()
                .Id("i")
                .Operation(OperatorToken.Exponentiation)
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
                .Operation(OperatorToken.Minus)
                .Number(2)
                .Id("i")
                .Operation(OperatorToken.Exponentiation)
                .Number(2)
                .Operation(OperatorToken.Plus)
                .Number(3)
                .Id("i")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarTest()
        {
            var tokens = lexer.Tokenize("10 ∠ 45°");
            var expected = Builder()
                .Number(10)
                .Angle()
                .Number(45)
                .Degree()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarPosNegTest()
        {
            var tokens = lexer.Tokenize("2.3 ∠ -7.1°");
            var expected = Builder()
                .Number(2.3)
                .Angle()
                .Operation(OperatorToken.Minus)
                .Number(7.1)
                .Degree()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }
    }
}