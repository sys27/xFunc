using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Logics;
using xFunc.Logics.Expressions;

namespace xFunc.Test
{

    [TestClass]
    public class LogicParserTest
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

        private LogicParser logicParser;

        [TestInitialize]
        public void TestInit()
        {
            logicParser = new LogicParser();
        }

        [TestMethod]
        public void GetLogicParametersTest()
        {
            string function = "a | b & c & (a | c)";
            LogicParameterCollection expected = new LogicParameterCollection()
                                                {
                                                    'a',
                                                    'b',
                                                    'c'
                                                };

            LogicParameterCollection actual = logicParser.GetLogicParameters(function);


            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertLogicExpressionToColletion()
        {
            ILogicExpression exp = logicParser.Parse("(a | b) -> !c");
            List<ILogicExpression> actual = new List<ILogicExpression>(logicParser.ConvertLogicExpressionToCollection(exp));

            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void LogicParseTest()
        {
            string function = "(a | c) & b";
            ILogicExpression actual = logicParser.Parse(function);
            Assert.IsTrue(actual is AndLogicExpression);

            AndLogicExpression a = (AndLogicExpression)actual;
            Assert.IsTrue(a.FirstOperand is OrLogicExpression);
            Assert.IsTrue(a.SecondOperand is VariableLogicExpression);
        }

    }

}
