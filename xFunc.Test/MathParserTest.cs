using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Exceptions;
using System.Collections.Generic;

namespace xFunc.Test
{

    [TestClass]
    public class MathParserTest
    {

        private MathParser parser;
        private MathLexerMock lexer;

        [TestInitialize]
        public void TestInit()
        {
            lexer = new MathLexerMock();
            parser = new MathParser(lexer);
        }

        [TestMethod]
        public void HasVarTest1()
        {
            IMathExpression exp = new Sin(new Multiplication(new Number(2), new Variable("x")));
            bool expected = MathParser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, true);
        }

        [TestMethod]
        public void HasVarTest2()
        {
            IMathExpression exp = new Sin(new Multiplication(new Number(2), new Number(3)));
            bool expected = MathParser.HasVar(exp, new Variable("x"));

            Assert.AreEqual(expected, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseNullStr()
        {
            parser.Parse(null);
        }

        [TestMethod]
        public void ParseLog()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Log),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(9),
                new MathToken(MathTokenType.Comma),
                new MathToken(3),
                new MathToken(MathTokenType.CloseBracket)
            };

            var exp = parser.Parse("log(9, 3)", false);
            Assert.AreEqual("log(9, 3)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseLogWithOneParam()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Log),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken(9),
                new MathToken(MathTokenType.CloseBracket)
            };

            var exp = parser.Parse("log(9)");
        }

        [TestMethod]
        public void ParseRoot()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Root),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken("x"),
                new MathToken(MathTokenType.Comma),
                new MathToken(3),
                new MathToken(MathTokenType.CloseBracket)
            };

            var exp = parser.Parse("root(x, 3)", false);
            Assert.AreEqual("root(x, 3)", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseRootWithOneParam()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Root),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken("x"),
                new MathToken(MathTokenType.CloseBracket)
            };

            var exp = parser.Parse("root(x)");
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseDerivWithOneParam()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Derivative),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken("x"),
                new MathToken(MathTokenType.CloseBracket)
            };

            var exp = parser.Parse("deriv(x)");
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseDerivSecondParamIsNotVar()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Derivative),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken("x"),
                new MathToken(MathTokenType.Comma),
                new MathToken(3),
                new MathToken(MathTokenType.CloseBracket)
            };

            var exp = parser.Parse("deriv(x, 3)");
        }

        [TestMethod]
        public void ParseAssign()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken("x"),
                new MathToken(MathTokenType.Assign),
                new MathToken(3)
            };

            var exp = parser.Parse("x := 3", false);
            Assert.AreEqual("x := 3", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseAssignWithOneParam()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken("x"),
                new MathToken(MathTokenType.Assign)
            };

            var exp = parser.Parse("x := ");
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ParseAssignFirstParamIsNotVar()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(5),
                new MathToken(MathTokenType.Assign),
                new MathToken(3)
            };

            var exp = parser.Parse("5 := 3");
        }

        [TestMethod]
        [ExpectedException(typeof(MathParserException))]
        public void ErrorWhileParsingTree()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken(MathTokenType.Sine),
                new MathToken(MathTokenType.OpenBracket),
                new MathToken("x"),
                new MathToken(MathTokenType.CloseBracket),
                new MathToken(2)
            };

            var exp = parser.Parse("sin(x) 2");
        }

        [TestMethod]
        public void StringVarParserTest()
        {
            lexer.Tokens = new List<MathToken>()
            {
                new MathToken("aaa"),
                new MathToken(MathTokenType.Assign),
                new MathToken(1)
            };

            var exp = parser.Parse("aaa := 1", false);
            Assert.AreEqual("aaa := 1", exp.ToString());
        }

    }

}
