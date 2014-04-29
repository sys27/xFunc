using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class IfTest
    {

        [TestMethod]
        public void CalculateIfElseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };

            var cond = new Equal(new Variable("x"), new Number(10));
            var @if = new If(cond, new Number(20), new Number(0));

            Assert.AreEqual(20.0, @if.Calculate(parameters));

            parameters["x"] = 0;

            Assert.AreEqual(0.0, @if.Calculate(parameters));
        }

        [TestMethod]
        public void CalculateIfTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };

            var cond = new Equal(new Variable("x"), new Number(10));
            var @if = new If(cond, new Number(20));

            Assert.AreEqual(20.0, @if.Calculate(parameters));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateElseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };

            var cond = new Equal(new Variable("x"), new Number(10));
            var @if = new If(cond, new Number(20));

            @if.Calculate(parameters);
        }

    }

}
