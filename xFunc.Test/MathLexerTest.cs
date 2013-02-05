using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths;

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
            var tokens = lexer.Tokenization(func + "(3)");

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
            lexer.Tokenization(null);
        }

        [TestMethod]
        public void Brackets()
        {
            var tokens = lexer.Tokenization("(2)");

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
            var tokens = lexer.Tokenization("2 + 2");

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
            var tokens = lexer.Tokenization("(+2)");

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
            var tokens = lexer.Tokenization("+2");

            var expected = new List<MathToken>()
            {
                new MathToken(2)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Sub()
        {
            var tokens = lexer.Tokenization("2 - 2");

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
            var tokens = lexer.Tokenization("(-2)");

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
            var tokens = lexer.Tokenization("-2");

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
            var tokens = lexer.Tokenization("2 * 2");

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
            var tokens = lexer.Tokenization("2 / 2");

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
            var tokens = lexer.Tokenization("2 ^ 2");

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
            var tokens = lexer.Tokenization("log(2, 2)");

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
            var tokens = lexer.Tokenization("~2");

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
            var tokens = lexer.Tokenization("2 & 2");

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
            var tokens = lexer.Tokenization("2 | 2");

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
            var tokens = lexer.Tokenization("x := 2");

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
            var tokens = lexer.Tokenization("-2764786 + 46489879");

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
            var tokens = lexer.Tokenization("-45.3 + 87.64");

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
            var tokens = lexer.Tokenization("-2x");

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
            var tokens = lexer.Tokenization("3pi");

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
        public void Sec()
        {
            FuncTest("sec", MathTokenType.Secant);
        }

        [TestMethod]
        public void Csc()
        {
            FuncTest("csc", MathTokenType.Cosecant);
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

    }

}
