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
using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Tokenization.Tokens;
using Xunit;

namespace xFunc.Tests.Tokenization
{

    public class LexerTest : BaseLexerTests
    {

        [Fact]
        public void NullString()
        {
            Assert.Throws<ArgumentNullException>(() => lexer.Tokenize(null));
        }

        [Fact]
        public void NotSupportedSymbol()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("@"));
        }

        [Fact]
        public void Brackets()
        {
            var tokens = lexer.Tokenize("(2)");
            var expected = Builder()
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ExpNumber1()
        {
            var tokens = lexer.Tokenize("1.2345E-10");
            var expected = Builder().Number(0.00000000012345).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ExpNumber2()
        {
            var tokens = lexer.Tokenize("1.2345E10");
            var expected = Builder().Number(12345000000).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ExpNumber3()
        {
            var tokens = lexer.Tokenize("1.2e2 + 2.1e-3");
            var expected = Builder()
                .Number(120)
                .Operation(Operations.Plus)
                .Number(0.0021)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Add()
        {
            var tokens = lexer.Tokenize("2 + 2");

            var expected = Builder()
                .Number(2)
                .Operation(Operations.Plus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(+2)");
            var expected = Builder()
                .OpenParenthesis()
                .Operation(Operations.Plus)
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddBeforeNumber()
        {
            var tokens = lexer.Tokenize("+2");
            var expected = Builder()
                .Operation(Operations.Plus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }


        [Fact]
        public void Sub()
        {
            var tokens = lexer.Tokenize("2 - 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Minus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubAlt()
        {
            var tokens = lexer.Tokenize("2 − 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Minus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(-2)");
            var expected = Builder()
                .OpenParenthesis()
                .Operation(Operations.Minus)
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubBeforeNumber()
        {
            var tokens = lexer.Tokenize("-2");
            var expected = Builder()
                .Operation(Operations.Minus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UnaryMinusInAssign()
        {
            var tokens = lexer.Tokenize("x := -2");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Assign)
                .Operation(Operations.Minus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Mul()
        {
            var tokens = lexer.Tokenize("2 * 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Multiplication)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulAlt()
        {
            var tokens = lexer.Tokenize("2 × 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Multiplication)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Div()
        {
            var tokens = lexer.Tokenize("2 / 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Division)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Inv()
        {
            var tokens = lexer.Tokenize("2 ^ 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Exponentiation)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Comma()
        {
            var tokens = lexer.Tokenize("log(2, 2)");
            var expected = Builder()
                .Id("log")
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Not()
        {
            var tokens = lexer.Tokenize("not(2)");
            var expected = Builder()
                .Operation(Operations.Not)
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotAsOperator()
        {
            var tokens = lexer.Tokenize("~2");
            var expected = Builder()
                .Operation(Operations.Not)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void And()
        {
            var tokens = lexer.Tokenize("2 & 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.And)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Or()
        {
            var tokens = lexer.Tokenize("2 | 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Or)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImplicationSymbolTest1()
        {
            var tokens = lexer.Tokenize("true -> false");
            var expected = Builder()
                .True()
                .Operation(Operations.Implication)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImplicationSymbolTest2()
        {
            var tokens = lexer.Tokenize("true => false");
            var expected = Builder()
                .True()
                .Operation(Operations.Implication)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualitySymbolTest1()
        {
            var tokens = lexer.Tokenize("true <-> false");
            var expected = Builder()
                .True()
                .Operation(Operations.Equality)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualitySymbolTestAlt1()
        {
            var tokens = lexer.Tokenize("true <−> false");
            var expected = Builder()
                .True()
                .Operation(Operations.Equality)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualitySymbolTest2()
        {
            var tokens = lexer.Tokenize("true <=> false");
            var expected = Builder()
                .True()
                .Operation(Operations.Equality)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Assign()
        {
            var tokens = lexer.Tokenize("x := 2");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Assign)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DefineVar()
        {
            var tokens = lexer.Tokenize("def(x, 2)");
            var expected = Builder()
                .Def()
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DefineFunc()
        {
            var tokens = lexer.Tokenize("def(f(x), 2)");
            var expected = Builder()
                .Def()
                .OpenParenthesis()
                .Id("f")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Integer()
        {
            var tokens = lexer.Tokenize("-2764786 + 46489879");
            var expected = Builder()
                .Operation(Operations.Minus)
                .Number(2764786)
                .Operation(Operations.Plus)
                .Number(46489879)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Double()
        {
            var tokens = lexer.Tokenize("-45.3 + 87.64");
            var expected = Builder()
                .Operation(Operations.Minus)
                .Number(45.3)
                .Operation(Operations.Plus)
                .Number(87.64)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumAndVar()
        {
            var tokens = lexer.Tokenize("-2x");
            var expected = Builder()
                .Operation(Operations.Minus)
                .Number(2)
                .VariableX()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumAndFunc()
        {
            var tokens = lexer.Tokenize("5cos(x)");
            var expected = Builder()
                .Number(5)
                .Id("cos")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumberMulBracketTest()
        {
            var tokens = lexer.Tokenize("2(x + y)");
            var expected = Builder()
                .Number(2)
                .OpenParenthesis()
                .VariableX()
                .Operation(Operations.Plus)
                .VariableY()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumberMulVectorTest()
        {
            var tokens = lexer.Tokenize("2{ 1, 2 }");
            var expected = Builder()
                .Number(2)
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumberMulMatrixTest()
        {
            var tokens = lexer.Tokenize("2{ { 1, 2 }, { 3, 4 } }");
            var expected = Builder()
                .Number(2)
                .OpenBrace()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarWithNumber1()
        {
            var tokens = lexer.Tokenize("x1");
            var expected = Builder().Id("x1").Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarWithNumber2()
        {
            var tokens = lexer.Tokenize("xdsa13213");
            var expected = Builder().Id("xdsa13213").Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarWithNumber3()
        {
            var tokens = lexer.Tokenize("x1b2v3");
            var expected = Builder().Id("x1b2v3").Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Pi()
        {
            var tokens = lexer.Tokenize("3pi");
            var expected = Builder()
                .Number(3)
                .Id("pi")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Deriv()
        {
            var tokens = lexer.Tokenize("deriv(sin(x), x)");
            var expected = Builder()
                .Id("deriv")
                .OpenParenthesis()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Comma()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotAsWord()
        {
            var tokens = lexer.Tokenize("~2");
            var expected = Builder()
                .Operation(Operations.Not)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AndAsWord()
        {
            var tokens = lexer.Tokenize("1 and 2");
            var expected = Builder()
                .Number(1)
                .Operation(Operations.And)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void OrAsWord()
        {
            var tokens = lexer.Tokenize("1 or 2");
            var expected = Builder()
                .Number(1)
                .Operation(Operations.Or)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void XOrAsWord()
        {
            var tokens = lexer.Tokenize("1 xor 2");
            var expected = Builder()
                .Number(1)
                .Operation(Operations.XOr)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImplicationAsWordTest()
        {
            var tokens = lexer.Tokenize("true impl false");
            var expected = Builder()
                .True()
                .Operation(Operations.Implication)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualityAsWordTest()
        {
            var tokens = lexer.Tokenize("true eq false");
            var expected = Builder()
                .True()
                .Operation(Operations.Equality)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NOrAsWordTest()
        {
            var tokens = lexer.Tokenize("true nor false");
            var expected = Builder()
                .True()
                .Operation(Operations.NOr)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NAndAsWordTest()
        {
            var tokens = lexer.Tokenize("true nand false");
            var expected = Builder()
                .True()
                .Operation(Operations.NAnd)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Var()
        {
            var tokens = lexer.Tokenize("x * y");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Multiplication)
                .VariableY()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BitVar()
        {
            var tokens = lexer.Tokenize("x and x");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.And)
                .VariableX()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void StringVarLexerTest()
        {
            var tokens = lexer.Tokenize("aaa := 1");
            var expected = Builder()
                .Id("aaa")
                .Operation(Operations.Assign)
                .Number(1)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void StringVarAnd()
        {
            var tokens = lexer.Tokenize("func and 1");
            var expected = Builder()
                .Id("func")
                .Operation(Operations.And)
                .Number(1)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFunc()
        {
            var tokens = lexer.Tokenize("func(x)");
            var expected = Builder()
                .Id("func")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncTwoVars()
        {
            var tokens = lexer.Tokenize("func(x, y)");
            var expected = Builder()
                .Id("func")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .VariableY()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncInUserFuncTwo()
        {
            var tokens = lexer.Tokenize("func(x, sin(x))");
            var expected = Builder()
                .Id("func")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncInUserFunc()
        {
            var tokens = lexer.Tokenize("f(x, g(y))");
            var expected = Builder()
                .Id("f")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Id("g")
                .OpenParenthesis()
                .VariableY()
                .CloseParenthesis()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UndefFunc()
        {
            var tokens = lexer.Tokenize("undef(f(x))");
            var expected = Builder()
                .Undef()
                .OpenParenthesis()
                .Id("f")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotBalancedOpen()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("sin(2("));
        }

        [Fact]
        public void NotBalancedClose()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("sin)2)"));
        }

        [Fact]
        public void NotBalancedFirstClose()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("sin)2("));
        }

        [Fact]
        public void NotBalancedBracesOpen()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("{2,1"));
        }

        [Fact]
        public void NotBalancedBracesClose()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("}2,1"));
        }

        [Fact]
        public void HexTest()
        {
            var tokens = lexer.Tokenize("0xFF00");
            var expected = Builder().Number(65280).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void OctTest()
        {
            var tokens = lexer.Tokenize("0436");
            var expected = Builder().Number(286).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BinTest()
        {
            var tokens = lexer.Tokenize("0b01100110");
            var expected = Builder().Number(102).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ZeroSubTwoTest()
        {
            var tokens = lexer.Tokenize("0-2");
            var expected = Builder()
                .Number(0)
                .Operation(Operations.Minus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GCDTest()
        {
            var tokens = lexer.Tokenize("gcd(12, 16)");
            var expected = Builder()
                .Id("gcd")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GCFTest()
        {
            var tokens = lexer.Tokenize("gcf(12, 16)");
            var expected = Builder()
                .Id("gcf")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HCFTest()
        {
            var tokens = lexer.Tokenize("hcf(12, 16)");
            var expected = Builder()
                .Id("hcf")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void LCMTest()
        {
            var tokens = lexer.Tokenize("lcm(12, 16)");
            var expected = Builder()
                .Id("lcm")
                .OpenParenthesis()
                .Number(12)
                .Comma()
                .Number(16)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SimplifyTest()
        {
            var tokens = lexer.Tokenize("simplify(x)");
            var expected = Builder()
                .Id("simplify")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialTest()
        {
            var tokens = lexer.Tokenize("fact(4)");
            var expected = Builder()
                .Id("fact")
                .OpenParenthesis()
                .Number(4)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialOperatorTest()
        {
            var tokens = lexer.Tokenize("4!");
            var expected = Builder()
                .Number(4)
                .Operation(Operations.Factorial)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialVarOperatorTest()
        {
            var tokens = lexer.Tokenize("x!");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Factorial)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void RootInRootTest()
        {
            var tokens = lexer.Tokenize("root(1 + root(2, x), 2)");
            var expected = Builder()
                .Id("root")
                .OpenParenthesis()
                .Number(1)
                .Operation(Operations.Plus)
                .Id("root")
                .OpenParenthesis()
                .Number(2)
                .Comma()
                .VariableX()
                .CloseParenthesis()
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BracketsForAllParamsTest()
        {
            var tokens = lexer.Tokenize("(3)cos((u))cos((v))");
            var expected = Builder()
                .OpenParenthesis()
                .Number(3)
                .CloseParenthesis()
                .Id("cos")
                .OpenParenthesis()
                .OpenParenthesis()
                .Id("u")
                .CloseParenthesis()
                .CloseParenthesis()
                .Id("cos")
                .OpenParenthesis()
                .OpenParenthesis()
                .Id("v")
                .CloseParenthesis()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ZeroTest()
        {
            var tokens = lexer.Tokenize("0");
            var expected = Builder().Number(0).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HexErrorTest()
        {
            var tokens = lexer.Tokenize("0x");
            var expected = Builder()
                .Number(0)
                .VariableX()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BinErrorTest()
        {
            var tokens = lexer.Tokenize("0b");
            var expected = Builder()
                .Number(0)
                .Id("b")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumToTest()
        {
            var tokens = lexer.Tokenize("sum(x, 20)");
            var expected = Builder()
                .Id("sum")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(20)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumFromToTest()
        {
            var tokens = lexer.Tokenize("sum(x, 2, 20)");
            var expected = Builder()
                .Id("sum")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .Comma()
                .Number(20)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumFromToIncTest()
        {
            var tokens = lexer.Tokenize("sum(x, 2, 20, 2)");
            var expected = Builder()
                .Id("sum")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .Comma()
                .Number(20)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumFromToIncVarTest()
        {
            var tokens = lexer.Tokenize("sum(k, 2, 20, 2, k)");
            var expected = Builder()
                .Id("sum")
                .OpenParenthesis()
                .Id("k")
                .Comma()
                .Number(2)
                .Comma()
                .Number(20)
                .Comma()
                .Number(2)
                .Comma()
                .Id("k")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductToTest()
        {
            var tokens = lexer.Tokenize("product(x, 20)");

            var expected = Builder()
                .Id("product")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(20)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductFromToTest()
        {
            var tokens = lexer.Tokenize("product(x, 2, 20)");
            var expected = Builder()
                .Id("product")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .Comma()
                .Number(20)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductFromToIncTest()
        {
            var tokens = lexer.Tokenize("product(x, 2, 20, 2)");
            var expected = Builder()
                .Id("product")
                .OpenParenthesis()
                .VariableX()
                .Comma()
                .Number(2)
                .Comma()
                .Number(20)
                .Comma()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductFromToIncVarTest()
        {
            var tokens = lexer.Tokenize("product(k, 2, 20, 2, k)");
            var expected = Builder()
                .Id("product")
                .OpenParenthesis()
                .Id("k")
                .Comma()
                .Number(2)
                .Comma()
                .Number(20)
                .Comma()
                .Number(2)
                .Comma()
                .Id("k")
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VectorBracesTest()
        {
            var tokens = lexer.Tokenize("vector{2, 3, 4}");
            var expected = Builder()
                .Id("vector")
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MatrixAndVectorTest()
        {
            var tokens = lexer.Tokenize("matrix{vector{2, 3}, vector{4, 7}}");
            var expected = Builder()
                .Id("matrix")
                .OpenBrace()
                .Id("vector")
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .Id("vector")
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void TransposeTest()
        {
            var tokens = lexer.Tokenize("transpose(2)");
            var expected = Builder()
                .Id("transpose")
                .OpenParenthesis()
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulMatrixTest()
        {
            var tokens = lexer.Tokenize("matrix{vector{3}, vector{-1}} * vector{-2, 1}");
            var expected = Builder()
                .Id("matrix")
                .OpenBrace()
                .Id("vector")
                .OpenBrace()
                .Number(3)
                .CloseBrace()
                .Comma()
                .Id("vector")
                .OpenBrace()
                .Operation(Operations.Minus)
                .Number(1)
                .CloseBrace()
                .CloseBrace()
                .Operation(Operations.Multiplication)
                .Id("vector")
                .OpenBrace()
                .Operation(Operations.Minus)
                .Number(2)
                .Comma()
                .Number(1)
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ShortVectorTest()
        {
            var tokens = lexer.Tokenize("{4, 7}");
            var expected = Builder()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ShortMatrixTest()
        {
            var tokens = lexer.Tokenize("{{2, 3}, {4, 7}}");
            var expected = Builder()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DeterminantTest()
        {
            var tokens = lexer.Tokenize("determinant({{2, 3}, {4, 7}})");
            var expected = Builder()
                .Id("determinant")
                .OpenParenthesis()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DetTest()
        {
            var tokens = lexer.Tokenize("det({{2, 3}, {4, 7}})");
            var expected = Builder()
                .Id("det")
                .OpenParenthesis()
                .OpenBrace()
                .OpenBrace()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void InverseTest()
        {
            var tokens = lexer.Tokenize("inverse({4, 7})");
            var expected = Builder()
                .Id("inverse")
                .OpenParenthesis()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(7)
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DotProductTest()
        {
            var tokens = lexer.Tokenize("dotProduct({1, 2}, {3, 4})");
            var expected = Builder()
                .Id("dotproduct")
                .OpenParenthesis()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(3)
                .Comma()
                .Number(4)
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void CrossProductTest()
        {
            var tokens = lexer.Tokenize("crossProduct({1, 2, 3}, {4, 5, 6})");
            var expected = Builder()
                .Id("crossproduct")
                .OpenParenthesis()
                .OpenBrace()
                .Number(1)
                .Comma()
                .Number(2)
                .Comma()
                .Number(3)
                .CloseBrace()
                .Comma()
                .OpenBrace()
                .Number(4)
                .Comma()
                .Number(5)
                .Comma()
                .Number(6)
                .CloseBrace()
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void IfTest()
        {
            var tokens = lexer.Tokenize("if(z, x ^ 2)");
            var expected = Builder()
                .If()
                .OpenParenthesis()
                .Id("z")
                .Comma()
                .VariableX()
                .Operation(Operations.Exponentiation)
                .Number(2)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void IfElseNegativeTest()
        {
            var tokens = lexer.Tokenize("if(True, 1, -1)");
            var expected = Builder()
                .If()
                .OpenParenthesis()
                .True()
                .Comma()
                .Number(1)
                .Comma()
                .Operation(Operations.Minus)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ForTest()
        {
            var tokens = lexer.Tokenize("for(z := z + 1)");
            var expected = Builder()
                .For()
                .OpenParenthesis()
                .Id("z")
                .Operation(Operations.Assign)
                .Id("z")
                .Operation(Operations.Plus)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void WhileTest()
        {
            var tokens = lexer.Tokenize("while(z := z + 1)");
            var expected = Builder()
                .While()
                .OpenParenthesis()
                .Id("z")
                .Operation(Operations.Assign)
                .Id("z")
                .Operation(Operations.Plus)
                .Number(1)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConditionalAndTest()
        {
            var tokens = lexer.Tokenize("x && y");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.ConditionalAnd)
                .VariableY()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var tokens = lexer.Tokenize("x || y");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.ConditionalOr)
                .VariableY()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void LessThenTest()
        {
            var tokens = lexer.Tokenize("x < 10");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.LessThan)
                .Number(10)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var tokens = lexer.Tokenize("x <= 10");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.LessOrEqual)
                .Number(10)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GreaterThenTest()
        {
            var tokens = lexer.Tokenize("x > 10");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.GreaterThan)
                .Number(10)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var tokens = lexer.Tokenize("x >= 10");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.GreaterOrEqual)
                .Number(10)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void IncTest()
        {
            var tokens = lexer.Tokenize("x++");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Increment)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DecTest()
        {
            var tokens = lexer.Tokenize("x--");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.Decrement)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddAssign()
        {
            var tokens = lexer.Tokenize("x += 2");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.AddAssign)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubAssign()
        {
            var tokens = lexer.Tokenize("x -= 2");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.SubAssign)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulAssign()
        {
            var tokens = lexer.Tokenize("x *= 2");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.MulAssign)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DivAssign()
        {
            var tokens = lexer.Tokenize("x /= 2");
            var expected = Builder()
                .VariableX()
                .Operation(Operations.DivAssign)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BoolConstLongTest()
        {
            var tokens = lexer.Tokenize("true & false");
            var expected = Builder()
                .True()
                .Operation(Operations.And)
                .False()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualTest()
        {
            var tokens = lexer.Tokenize("1 == 1");
            var expected = Builder()
                .Number(1)
                .Operation(Operations.Equal)
                .Number(1)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotEqualTest()
        {
            var tokens = lexer.Tokenize("1 != 1");
            var expected = Builder()
                .Number(1)
                .Operation(Operations.NotEqual)
                .Number(1)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumberFormatTest()
        {
            Assert.Throws<TokenizeException>(() => lexer.Tokenize("0."));
        }

        [Fact]
        public void TabTest()
        {
            var tokens = lexer.Tokenize("\t2 + 2");

            var expected = Builder()
                .Number(2)
                .Operation(Operations.Plus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NewlineTest()
        {
            var tokens = lexer.Tokenize("\n2 + 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Plus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void CRTest()
        {
            var tokens = lexer.Tokenize("\r2 + 2");
            var expected = Builder()
                .Number(2)
                .Operation(Operations.Plus)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ModuloTest()
        {
            var tokens = lexer.Tokenize("7 % 2");
            var expected = Builder()
                .Number(7)
                .Operation(Operations.Modulo)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ModuloAsFuncTest()
        {
            var tokens = lexer.Tokenize("7 mod 2");
            var expected = Builder()
                .Number(7)
                .Operation(Operations.Modulo)
                .Number(2)
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UseGreekLettersTest()
        {
            var tokens = lexer.Tokenize("4 * φ");
            var expected = Builder()
                .Number(4)
                .Operation(Operations.Multiplication)
                .Id("φ")
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotOperationCaseInsensitive()
        {
            var tokens = lexer.Tokenize("nOt(4)");
            var expected = Builder()
                .Operation(Operations.Not)
                .OpenParenthesis()
                .Number(4)
                .CloseParenthesis()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HexNumberCaseInsensitive()
        {
            var tokens = lexer.Tokenize("0XFF");
            var expected = Builder().Number(0xFF).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BinNumberCaseInsensitive()
        {
            var tokens = lexer.Tokenize("0b1001");
            var expected = Builder().Number(0b1001).Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void TrueConstCaseInsensitive()
        {
            var tokens = lexer.Tokenize("tRuE");
            var expected = Builder().True().Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SinCaseInsensitive()
        {
            var tokens = lexer.Tokenize("sIn(x)");
            var expected = Builder()
                .Id("sin")
                .OpenParenthesis()
                .VariableX()
                .CloseParenthesis()
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
                .Operation(Operations.Minus)
                .Number(7.1)
                .Degree()
                .Tokens;

            Assert.Equal(expected, tokens.ToList());
        }

    }

}