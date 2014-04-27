using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class OrTest
    {

        [TestMethod]
        public void CalculateOrTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThen(new Variable("x"), new Number(10));
            var greaterThen = new GreaterThen(new Variable("x"), new Number(-10));
            var or = new Or(lessThen, greaterThen);

            Assert.AreEqual(1, or.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateOrTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThen(new Variable("x"), new Number(-10));
            var greaterThen = new GreaterThen(new Variable("x"), new Number(-10));
            var or = new Or(lessThen, greaterThen);

            Assert.AreEqual(1, or.Calculate(parameters));
        }

    }

}
