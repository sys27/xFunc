using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class MulAssignTest
    {

        [TestMethod]
        public void MulAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var mul = new MulAssign(new Variable("x"), new Number(2));
            var result = mul.Calculate(parameters);
            var expected = 20.0;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected, parameters["x"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void BoolMulNumberTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", true) };
            var add = new MulAssign(new Variable("x"), new Number(2));
            add.Calculate(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterTypeMismatchException))]
        public void NumberMulBoolTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 2) };
            var add = new MulAssign(new Variable("x"), new Bool(true));
            add.Calculate(parameters);
        }

    }

}
