using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Library.Logics;
using xFunc.Library.Logics.Expressions;

namespace xFunc.Test.Expressions.Logics
{

    [TestClass]
    public class NAndLogicExpressionTest
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
            ILogicExpression exp = parser.Parse("a nand b");
            LogicParameterCollection parameters = new LogicParameterCollection();
            parameters.Add('a');
            parameters.Add('b');

            parameters['a'] = true;
            parameters['b'] = true;
            Assert.IsFalse(exp.Calculate(parameters));

            parameters['a'] = true;
            parameters['b'] = false;
            Assert.IsTrue(exp.Calculate(parameters));

            parameters['a'] = false;
            parameters['b'] = true;
            Assert.IsTrue(exp.Calculate(parameters));

            parameters['a'] = false;
            parameters['b'] = false;
            Assert.IsTrue(exp.Calculate(parameters));
        }

    }

}
