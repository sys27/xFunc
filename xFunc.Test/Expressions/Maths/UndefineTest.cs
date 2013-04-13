using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using System.Collections.Generic;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class UndefineTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            MathParameterCollection parameters = new MathParameterCollection();
            IMathExpression def = new Assign("a", new Number(1));
            def.Calculate(parameters);
            Assert.AreEqual(1, parameters["a"]);

            IMathExpression undef = new Undefine("a");
            undef.Calculate(parameters);
            Assert.IsFalse(parameters.ContainsKey("a"));
        }

    }

}
