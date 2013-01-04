using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Exceptions;
using xFunc.Library.Logics;
using xFunc.Library.Logics.Exceptions;

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
            List<LogicToken> expected = new List<LogicToken>
                                          {
                                              new LogicToken(LogicTokenType.Variable, 'a'),
                                              new LogicToken(LogicTokenType.And),
                                              new LogicToken(LogicTokenType.OpenBracket),
                                              new LogicToken(LogicTokenType.Variable, 'b'),
                                              new LogicToken(LogicTokenType.Or),
                                              new LogicToken(LogicTokenType.Variable, 'c'),
                                              new LogicToken(LogicTokenType.CloseBracket)
                                          };
            List<LogicToken> actual = new List<LogicToken>(logicLexer.LogicTokenization(function));

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
        [ExpectedException(typeof(LogicLexerException))]
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
            List<MathToken> expected = new List<MathToken>
                                          {
                                              new MathToken(MathTokenType.Sine),
                                              new MathToken(MathTokenType.OpenBracket),
                                              new MathToken(MathTokenType.Variable, 'x'),
                                              new MathToken(MathTokenType.CloseBracket),
                                              new MathToken(MathTokenType.Addition),
                                              new MathToken(MathTokenType.Number, 1),
                                              new MathToken(MathTokenType.Multiplication),
                                              new MathToken(MathTokenType.Variable, 'x')
                                          };
            List<MathToken> actual = new List<MathToken>(mathLexer.MathTokenization(function));

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
        [ExpectedException(typeof(MathLexerException))]
        public void MathTokenizationSymbolNotSupportedTest()
        {
            mathLexer.MathTokenization("a | b");
        }

    }

}
