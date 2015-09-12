using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class IncTest
    {

        [TestMethod]
        public void IncCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var inc = new Inc(new Variable("x"));
            var result = (double)inc.Calculate(parameters);

            Assert.AreEqual(11.0, result);
            Assert.AreEqual(11.0, parameters["x"]);
        }

    }

}
