using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class GreaterTest
    {

        [TestMethod]
        public void CalculateGreaterTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 463) };
            var greaterThen = new GreaterThan(new Variable("x"), new Number(10));

            Assert.AreEqual(true, greaterThen.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateGreaterFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new GreaterThan(new Variable("x"), new Number(10));

            Assert.AreEqual(false, lessThen.Calculate(parameters));
        }

    }

}
