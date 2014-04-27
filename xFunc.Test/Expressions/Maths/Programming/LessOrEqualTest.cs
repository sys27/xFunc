using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class LessOrEqualTest
    {

        [TestMethod]
        public void CalculateLessTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessOrEqual(new Variable("x"), new Number(10));

            Assert.AreEqual(1, lessThen.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateLessTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var lessThen = new LessOrEqual(new Variable("x"), new Number(10));

            Assert.AreEqual(1, lessThen.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateLessFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 666) };
            var lessThen = new LessOrEqual(new Variable("x"), new Number(10));

            Assert.AreEqual(0, lessThen.Calculate(parameters));
        }

    }

}
