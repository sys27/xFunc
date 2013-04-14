using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Logics;
using xFunc.Logics.Expressions;

namespace xFunc.Test.Expressions.Logics
{

    [TestClass]
    public class AndTest
    {

        private LogicParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new LogicParser();
        }

        [TestMethod]
        public void CalculateTest()
        {
            ILogicExpression exp = parser.Parse("a & b");
            LogicParameterCollection parameters = new LogicParameterCollection();
            parameters.Add("a");
            parameters.Add("b");

            parameters["a"] = true;
            parameters["b"] = true;
            Assert.IsTrue(exp.Calculate(parameters));

            parameters["a"] = true;
            parameters["b"] = false;
            Assert.IsFalse(exp.Calculate(parameters));

            parameters["a"] = false;
            parameters["b"] = true;
            Assert.IsFalse(exp.Calculate(parameters));

            parameters["a"] = false;
            parameters["b"] = false;
            Assert.IsFalse(exp.Calculate(parameters));
        }

    }
}
