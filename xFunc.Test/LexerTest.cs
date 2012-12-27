using xFunc.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using xFunc.Library.Exceptions;

namespace xFunc.Test
{

    [TestClass()]
    public class LexerTest
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private MathLexer mathLexer;
        private LogicLexer logicLexer;

        [TestInitialize]
        public void TestInit()
        {
            mathLexer = new MathLexer();
            logicLexer = new LogicLexer();
        }

        /// <summary>
        ///Тест для LogicTokenization
        ///</summary>
        [TestMethod]
        public void LogicTokenizationTest()
        {
            string function = "a & (b | c)";
            List<Token> expected = new List<Token>
                                          {
                                              new Token(TokenType.Variable, 'a'),
                                              new Token(TokenType.And),
                                              new Token(TokenType.OpenBracket),
                                              new Token(TokenType.Variable, 'b'),
                                              new Token(TokenType.Or),
                                              new Token(TokenType.Variable, 'c'),
                                              new Token(TokenType.CloseBracket)
                                          };
            List<Token> actual = new List<Token>(logicLexer.LogicTokenization(function));

            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест для LogicTokenization, передача пустой строки.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogicTokenizationIsFunctionNullTest()
        {
            logicLexer.LogicTokenization(null);
        }

        /// <summary>
        ///Тест для LogicTokenization, не поддерживаемые символы.
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void LogicTokenizationSymbolNotSupportedTest()
        {
            logicLexer.LogicTokenization("a # b");
        }

        /// <summary>
        ///Тест для MathTokenization
        ///</summary>
        [TestMethod]
        public void MathTokenizationTest()
        {
            string function = "sin(x)+1*x";
            List<Token> expected = new List<Token>
                                          {
                                              new Token(TokenType.Sine),
                                              new Token(TokenType.OpenBracket),
                                              new Token(TokenType.Variable, 'x'),
                                              new Token(TokenType.CloseBracket),
                                              new Token(TokenType.Addition),
                                              new Token(TokenType.Number, 1),
                                              new Token(TokenType.Multiplication),
                                              new Token(TokenType.Variable, 'x')
                                          };
            List<Token> actual = new List<Token>(mathLexer.MathTokenization(function));

            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Тест для MathTokenization, передача пустой строки.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MathTokenizationIsFunctionNullTest()
        {
            mathLexer.MathTokenization(null);
        }

        /// <summary>
        ///Тест для MathTokenization, не поддерживаемые символы.
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        public void MathTokenizationSymbolNotSupportedTest()
        {
            mathLexer.MathTokenization("a | b");
        }

    }

}
