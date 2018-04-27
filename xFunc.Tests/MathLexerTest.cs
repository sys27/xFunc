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

namespace xFunc.Tests
{

    public class MathLexerTest
    {

        private ILexer lexer;

        public MathLexerTest()
        {
            lexer = new Lexer();
        }

        private void FuncTest(string func, Functions type)
        {
            var tokens = lexer.Tokenize(func + "(3)");

            var expected = new List<IToken>()
            {
                new FunctionToken(type, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NullString()
        {
            Assert.Throws<ArgumentNullException>(() => lexer.Tokenize(null));
        }

        [Fact]
        public void NotSupportedSymbol()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("@"));
        }

        [Fact]
        public void Brackets()
        {
            var tokens = lexer.Tokenize("(2)");

            var expected = new List<IToken>()
            {
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ExpNumber1()
        {
            var tokens = lexer.Tokenize("1.2345E-10");
            var expected = new List<IToken>
            {
                new NumberToken(0.00000000012345)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ExpNumber2()
        {
            var tokens = lexer.Tokenize("1.2345E10");
            var expected = new List<IToken>
            {
                new NumberToken(12345000000)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ExpNumber3()
        {
            var tokens = lexer.Tokenize("1.2e2 + 2.1e-3");
            var expected = new List<IToken>
            {
                new NumberToken(120),
                new OperationToken(Operations.Addition),
                new NumberToken(0.0021)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Add()
        {
            var tokens = lexer.Tokenize("2 + 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(+2)");

            var expected = new List<IToken>()
            {
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddBeforeNumber()
        {
            var tokens = lexer.Tokenize("+2");

            var expected = new List<IToken>()
            {
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Sub()
        {
            var tokens = lexer.Tokenize("2 - 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubAlt()
        {
            var tokens = lexer.Tokenize("2 − 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(-2)");

            var expected = new List<IToken>()
            {
                new SymbolToken(Symbols.OpenBracket),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubBeforeNumber()
        {
            var tokens = lexer.Tokenize("-2");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UnaryMinusAfterMulTest()
        {
            var tokens = lexer.Tokenize("2 * -2");
            var expected = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UnaryMinusInDivision()
        {
            var tokens = lexer.Tokenize("1 / -2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.Division),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UnaryMinusInAssign()
        {
            var tokens = lexer.Tokenize("x := -2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Mul()
        {
            var tokens = lexer.Tokenize("2 * 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulAlt()
        {
            var tokens = lexer.Tokenize("2 × 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Div()
        {
            var tokens = lexer.Tokenize("2 / 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Division),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Inv()
        {
            var tokens = lexer.Tokenize("2 ^ 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Comma()
        {
            var tokens = lexer.Tokenize("log(2, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Log, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Not()
        {
            var tokens = lexer.Tokenize("not(2)");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.Not),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotAsOperator()
        {
            var tokens = lexer.Tokenize("~2");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.Not),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotAsOperatorFail()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("2~"));
        }

        [Fact]
        public void NotWithVarAsOperatorFail()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("x~"));
        }

        [Fact]
        public void And()
        {
            var tokens = lexer.Tokenize("2 & 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.And),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Or()
        {
            var tokens = lexer.Tokenize("2 | 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Or),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImplicationSymbolTest1()
        {
            var tokens = lexer.Tokenize("true -> false");

            var expected = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.Implication),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImplicationSymbolTest2()
        {
            var tokens = lexer.Tokenize("true => false");

            var expected = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.Implication),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualitySymbolTest1()
        {
            var tokens = lexer.Tokenize("true <-> false");

            var expected = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.Equality),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualitySymbolTestAlt1()
        {
            var tokens = lexer.Tokenize("true <−> false");

            var expected = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.Equality),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualitySymbolTest2()
        {
            var tokens = lexer.Tokenize("true <=> false");

            var expected = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.Equality),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Assign()
        {
            var tokens = lexer.Tokenize("x := 2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DefineVar()
        {
            var tokens = lexer.Tokenize("def(x, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Define, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DefineFunc()
        {
            var tokens = lexer.Tokenize("def(f(x), 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Define, 2),
                new SymbolToken(Symbols.OpenBracket),
                new UserFunctionToken("f", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Integer()
        {
            var tokens = lexer.Tokenize("-2764786 + 46489879");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2764786),
                new OperationToken(Operations.Addition),
                new NumberToken(46489879)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Double()
        {
            var tokens = lexer.Tokenize("-45.3 + 87.64");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(45.3),
                new OperationToken(Operations.Addition),
                new NumberToken(87.64)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumAndVar()
        {
            var tokens = lexer.Tokenize("-2x");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new VariableToken("x")
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumAndFunc()
        {
            var tokens = lexer.Tokenize("5cos(x)");

            var expected = new List<IToken>()
            {
                new NumberToken(5),
                new OperationToken(Operations.Multiplication),
                new FunctionToken(Functions.Cosine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarWithNumber1()
        {
            var tokens = lexer.Tokenize("x1");

            var expected = new List<IToken>
            {
                new VariableToken("x1")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarWithNumber2()
        {
            var tokens = lexer.Tokenize("xdsa13213");

            var expected = new List<IToken>
            {
                new VariableToken("xdsa13213")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarWithNumber3()
        {
            var tokens = lexer.Tokenize("x1b2v3");

            var expected = new List<IToken>
            {
                new VariableToken("x1b2v3")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Pi()
        {
            var tokens = lexer.Tokenize("3pi");

            var expected = new List<IToken>()
            {
                new NumberToken(3),
                new OperationToken(Operations.Multiplication),
                new VariableToken("pi")
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Exp()
        {
            FuncTest("exp", Functions.Exp);
        }

        [Fact]
        public void Abs()
        {
            FuncTest("abs", Functions.Absolute);
        }

        [Fact]
        public void Sh()
        {
            FuncTest("sh", Functions.Sineh);
        }

        [Fact]
        public void Sinh()
        {
            FuncTest("sinh", Functions.Sineh);
        }

        [Fact]
        public void Ch()
        {
            FuncTest("ch", Functions.Cosineh);
        }

        [Fact]
        public void Cosh()
        {
            FuncTest("cosh", Functions.Cosineh);
        }

        [Fact]
        public void Th()
        {
            FuncTest("th", Functions.Tangenth);
        }

        [Fact]
        public void Tanh()
        {
            FuncTest("tanh", Functions.Tangenth);
        }

        [Fact]
        public void Cth()
        {
            FuncTest("cth", Functions.Cotangenth);
        }

        [Fact]
        public void Coth()
        {
            FuncTest("coth", Functions.Cotangenth);
        }

        [Fact]
        public void Sech()
        {
            FuncTest("sech", Functions.Secanth);
        }

        [Fact]
        public void Csch()
        {
            FuncTest("csch", Functions.Cosecanth);
        }

        [Fact]
        public void Arsinh()
        {
            FuncTest("arsinh", Functions.Arsineh);
        }

        [Fact]
        public void Arsh()
        {
            FuncTest("arsh", Functions.Arsineh);
        }

        [Fact]
        public void Arcosh()
        {
            FuncTest("arcosh", Functions.Arcosineh);
        }

        [Fact]
        public void Arch()
        {
            FuncTest("arch", Functions.Arcosineh);
        }

        [Fact]
        public void Artanh()
        {
            FuncTest("artanh", Functions.Artangenth);
        }

        [Fact]
        public void Arth()
        {
            FuncTest("arth", Functions.Artangenth);
        }

        [Fact]
        public void Arcoth()
        {
            FuncTest("arcoth", Functions.Arcotangenth);
        }

        [Fact]
        public void Arcth()
        {
            FuncTest("arcth", Functions.Arcotangenth);
        }

        [Fact]
        public void Arsech()
        {
            FuncTest("arsech", Functions.Arsecanth);
        }

        [Fact]
        public void Arsch()
        {
            FuncTest("arsch", Functions.Arsecanth);
        }

        [Fact]
        public void Arcsch()
        {
            FuncTest("arcsch", Functions.Arcosecanth);
        }

        [Fact]
        public void Sin()
        {
            FuncTest("sin", Functions.Sine);
        }

        [Fact]
        public void Cosec()
        {
            FuncTest("cosec", Functions.Cosecant);
        }

        [Fact]
        public void Csc()
        {
            FuncTest("csc", Functions.Cosecant);
        }

        [Fact]
        public void Cos()
        {
            FuncTest("cos", Functions.Cosine);
        }

        [Fact]
        public void Tg()
        {
            FuncTest("tg", Functions.Tangent);
        }

        [Fact]
        public void Tan()
        {
            FuncTest("tan", Functions.Tangent);
        }

        [Fact]
        public void Ctg()
        {
            FuncTest("ctg", Functions.Cotangent);
        }

        [Fact]
        public void Cot()
        {
            FuncTest("cot", Functions.Cotangent);
        }

        [Fact]
        public void Sec()
        {
            FuncTest("sec", Functions.Secant);
        }

        [Fact]
        public void Arcsin()
        {
            FuncTest("arcsin", Functions.Arcsine);
        }

        [Fact]
        public void Arccosec()
        {
            FuncTest("arccosec", Functions.Arccosecant);
        }

        [Fact]
        public void Arccsc()
        {
            FuncTest("arccsc", Functions.Arccosecant);
        }

        [Fact]
        public void Arccos()
        {
            FuncTest("arccos", Functions.Arccosine);
        }

        [Fact]
        public void Arctg()
        {
            FuncTest("arctg", Functions.Arctangent);
        }

        [Fact]
        public void Arctan()
        {
            FuncTest("arctan", Functions.Arctangent);
        }

        [Fact]
        public void Arcctg()
        {
            FuncTest("arcctg", Functions.Arccotangent);
        }

        [Fact]
        public void Arccot()
        {
            FuncTest("arccot", Functions.Arccotangent);
        }

        [Fact]
        public void Arcsec()
        {
            FuncTest("arcsec", Functions.Arcsecant);
        }

        [Fact]
        public void Sqrt()
        {
            FuncTest("sqrt", Functions.Sqrt);
        }

        [Fact]
        public void Round()
        {
            FuncTest("round", Functions.Round);
        }

        [Fact]
        public void Ceil()
        {
            FuncTest("ceil", Functions.Ceil);
        }

        [Fact]
        public void Floor()
        {
            FuncTest("floor", Functions.Floor);
        }

        [Fact]
        public void Root()
        {
            var tokens = lexer.Tokenize("root(27, 3)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Root, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(27),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Lg()
        {
            FuncTest("lg", Functions.Lg);
        }

        [Fact]
        public void Ln()
        {
            FuncTest("ln", Functions.Ln);
        }

        [Fact]
        public void Lb()
        {
            FuncTest("lb", Functions.Lb);
        }

        [Fact]
        public void Log2()
        {
            FuncTest("log2", Functions.Lb);
        }

        [Fact]
        public void Log()
        {
            var tokens = lexer.Tokenize("log(2, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Log, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Deriv()
        {
            var tokens = lexer.Tokenize("deriv(sin(x), x)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Derivative, 2),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotAsWord()
        {
            var tokens = lexer.Tokenize("~2");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.Not),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AndAsWord()
        {
            var tokens = lexer.Tokenize("1 and 2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.And),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void OrAsWord()
        {
            var tokens = lexer.Tokenize("1 or 2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.Or),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void XOrAsWord()
        {
            var tokens = lexer.Tokenize("1 xor 2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.XOr),
                new NumberToken(2)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImplicationAsWordTest()
        {
            var tokens = lexer.Tokenize("true impl false");

            var expected = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.Implication),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualityAsWordTest()
        {
            var tokens = lexer.Tokenize("true eq false");

            var expected = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.Equality),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NOrAsWordTest()
        {
            var tokens = lexer.Tokenize("true nor false");

            var expected = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.NOr),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NAndAsWordTest()
        {
            var tokens = lexer.Tokenize("true nand false");

            var expected = new List<IToken>
            {
                new BooleanToken(true),
                new OperationToken(Operations.NAnd),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void Var()
        {
            var tokens = lexer.Tokenize("x * y");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Multiplication),
                new VariableToken("y")
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BitVar()
        {
            var tokens = lexer.Tokenize("x and x");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.And),
                new VariableToken("x")
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void StringVarLexerTest()
        {
            var tokens = lexer.Tokenize("aaa := 1");

            var expected = new List<IToken>()
            {
                new VariableToken("aaa"),
                new OperationToken(Operations.Assign),
                new NumberToken(1)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void StringVarAnd()
        {
            var tokens = lexer.Tokenize("func and 1");

            var expected = new List<IToken>()
            {
                new VariableToken("func"),
                new OperationToken(Operations.And),
                new NumberToken(1)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFunc()
        {
            var tokens = lexer.Tokenize("func(x)");

            var expected = new List<IToken>()
            {
                new UserFunctionToken("func", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncTwoVars()
        {
            var tokens = lexer.Tokenize("func(x, y)");

            var expected = new List<IToken>()
            {
                new UserFunctionToken("func", 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new VariableToken("y"),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncInUserFuncTwo()
        {
            var tokens = lexer.Tokenize("func(x, sin(x))");

            var expected = new List<IToken>()
            {
                new UserFunctionToken("func", 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                 new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };
            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncInUserFunc()
        {
            var tokens = lexer.Tokenize("f(x, g(y))");

            var expected = new List<IToken>()
            {
                new UserFunctionToken("f", 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new UserFunctionToken("g", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("y"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UndefFunc()
        {
            var tokens = lexer.Tokenize("undef(f(x))");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Undefine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new UserFunctionToken("f", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotBalancedOpen()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("sin(2("));
        }

        [Fact]
        public void NotBalancedClose()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("sin)2)"));
        }

        [Fact]
        public void NotBalancedFirstClose()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("sin)2("));
        }

        [Fact]
        public void NotBalancedBracesOpen()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("{2,1"));
        }

        [Fact]
        public void NotBalancedBracesClose()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("}2,1"));
        }

        [Fact]
        public void HexTest()
        {
            var tokens = lexer.Tokenize("0xFF00");

            var expected = new List<IToken>()
            {
                new NumberToken(65280)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void OctTest()
        {
            var tokens = lexer.Tokenize("0436");

            var expected = new List<IToken>()
            {
                new NumberToken(286)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BinTest()
        {
            var tokens = lexer.Tokenize("0b01100110");

            var expected = new List<IToken>()
            {
                new NumberToken(102)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ZeroSubTwoTest()
        {
            var tokens = lexer.Tokenize("0-2");

            var expected = new List<IToken>()
            {
                new NumberToken(0),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GCDTest()
        {
            var tokens = lexer.Tokenize("gcd(12, 16)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.GCD, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GCFTest()
        {
            var tokens = lexer.Tokenize("gcf(12, 16)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.GCD, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HCFTest()
        {
            var tokens = lexer.Tokenize("hcf(12, 16)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.GCD, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void LCMTest()
        {
            var tokens = lexer.Tokenize("lcm(12, 16)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.LCM, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(12),
                new SymbolToken(Symbols.Comma),
                new NumberToken(16),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SimplifyTest()
        {
            var tokens = lexer.Tokenize("simplify(x)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Simplify, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialTest()
        {
            var tokens = lexer.Tokenize("fact(4)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Factorial, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialOperatorTest()
        {
            var tokens = lexer.Tokenize("4!");

            var expected = new List<IToken>()
            {
                new NumberToken(4),
                new OperationToken(Operations.Factorial)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialVarOperatorTest()
        {
            var tokens = lexer.Tokenize("x!");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Factorial)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void FactorialOperatorFailTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("!4"));
        }

        [Fact]
        public void RootInRootTest()
        {
            var tokens = lexer.Tokenize("root(1 + root(2, x), 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Root, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new OperationToken(Operations.Addition),
                new FunctionToken(Functions.Root, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BracketsForAllParamsTest()
        {
            var tokens = lexer.Tokenize("(3)cos((u))cos((v))");

            var expected = new List<IToken>()
            {
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBracket),
                new FunctionToken(Functions.Cosine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("u"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket),
                new FunctionToken(Functions.Cosine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("v"),
                new SymbolToken(Symbols.CloseBracket),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ZeroTest()
        {
            var tokens = lexer.Tokenize("0");

            var expected = new List<IToken>()
            {
                new NumberToken(0)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HexErrorTest()
        {
            var tokens = lexer.Tokenize("0x");

            var expected = new List<IToken>()
            {
                new NumberToken(0),
                new OperationToken(Operations.Multiplication),
                new VariableToken("x")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BinErrorTest()
        {
            var tokens = lexer.Tokenize("0b");

            var expected = new List<IToken>()
            {
                new NumberToken(0),
                new OperationToken(Operations.Multiplication),
                new VariableToken("b")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumToTest()
        {
            var tokens = lexer.Tokenize("sum(x, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumFromToTest()
        {
            var tokens = lexer.Tokenize("sum(x, 2, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumFromToIncTest()
        {
            var tokens = lexer.Tokenize("sum(x, 2, 20, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 4),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SumFromToIncVarTest()
        {
            var tokens = lexer.Tokenize("sum(k, 2, 20, 2, k)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 5),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("k"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("k"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductToTest()
        {
            var tokens = lexer.Tokenize("product(x, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductFromToTest()
        {
            var tokens = lexer.Tokenize("product(x, 2, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductFromToIncTest()
        {
            var tokens = lexer.Tokenize("product(x, 2, 20, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 4),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ProductFromToIncVarTest()
        {
            var tokens = lexer.Tokenize("product(k, 2, 20, 2, k)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 5),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("k"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new VariableToken("k"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VectorBracesTest()
        {
            var tokens = lexer.Tokenize("vector{2, 3, 4}");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Vector, 3),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.Comma),
                new NumberToken(4),
                new SymbolToken(Symbols.CloseBrace)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MatrixAndVectorTest()
        {
            var tokens = lexer.Tokenize("matrix{vector{2, 3}, vector{4, 7}}");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBrace),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBrace)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void TransposeTest()
        {
            var tokens = lexer.Tokenize("transpose(2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Transpose, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulMatrixTest()
        {
            var tokens = lexer.Tokenize("matrix{vector{3}, vector{-1}} * vector{-2, 1}");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBrace),
                new FunctionToken(Functions.Vector, 1),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 1),
                new SymbolToken(Symbols.OpenBrace),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBrace),
                new OperationToken(Operations.Multiplication),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBrace)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void PlusVectorTest()
        {
            var tokens = lexer.Tokenize("vector{+3}");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Vector, 1),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBrace)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ShortVectorTest()
        {
            var tokens = lexer.Tokenize("{4, 7}");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBrace)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ShortMatrixTest()
        {
            var tokens = lexer.Tokenize("{{2, 3}, {4, 7}}");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBrace),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBrace)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DeterminantTest()
        {
            var tokens = lexer.Tokenize("determinant({{2, 3}, {4, 7}})");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Determinant, 1),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBrace),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DetTest()
        {
            var tokens = lexer.Tokenize("det({{2, 3}, {4, 7}})");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Determinant, 1),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Matrix, 2),
                new SymbolToken(Symbols.OpenBrace),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(3),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.Comma),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void InverseTest()
        {
            var tokens = lexer.Tokenize("inverse({4, 7})");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Inverse, 1),
                new SymbolToken(Symbols.OpenBracket),
                new FunctionToken(Functions.Vector, 2),
                new SymbolToken(Symbols.OpenBrace),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(7),
                new SymbolToken(Symbols.CloseBrace),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void IfTest()
        {
            var tokens = lexer.Tokenize("if(z, x ^ 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.If, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("z"),
                new SymbolToken(Symbols.Comma),
                new VariableToken("x"),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void IfElseNegativeTest()
        {
            var tokens = lexer.Tokenize("if(True, 1, -1)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.If, 3),
                new SymbolToken(Symbols.OpenBracket),
                new BooleanToken(true),
                new SymbolToken(Symbols.Comma),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ForTest()
        {
            var tokens = lexer.Tokenize("for(z := z + 1)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.For, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("z"),
                new OperationToken(Operations.Assign),
                new VariableToken("z"),
                new OperationToken(Operations.Addition),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void WhileTest()
        {
            var tokens = lexer.Tokenize("while(z := z + 1)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.While, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("z"),
                new OperationToken(Operations.Assign),
                new VariableToken("z"),
                new OperationToken(Operations.Addition),
                new NumberToken(1),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConditionalAndTest()
        {
            var tokens = lexer.Tokenize("x && y");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.ConditionalAnd),
                new VariableToken("y")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConditionalOrTest()
        {
            var tokens = lexer.Tokenize("x || y");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.ConditionalOr),
                new VariableToken("y")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void LessThenTest()
        {
            var tokens = lexer.Tokenize("x < 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessThan),
                new NumberToken(10)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void LessOrEqualTest()
        {
            var tokens = lexer.Tokenize("x <= 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessOrEqual),
                new NumberToken(10)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GreaterThenTest()
        {
            var tokens = lexer.Tokenize("x > 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterThan),
                new NumberToken(10)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void GreaterOrEqualTest()
        {
            var tokens = lexer.Tokenize("x >= 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterOrEqual),
                new NumberToken(10)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void IncTest()
        {
            var tokens = lexer.Tokenize("x++");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Increment)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DecTest()
        {
            var tokens = lexer.Tokenize("x--");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Decrement)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddAssign()
        {
            var tokens = lexer.Tokenize("x += 2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.AddAssign),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubAssign()
        {
            var tokens = lexer.Tokenize("x -= 2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.SubAssign),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulAssign()
        {
            var tokens = lexer.Tokenize("x *= 2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.MulAssign),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DivAssign()
        {
            var tokens = lexer.Tokenize("x /= 2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.DivAssign),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BoolConstLongTest()
        {
            var tokens = lexer.Tokenize("true & false");

            var expected = new List<IToken>()
            {
                new BooleanToken(true),
                new OperationToken(Operations.And),
                new BooleanToken(false)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void EqualTest()
        {
            var tokens = lexer.Tokenize("1 == 1");

            var expected = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.Equal),
                new NumberToken(1)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotEqualTest()
        {
            var tokens = lexer.Tokenize("1 != 1");

            var expected = new List<IToken>
            {
                new NumberToken(1),
                new OperationToken(Operations.NotEqual),
                new NumberToken(1)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NumberFormatTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("0."));
        }

        [Fact]
        public void TabTest()
        {
            var tokens = lexer.Tokenize("\t2 + 2");

            var expected = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NewlineTest()
        {
            var tokens = lexer.Tokenize("\n2 + 2");

            var expected = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void CRTest()
        {
            var tokens = lexer.Tokenize("\r2 + 2");

            var expected = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberTest()
        {
            var tokens = lexer.Tokenize("3 + 2i");

            var expected = new List<IToken>
            {
                new NumberToken(3),
                new OperationToken(Operations.Addition),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberNegativeTest()
        {
            var tokens = lexer.Tokenize("3 - 2i");

            var expected = new List<IToken>
            {
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexNumberNegativeAllPartsTest()
        {
            var tokens = lexer.Tokenize("-3 - 2i");

            var expected = new List<IToken>
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyRePartTest()
        {
            var tokens = lexer.Tokenize("3 + 0i");

            var expected = new List<IToken>
            {
                new NumberToken(3),
                new OperationToken(Operations.Addition),
                new NumberToken(0),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyImPartTest()
        {
            var tokens = lexer.Tokenize("2i");

            var expected = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyImPartNegativeTest()
        {
            var tokens = lexer.Tokenize("-2i");

            var expected = new List<IToken>
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexOnlyITest()
        {
            var tokens = lexer.Tokenize("i");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexEmptyImPartTest()
        {
            var tokens = lexer.Tokenize("2 + i");

            var expected = new List<IToken>
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexWithVarTest1()
        {
            var tokens = lexer.Tokenize("x - 2i");

            var expected = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexWithVarTest2()
        {
            var tokens = lexer.Tokenize("x + 3 - 2i");

            var expected = new List<IToken>
            {
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexFromPolarTest()
        {
            var tokens = lexer.Tokenize("10+45°");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(10, 45 * Math.PI / 180))
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImTest()
        {
            var tokens = lexer.Tokenize("im(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Im, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ImaginaryTest()
        {
            var tokens = lexer.Tokenize("imaginary(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Im, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ReTest()
        {
            var tokens = lexer.Tokenize("re(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Re, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void RealTest()
        {
            var tokens = lexer.Tokenize("real(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Re, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void PhaseTest()
        {
            var tokens = lexer.Tokenize("phase(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Phase, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ConjugateTest()
        {
            var tokens = lexer.Tokenize("conjugate(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Conjugate, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ReciprocalTest()
        {
            var tokens = lexer.Tokenize("reciprocal(3 - 2i)");

            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Reciprocal, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPowNumberTest()
        {
            var tokens = lexer.Tokenize("3 - 2i ^ 2");

            var expected = new List<IToken>
            {
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexIPowNumberTest()
        {
            var tokens = lexer.Tokenize("i ^ 2");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(new Complex(0, 1)),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPowComplexTest()
        {
            var tokens = lexer.Tokenize("3 - 2i ^ 2 + 3i");

            var expected = new List<IToken>
            {
                new NumberToken(3),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(3),
                new OperationToken(Operations.Multiplication),
                new ComplexNumberToken(Complex.ImaginaryOne)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarPosPosTest()
        {
            var tokens = lexer.Tokenize("2.3 + 7.1°");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(2.3, 7.1 * Math.PI / 180))
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarPosNegTest()
        {
            var tokens = lexer.Tokenize("2.3 - 7.1°");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(2.3, -7.1 * Math.PI / 180))
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarNegNegTest()
        {
            var tokens = lexer.Tokenize("-2.3 - 7.1°");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(-2.3, -7.1 * Math.PI / 180))
            };

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

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(2.3, 0))
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarNegMagnitudeTest()
        {
            var tokens = lexer.Tokenize("-2.3 + 0°");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(-2.3, 0))
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarTest()
        {
            var tokens = lexer.Tokenize("10 ∠ 45°");

            var expected = new List<IToken>
            {
                new ComplexNumberToken(Complex.FromPolarCoordinates(10, 45 * Math.PI / 180))
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ComplexPolarInvalidTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("x°"));
        }

        [Fact]
        public void ModuloTest()
        {
            var tokens = lexer.Tokenize("7 % 2");

            var expected = new List<IToken>
            {
                new NumberToken(7),
                new OperationToken(Operations.Modulo),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void ModuloAsFuncTest()
        {
            var tokens = lexer.Tokenize("7 mod 2");

            var expected = new List<IToken>
            {
                new NumberToken(7),
                new OperationToken(Operations.Modulo),
                new NumberToken(2)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MinTest()
        {
            var tokens = lexer.Tokenize("min(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Min, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MaxTest()
        {
            var tokens = lexer.Tokenize("max(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Max, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AvgTest()
        {
            var tokens = lexer.Tokenize("avg(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Avg, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void CountTest()
        {
            var tokens = lexer.Tokenize("count(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Count, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarTest()
        {
            var tokens = lexer.Tokenize("var(4, 9)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Var, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarpTest()
        {
            var tokens = lexer.Tokenize("varp(4, 9)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Varp, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void StdevTest()
        {
            var tokens = lexer.Tokenize("stdev(4, 9)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Stdev, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void StdevpTest()
        {
            var tokens = lexer.Tokenize("stdevp(4, 9)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Stdevp, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.Comma),
                new NumberToken(9),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DelTest()
        {
            var tokens = lexer.Tokenize("del(2x + 3y + 4z)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Del, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(3),
                new OperationToken(Operations.Multiplication),
                new VariableToken("y"),
                new OperationToken(Operations.Addition),
                new NumberToken(4),
                new OperationToken(Operations.Multiplication),
                new VariableToken("z"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NablaTest()
        {
            var tokens = lexer.Tokenize("nabla(2x + 3y + 4z)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Del, 1),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new VariableToken("x"),
                new OperationToken(Operations.Addition),
                new NumberToken(3),
                new OperationToken(Operations.Multiplication),
                new VariableToken("y"),
                new OperationToken(Operations.Addition),
                new NumberToken(4),
                new OperationToken(Operations.Multiplication),
                new VariableToken("z"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void AddTest()
        {
            var tokens = lexer.Tokenize("add(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Add, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SubTest()
        {
            var tokens = lexer.Tokenize("sub(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Sub, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void MulTest()
        {
            var tokens = lexer.Tokenize("mul(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Mul, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void DivTest()
        {
            var tokens = lexer.Tokenize("div(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Div, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void PowTest()
        {
            var tokens = lexer.Tokenize("pow(1, 2)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Pow, 2),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(1),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotEnoughParamsTest()
        {
            Assert.Throws<LexerException>(() => lexer.Tokenize("deriv(x,)"));
        }

        [Fact]
        public void UseGreekLettersTest()
        {
            var tokens = lexer.Tokenize("4 * φ");
            var expected = new List<IToken>
            {
                new NumberToken(4),
                new OperationToken(Operations.Multiplication),
                new VariableToken("φ")
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void NotOperationCaseInsensitive()
        {
            var tokens = lexer.Tokenize("nOt(4)");
            var expected = new List<IToken>
            {
                new OperationToken(Operations.Not),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(4),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void HexNumberCaseInsensitive()
        {
            var tokens = lexer.Tokenize("0XFF");
            var expected = new List<IToken>
            {
                new NumberToken(0xFF)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void BinNumberCaseInsensitive()
        {
            var tokens = lexer.Tokenize("0b1001");
            var expected = new List<IToken>
            {
                new NumberToken(0b1001)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void TrueConstCaseInsensitive()
        {
            var tokens = lexer.Tokenize("tRuE");
            var expected = new List<IToken>
            {
                new BooleanToken(true)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void SinCaseInsensitive()
        {
            var tokens = lexer.Tokenize("sIn(x)");
            var expected = new List<IToken>
            {
                new FunctionToken(Functions.Sine, 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void UserFuncCaseSensitive()
        {
            var tokens = lexer.Tokenize("caSe(x)");
            var expected = new List<IToken>
            {
                new UserFunctionToken("caSe", 1),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("x"),
                new SymbolToken(Symbols.CloseBracket)
            };

            Assert.Equal(expected, tokens.ToList());
        }

        [Fact]
        public void VarCaseSensitive()
        {
            var tokens = lexer.Tokenize("caseSensitive");
            var expected = new List<IToken>
            {
                new VariableToken("caseSensitive")
            };

            Assert.Equal(expected, tokens.ToList());
        }

    }

}
