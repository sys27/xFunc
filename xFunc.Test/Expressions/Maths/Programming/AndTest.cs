using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class AndTest
    {

        [TestMethod]
        public void CalculateAndTrueTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));
            var greaterThen = new GreaterThan(new Variable("x"), new Number(-10));
            var and = new And(lessThen, greaterThen);

            Assert.AreEqual(1, and.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateAndFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var lessThen = new LessThan(new Variable("x"), new Number(10));
            var greaterThen = new GreaterThan(new Variable("x"), new Number(10));
            var and = new And(lessThen, greaterThen);

            Assert.AreEqual(0, and.Calculate(parameters));
        }

    }

}
