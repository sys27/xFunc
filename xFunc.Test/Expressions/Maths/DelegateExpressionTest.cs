using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DelegateExpressionTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", 10)
            };
            var func = new DelegateExpression(p => p.Parameters["x"] + 1);

            var result = func.Calculate(parameters);

            Assert.AreEqual(11.0, result);
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var func = new DelegateExpression(p => 10.0);

            var result = func.Calculate(null);

            Assert.AreEqual(10.0, result);
        }

    }

}
