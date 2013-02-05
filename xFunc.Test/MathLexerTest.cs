using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths;
using xFunc.Maths.Exceptions;

namespace xFunc.Test
{

    [TestClass]
    public class MathLexerTest
    {

        private ILexer lexer;

        [TestInitialize]
        public void TestInit()
        {
            lexer = new MathLexer();
        }

        private void FuncTest(string func, MathTokenType type)
        {
            var tokens = lexer.Tokenize(func + "(3)");

            var expected = new List<MathToken>()
            {
                new MathToken(type),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(3),
                new MathToken(MathTokenType.CloseBracket)
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
        [ExpectedException(typeof(MathLexerException))]
        public void NotSupportedSymbol()
        {
            var tokens = lexer.Tokenize("@");
        }

        [TestMethod]
        public void Brackets()
        {
            var tokens = lexer.Tokenize("(2)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(2),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Add()
        {
            var tokens = lexer.Tokenize("2 + 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.Addition),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void AddAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(+2)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(2),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void AddBeforeNumber()
        {
            var tokens = lexer.Tokenize("+2");

            var expected = new List<MathToken>()
            {
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Sub()
        {
            var tokens = lexer.Tokenize("2 - 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.Subtraction),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void SubAfterOpenBracket()
        {
            var tokens = lexer.Tokenize("(-2)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(MathTokenType.UnaryMinus),
                new MathToken(2),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void SubBeforeNumber()
        {
            var tokens = lexer.Tokenize("-2");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.UnaryMinus),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Mul()
        {
            var tokens = lexer.Tokenize("2 * 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.Multiplication),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Div()
        {
            var tokens = lexer.Tokenize("2 / 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.Division),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Inv()
        {
            var tokens = lexer.Tokenize("2 ^ 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.Exponentiation),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Comma()
        {
            var tokens = lexer.Tokenize("log(2, 2)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.Log),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(2),
                new MathToken(MathTokenType.Comma),
                new MathToken(2),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Not()
        {
            var tokens = lexer.Tokenize("~2");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.Not),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void And()
        {
            var tokens = lexer.Tokenize("2 & 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.And),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Or()
        {
            var tokens = lexer.Tokenize("2 | 2");

            var expected = new List<MathToken>()
            {
                new MathToken(2),
                new MathToken(MathTokenType.Or),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Assign()
        {
            var tokens = lexer.Tokenize("x := 2");

            var expected = new List<MathToken>()
            {
                new MathToken('x'),
                new MathToken(MathTokenType.Assign),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Integer()
        {
            var tokens = lexer.Tokenize("-2764786 + 46489879");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.UnaryMinus),
                new MathToken(2764786),
                new MathToken(MathTokenType.Addition),
                new MathToken(46489879)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Double()
        {
            var tokens = lexer.Tokenize("-45.3 + 87.64");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.UnaryMinus),
                new MathToken(45.3),
                new MathToken(MathTokenType.Addition),
                new MathToken(87.64)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NumAndVar()
        {
            var tokens = lexer.Tokenize("-2x");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.UnaryMinus),
                new MathToken(2),
                new MathToken(MathTokenType.Multiplication),
                new MathToken('x')
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Pi()
        {
            var tokens = lexer.Tokenize("3pi");

            var expected = new List<MathToken>()
            {
                new MathToken(3),
                new MathToken(MathTokenType.Multiplication),
                new MathToken('π')
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Exp()
        {
            FuncTest("exp", MathTokenType.E);
        }

        [TestMethod]
        public void Abs()
        {
            FuncTest("abs", MathTokenType.Absolute);
        }

        [TestMethod]
        public void Sh()
        {
            FuncTest("sh", MathTokenType.Sineh);
        }

        [TestMethod]
        public void Sinh()
        {
            FuncTest("sinh", MathTokenType.Sineh);
        }

        [TestMethod]
        public void Ch()
        {
            FuncTest("ch", MathTokenType.Cosineh);
        }

        [TestMethod]
        public void Cosh()
        {
            FuncTest("cosh", MathTokenType.Cosineh);
        }

        [TestMethod]
        public void Th()
        {
            FuncTest("th", MathTokenType.Tangenth);
        }

        [TestMethod]
        public void Tanh()
        {
            FuncTest("tanh", MathTokenType.Tangenth);
        }

        [TestMethod]
        public void Cth()
        {
            FuncTest("cth", MathTokenType.Cotangenth);
        }

        [TestMethod]
        public void Coth()
        {
            FuncTest("coth", MathTokenType.Cotangenth);
        }

        [TestMethod]
        public void Sech()
        {
            FuncTest("sech", MathTokenType.Secanth);
        }

        [TestMethod]
        public void Csch()
        {
            FuncTest("csch", MathTokenType.Cosecanth);
        }

        [TestMethod]
        public void Arsinh()
        {
            FuncTest("arsinh", MathTokenType.Arsineh);
        }

        [TestMethod]
        public void Arcosh()
        {
            FuncTest("arcosh", MathTokenType.Arcosineh);
        }

        [TestMethod]
        public void Artanh()
        {
            FuncTest("artanh", MathTokenType.Artangenth);
        }

        [TestMethod]
        public void Arcoth()
        {
            FuncTest("arcoth", MathTokenType.Arcotangenth);
        }

        [TestMethod]
        public void Arsech()
        {
            FuncTest("arsech", MathTokenType.Arsecanth);
        }

        [TestMethod]
        public void Arcsch()
        {
            FuncTest("arcsch", MathTokenType.Arcosecanth);
        }

        [TestMethod]
        public void Sin()
        {
            FuncTest("sin", MathTokenType.Sine);
        }

        [TestMethod]
        public void Cosec()
        {
            FuncTest("cosec", MathTokenType.Cosecant);
        }

        [TestMethod]
        public void Csc()
        {
            FuncTest("csc", MathTokenType.Cosecant);
        }

        [TestMethod]
        public void Cos()
        {
            FuncTest("cos", MathTokenType.Cosine);
        }

        [TestMethod]
        public void Tg()
        {
            FuncTest("tg", MathTokenType.Tangent);
        }

        [TestMethod]
        public void Tan()
        {
            FuncTest("tan", MathTokenType.Tangent);
        }

        [TestMethod]
        public void Ctg()
        {
            FuncTest("ctg", MathTokenType.Cotangent);
        }

        [TestMethod]
        public void Cot()
        {
            FuncTest("cot", MathTokenType.Cotangent);
        }

        [TestMethod]
        public void Sec()
        {
            FuncTest("sec", MathTokenType.Secant);
        }

        [TestMethod]
        public void Arcsin()
        {
            FuncTest("arcsin", MathTokenType.Arcsine);
        }

        [TestMethod]
        public void Arccosec()
        {
            FuncTest("arccosec", MathTokenType.Arccosecant);
        }

        [TestMethod]
        public void Arccsc()
        {
            FuncTest("arccsc", MathTokenType.Arccosecant);
        }

        [TestMethod]
        public void Arccos()
        {
            FuncTest("arccos", MathTokenType.Arccosine);
        }

        [TestMethod]
        public void Arctg()
        {
            FuncTest("arctg", MathTokenType.Arctangent);
        }

        [TestMethod]
        public void Arctan()
        {
            FuncTest("arctan", MathTokenType.Arctangent);
        }

        [TestMethod]
        public void Arcctg()
        {
            FuncTest("arcctg", MathTokenType.Arccotangent);
        }

        [TestMethod]
        public void Arccot()
        {
            FuncTest("arccot", MathTokenType.Arccotangent);
        }

        [TestMethod]
        public void Arcsec()
        {
            FuncTest("arcsec", MathTokenType.Arcsecant);
        }

        [TestMethod]
        public void Sqrt()
        {
            FuncTest("sqrt", MathTokenType.Sqrt);
        }

        [TestMethod]
        public void Root()
        {
            var tokens = lexer.Tokenize("root(27, 3)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.Root),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(27),
                new MathToken(MathTokenType.Comma),
                new MathToken(3),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Lg()
        {
            FuncTest("lg", MathTokenType.Lg);
        }

        [TestMethod]
        public void Ln()
        {
            FuncTest("ln", MathTokenType.Ln);
        }

        [TestMethod]
        public void Log()
        {
            var tokens = lexer.Tokenize("log(2, 2)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.Log),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(2),
                new MathToken(MathTokenType.Comma),
                new MathToken(2),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Deriv()
        {
            var tokens = lexer.Tokenize("deriv(sin(x), x)");

            var expected = new List<MathToken>()
            {
                new MathToken(MathTokenType.Derivative),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(MathTokenType.Sine),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken('x'),
                new MathToken(MathTokenType.CloseBracket),
                new MathToken(MathTokenType.Comma),
                new MathToken('x'),
                new MathToken(MathTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NotAsWord()
        {
            FuncTest("not", MathTokenType.Not);
        }

        [TestMethod]
        public void AndAsWord()
        {
            var tokens = lexer.Tokenize("1 and 2");

            var expected = new List<MathToken>()
            {
                new MathToken(1),
                new MathToken(MathTokenType.And),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void OrAsWord()
        {
            var tokens = lexer.Tokenize("1 or 2");

            var expected = new List<MathToken>()
            {
                new MathToken(1),
                new MathToken(MathTokenType.Or),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void XOrAsWord()
        {
            var tokens = lexer.Tokenize("1 xor 2");

            var expected = new List<MathToken>()
            {
                new MathToken(1),
                new MathToken(MathTokenType.XOr),
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Var()
        {
            var tokens = lexer.Tokenize("x * y");

            var expected = new List<MathToken>()
            {
                new MathToken('x'),
                new MathToken(MathTokenType.Multiplication),
                new MathToken('y')
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

    }

}
