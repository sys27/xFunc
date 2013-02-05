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

    }

}
