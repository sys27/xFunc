using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class GreaterOrEqualTest
    {

        [TestMethod]
        public void CalculateGreaterTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 463) };
            var lessThen = new GreaterOrEqual(new Variable("x"), new Number(10));

            Assert.AreEqual(1, lessThen.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateGreaterTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var lessThen = new GreaterOrEqual(new Variable("x"), new Number(10));

            Assert.AreEqual(1, lessThen.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateGreaterFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new GreaterOrEqual(new Variable("x"), new Number(10));

            Assert.AreEqual(0, lessThen.Calculate(parameters));
        }

    }

}
