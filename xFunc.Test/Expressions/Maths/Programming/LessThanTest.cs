using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class LessThanTest
    {

        [TestMethod]
        public void CalculateLessTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));

            Assert.AreEqual(true, lessThen.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateLessFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));

            Assert.AreEqual(false, lessThen.Calculate(parameters));
        }

    }

}
