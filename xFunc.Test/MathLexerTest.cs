using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Tokens;

namespace xFunc.Test
{

    [TestClass]
    public class MathLexerTest
    {

        private ILexer lexer;

        [TestInitialize]
        public void TestInit()
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullString()
        {
            lexer.Tokenize(null);
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotSupportedSymbol()
        {
            var tokens = lexer.Tokenize("@");
        }

        [TestMethod]
        public void Brackets()
        {
            var tokens = lexer.Tokenize("(2)");

            var expected = new List<IToken>()
            {
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Add()
        {
            var tokens = lexer.Tokenize("2 + 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Addition),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void AddAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(+2)");

            var expected = new List<IToken>()
            {
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void AddBeforeNumber()
        {
            var tokens = lexer.Tokenize("+2");

            var expected = new List<IToken>()
            {
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Sub()
        {
            var tokens = lexer.Tokenize("2 - 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void SubBeforeNumber()
        {
            var tokens = lexer.Tokenize("-2");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.UnaryMinus),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Mul()
        {
            var tokens = lexer.Tokenize("2 * 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Multiplication),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Div()
        {
            var tokens = lexer.Tokenize("2 / 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Division),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Inv()
        {
            var tokens = lexer.Tokenize("2 ^ 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.Exponentiation),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Not()
        {
            var tokens = lexer.Tokenize("not(2)");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.BitwiseNot),
                new SymbolToken(Symbols.OpenBracket),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NotAsOperator()
        {
            var tokens = lexer.Tokenize("~2");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.BitwiseNot),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotAsOperatorFail()
        {
            var tokens = lexer.Tokenize("2~");
        }

        [TestMethod]
        public void And()
        {
            var tokens = lexer.Tokenize("2 & 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.BitwiseAnd),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Or()
        {
            var tokens = lexer.Tokenize("2 | 2");

            var expected = new List<IToken>()
            {
                new NumberToken(2),
                new OperationToken(Operations.BitwiseOr),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Assign()
        {
            var tokens = lexer.Tokenize("x := 2");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Assign),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Pi()
        {
            var tokens = lexer.Tokenize("3pi");

            var expected = new List<IToken>()
            {
                new NumberToken(3),
                new OperationToken(Operations.Multiplication),
                new VariableToken("π")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Exp()
        {
            FuncTest("exp", Functions.Exp);
        }

        [TestMethod]
        public void Abs()
        {
            FuncTest("abs", Functions.Absolute);
        }

        [TestMethod]
        public void Sh()
        {
            FuncTest("sh", Functions.Sineh);
        }

        [TestMethod]
        public void Sinh()
        {
            FuncTest("sinh", Functions.Sineh);
        }

        [TestMethod]
        public void Ch()
        {
            FuncTest("ch", Functions.Cosineh);
        }

        [TestMethod]
        public void Cosh()
        {
            FuncTest("cosh", Functions.Cosineh);
        }

        [TestMethod]
        public void Th()
        {
            FuncTest("th", Functions.Tangenth);
        }

        [TestMethod]
        public void Tanh()
        {
            FuncTest("tanh", Functions.Tangenth);
        }

        [TestMethod]
        public void Cth()
        {
            FuncTest("cth", Functions.Cotangenth);
        }

        [TestMethod]
        public void Coth()
        {
            FuncTest("coth", Functions.Cotangenth);
        }

        [TestMethod]
        public void Sech()
        {
            FuncTest("sech", Functions.Secanth);
        }

        [TestMethod]
        public void Csch()
        {
            FuncTest("csch", Functions.Cosecanth);
        }

        [TestMethod]
        public void Arsinh()
        {
            FuncTest("arsinh", Functions.Arsineh);
        }

        [TestMethod]
        public void Arsh()
        {
            FuncTest("arsh", Functions.Arsineh);
        }

        [TestMethod]
        public void Arcosh()
        {
            FuncTest("arcosh", Functions.Arcosineh);
        }

        [TestMethod]
        public void Arch()
        {
            FuncTest("arch", Functions.Arcosineh);
        }

        [TestMethod]
        public void Artanh()
        {
            FuncTest("artanh", Functions.Artangenth);
        }

        [TestMethod]
        public void Arth()
        {
            FuncTest("arth", Functions.Artangenth);
        }

        [TestMethod]
        public void Arcoth()
        {
            FuncTest("arcoth", Functions.Arcotangenth);
        }

        [TestMethod]
        public void Arcth()
        {
            FuncTest("arcth", Functions.Arcotangenth);
        }

        [TestMethod]
        public void Arsech()
        {
            FuncTest("arsech", Functions.Arsecanth);
        }

        [TestMethod]
        public void Arsch()
        {
            FuncTest("arsch", Functions.Arsecanth);
        }

        [TestMethod]
        public void Arcsch()
        {
            FuncTest("arcsch", Functions.Arcosecanth);
        }

        [TestMethod]
        public void Sin()
        {
            FuncTest("sin", Functions.Sine);
        }

        [TestMethod]
        public void Cosec()
        {
            FuncTest("cosec", Functions.Cosecant);
        }

        [TestMethod]
        public void Csc()
        {
            FuncTest("csc", Functions.Cosecant);
        }

        [TestMethod]
        public void Cos()
        {
            FuncTest("cos", Functions.Cosine);
        }

        [TestMethod]
        public void Tg()
        {
            FuncTest("tg", Functions.Tangent);
        }

        [TestMethod]
        public void Tan()
        {
            FuncTest("tan", Functions.Tangent);
        }

        [TestMethod]
        public void Ctg()
        {
            FuncTest("ctg", Functions.Cotangent);
        }

        [TestMethod]
        public void Cot()
        {
            FuncTest("cot", Functions.Cotangent);
        }

        [TestMethod]
        public void Sec()
        {
            FuncTest("sec", Functions.Secant);
        }

        [TestMethod]
        public void Arcsin()
        {
            FuncTest("arcsin", Functions.Arcsine);
        }

        [TestMethod]
        public void Arccosec()
        {
            FuncTest("arccosec", Functions.Arccosecant);
        }

        [TestMethod]
        public void Arccsc()
        {
            FuncTest("arccsc", Functions.Arccosecant);
        }

        [TestMethod]
        public void Arccos()
        {
            FuncTest("arccos", Functions.Arccosine);
        }

        [TestMethod]
        public void Arctg()
        {
            FuncTest("arctg", Functions.Arctangent);
        }

        [TestMethod]
        public void Arctan()
        {
            FuncTest("arctan", Functions.Arctangent);
        }

        [TestMethod]
        public void Arcctg()
        {
            FuncTest("arcctg", Functions.Arccotangent);
        }

        [TestMethod]
        public void Arccot()
        {
            FuncTest("arccot", Functions.Arccotangent);
        }

        [TestMethod]
        public void Arcsec()
        {
            FuncTest("arcsec", Functions.Arcsecant);
        }

        [TestMethod]
        public void Sqrt()
        {
            FuncTest("sqrt", Functions.Sqrt);
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Lg()
        {
            FuncTest("lg", Functions.Lg);
        }

        [TestMethod]
        public void Ln()
        {
            FuncTest("ln", Functions.Ln);
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NotAsWord()
        {
            var tokens = lexer.Tokenize("~2");

            var expected = new List<IToken>()
            {
                new OperationToken(Operations.BitwiseNot),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void AndAsWord()
        {
            var tokens = lexer.Tokenize("1 and 2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.BitwiseAnd),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void OrAsWord()
        {
            var tokens = lexer.Tokenize("1 or 2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.BitwiseOr),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void XOrAsWord()
        {
            var tokens = lexer.Tokenize("1 xor 2");

            var expected = new List<IToken>()
            {
                new NumberToken(1),
                new OperationToken(Operations.BitwiseXOr),
                new NumberToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Var()
        {
            var tokens = lexer.Tokenize("x * y");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.Multiplication),
                new VariableToken("y")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void BitVar()
        {
            var tokens = lexer.Tokenize("x and x");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.BitwiseAnd),
                new VariableToken("x")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void StringVarLexerTest()
        {
            var tokens = lexer.Tokenize("aaa := 1");

            var expected = new List<IToken>()
            {
                new VariableToken("aaa"),
                new OperationToken(Operations.Assign),
                new NumberToken(1)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void StringVarAnd()
        {
            var tokens = lexer.Tokenize("func and 1");

            var expected = new List<IToken>()
            {
                new VariableToken("func"),
                new OperationToken(Operations.BitwiseAnd),
                new NumberToken(1)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotBalancedOpen()
        {
            var tokens = lexer.Tokenize("sin(2(");
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotBalancedClose()
        {
            var tokens = lexer.Tokenize("sin)2)");
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotBalancedFirstClose()
        {
            var tokens = lexer.Tokenize("sin)2(");
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotBalancedBracesOpen()
        {
            var tokens = lexer.Tokenize("{2,1");
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void NotBalancedBracesClose()
        {
            var tokens = lexer.Tokenize("}2,1");
        }

        [TestMethod]
        public void HexTest()
        {
            var tokens = lexer.Tokenize("0xFF00");

            var expected = new List<IToken>()
            {
                new NumberToken(65280)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void OctTest()
        {
            var tokens = lexer.Tokenize("0436");

            var expected = new List<IToken>()
            {
                new NumberToken(286)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void BinTest()
        {
            var tokens = lexer.Tokenize("0b01100110");

            var expected = new List<IToken>()
            {
                new NumberToken(102)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ZeroSubTwoTest()
        {
            var tokens = lexer.Tokenize("0-2");

            var expected = new List<IToken>()
            {
                new NumberToken(0),
                new OperationToken(Operations.Subtraction),
                new NumberToken(2)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BinError1()
        {
            var tokens = lexer.Tokenize("0b*01100110");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BinError2()
        {
            var tokens = lexer.Tokenize("0b-01100110");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void HexError1()
        {
            var tokens = lexer.Tokenize("0x*FF00");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HexError2()
        {
            var tokens = lexer.Tokenize("0x-FF00");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void HexError3()
        {
            var tokens = lexer.Tokenize("0xJFF00");
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void FactorialOperatorTest()
        {
            var tokens = lexer.Tokenize("4!");

            var expected = new List<IToken>()
            {
                new NumberToken(4),
                new OperationToken(Operations.Factorial)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void FactorialOperatorFailTest()
        {
            lexer.Tokenize("!4");
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ZeroTest()
        {
            var tokens = lexer.Tokenize("0");

            var expected = new List<IToken>()
            {
                new NumberToken(0)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void HexErrorTest()
        {
            var tokens = lexer.Tokenize("0x");

            var expected = new List<IToken>()
            {
                new NumberToken(0),
                new OperationToken(Operations.Multiplication),
                new VariableToken("x")
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void BinErrorTest()
        {
            var tokens = lexer.Tokenize("0b");

            var expected = new List<IToken>()
            {
                new NumberToken(0),
                new OperationToken(Operations.Multiplication),
                new VariableToken("b")
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void SumToTest()
        {
            var tokens = lexer.Tokenize("sum(i, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void SumFromToTest()
        {
            var tokens = lexer.Tokenize("sum(i, 2, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void SumFromToIncTest()
        {
            var tokens = lexer.Tokenize("sum(i, 2, 20, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Sum, 4),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ProductToTest()
        {
            var tokens = lexer.Tokenize("product(i, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 2),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ProductFromToTest()
        {
            var tokens = lexer.Tokenize("product(i, 2, 20)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 3),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.CloseBracket)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ProductFromToIncTest()
        {
            var tokens = lexer.Tokenize("product(i, 2, 20, 2)");

            var expected = new List<IToken>()
            {
                new FunctionToken(Functions.Product, 4),
                new SymbolToken(Symbols.OpenBracket),
                new VariableToken("i"),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.Comma),
                new NumberToken(20),
                new SymbolToken(Symbols.Comma),
                new NumberToken(2),
                new SymbolToken(Symbols.CloseBracket)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
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

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ConditionalAndTest()
        {
            var tokens = lexer.Tokenize("x && y");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.ConditionalAnd),
                new VariableToken("y")
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ConditionalOrTest()
        {
            var tokens = lexer.Tokenize("x || y");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.ConditionalOr),
                new VariableToken("y")
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void LessThenTest()
        {
            var tokens = lexer.Tokenize("x < 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessThen),
                new NumberToken(10)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void LessOrEqualTest()
        {
            var tokens = lexer.Tokenize("x <= 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.LessOrEqual),
                new NumberToken(10)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void GreaterThenTest()
        {
            var tokens = lexer.Tokenize("x > 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterThen),
                new NumberToken(10)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void GreaterOrEqualTest()
        {
            var tokens = lexer.Tokenize("x >= 10");

            var expected = new List<IToken>()
            {
                new VariableToken("x"),
                new OperationToken(Operations.GreaterOrEqual),
                new NumberToken(10)
            };

            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

    }

}
