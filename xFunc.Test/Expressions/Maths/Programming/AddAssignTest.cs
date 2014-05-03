using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class AddAssignTest
    {

        [TestMethod]
        public void AddAssignCalc()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var add = new AddAssign(new Variable("x"), new Number(2));
            var result = add.Calculate(parameters);
            var expected = 12.0;

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected, parameters["x"]);
        }

    }

}
