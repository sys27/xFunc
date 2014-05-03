using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class DivAssignTest
    {

        [TestMethod]
        public void DivAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var div = new DivAssign(new Variable("x"), new Number(2));
            var result = div.Calculate(parameters);
            var expected = 5.0;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected, parameters["x"]);
        }

    }

}
