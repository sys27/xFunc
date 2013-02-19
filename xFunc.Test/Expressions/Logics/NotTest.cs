using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Logics.Expressions;
using xFunc.Logics;

namespace xFunc.Test.Expressions.Logics
{

    [TestClass]
    public class NotTest
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
            ILogicExpression exp = parser.Parse("!a");
            LogicParameterCollection parameters = new LogicParameterCollection();
            parameters.Add('a');

            parameters['a'] = true;
            Assert.IsFalse(exp.Calculate(parameters));

            parameters['a'] = false;
            Assert.IsTrue(exp.Calculate(parameters));
        }

    }

}
