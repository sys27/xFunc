using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Logics;

namespace xFunc.Test
{

    [TestClass]
    public class LogicLexerTest
    {

        private ILexer lexer;

        [TestInitialize]
        public void TestInit()
        {
            lexer = new LogicLexer();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullArg()
        {
            lexer.Tokenize(null);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicLexerException))]
        public void NotSupportedSymbol()
        {
            lexer.Tokenize("@");
        }

        [TestMethod]
        public void Brackets()
        {
            var tokens = lexer.Tokenize("(a)");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.OpenBracket),
                new LogicToken("a"),
                new LogicToken(LogicTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Not()
        {
            var tokens = lexer.Tokenize("!a");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.Not),
                new LogicToken("a")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void And()
        {
            var tokens = lexer.Tokenize("a & b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.And),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Or()
        {
            var tokens = lexer.Tokenize("a | b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Or),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Impl1()
        {
            var tokens = lexer.Tokenize("a -> b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Implication),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Impl2()
        {
            var tokens = lexer.Tokenize("a => b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Implication),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Eq1()
        {
            var tokens = lexer.Tokenize("a <-> b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Equality),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Eq()
        {
            var tokens = lexer.Tokenize("a <=> b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Equality),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void XOr()
        {
            var tokens = lexer.Tokenize("a ^ b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.XOr),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Assign()
        {
            var tokens = lexer.Tokenize("a := true");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Assign),
                new LogicToken(LogicTokenType.True)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void True()
        {
            var tokens = lexer.Tokenize("true");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.True)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void False()
        {
            var tokens = lexer.Tokenize("false");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.False)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NotAsWord()
        {
            var tokens = lexer.Tokenize("not(a)");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.Not),
                new LogicToken(LogicTokenType.OpenBracket),
                new LogicToken("a"),
                new LogicToken(LogicTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void AndAsWord()
        {
            var tokens = lexer.Tokenize("a and b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.And),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void OrAsWord()
        {
            var tokens = lexer.Tokenize("a or b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Or),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void ImplAsWord()
        {
            var tokens = lexer.Tokenize("a impl b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Implication),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void EqAsWord()
        {
            var tokens = lexer.Tokenize("a eq b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.Equality),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NOrAsWord()
        {
            var tokens = lexer.Tokenize("a nor b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.NOr),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void NAndAsWord()
        {
            var tokens = lexer.Tokenize("a nand b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.NAnd),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void XOrAsWord()
        {
            var tokens = lexer.Tokenize("a xor b");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a"),
                new LogicToken(LogicTokenType.XOr),
                new LogicToken("b")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void TruthTableAsWord()
        {
            var tokens = lexer.Tokenize("table(a & b)");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.TruthTable),
                new LogicToken(LogicTokenType.OpenBracket),
                new LogicToken("a"),
                new LogicToken(LogicTokenType.And),
                new LogicToken("b"),
                new LogicToken(LogicTokenType.CloseBracket)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void TrueShort()
        {
            var tokens = lexer.Tokenize("t");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.True)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void FalseShort()
        {
            var tokens = lexer.Tokenize("f");

            var expected = new List<LogicToken>()
            {
                new LogicToken(LogicTokenType.False)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void Var()
        {
            var tokens = lexer.Tokenize("a");

            var expected = new List<LogicToken>()
            {
                new LogicToken("a")
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        public void StringVarLexerTest()
        {
            var tokens = lexer.Tokenize("aaa := true");

            var expected = new List<LogicToken>()
            {
                new LogicToken("aaa"),
                new LogicToken(LogicTokenType.Assign),
                new LogicToken(LogicTokenType.True)
            };
            CollectionAssert.AreEqual(expected, tokens.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(LogicLexerException))]
        public void NotBalancedOpen()
        {
            var tokens = lexer.Tokenize("table(a(");
        }

        [TestMethod]
        [ExpectedException(typeof(LogicLexerException))]
        public void NotBalancedClose()
        {
            var tokens = lexer.Tokenize("table)a)");
        }

        [TestMethod]
        [ExpectedException(typeof(LogicLexerException))]
        public void NotBalancedFirstClose()
        {
            var tokens = lexer.Tokenize("table)a(");
        }

    }

}
