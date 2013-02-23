using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

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
            IMathExpression exp = new Sine(new Multiplication(new Number(2), new Variable('x')));
            bool expected = MathParser.HasVar(exp, new Variable('x'));

            Assert.AreEqual(expected, true);
        }

        [TestMethod]
        public void HasVarTest2()
        {
            IMathExpression exp = new Sine(new Multiplication(new Number(2), new Number(3)));
            bool expected = MathParser.HasVar(exp, new Variable('x'));

            Assert.AreEqual(expected, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParseNullStr()
        {
            parser.Parse(null);
        }

    }

}
