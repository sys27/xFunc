using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class DecTest
    {

        [TestMethod]
        public void DecCalcTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var dec = new Dec(new Variable("x"));
            var result = (double)dec.Calculate(parameters);

            Assert.AreEqual(9.0, result);
            Assert.AreEqual(9.0, parameters["x"]);
        }

    }

}
