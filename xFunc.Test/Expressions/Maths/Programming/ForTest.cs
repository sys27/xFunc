using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class ForTest
    {

        [TestMethod]
        public void CalculateForTest()
        {
            var parameters = new ExpressionParameters();

            var init = new Define(new Variable("i"), new Number(0));
            var cond = new LessThan(new Variable("i"), new Number(10));
            var iter = new Define(new Variable("i"), new Add(new Variable("i"), new Number(1))); 

            var @for = new For(new Variable("i"), init, cond, iter);
            @for.Calculate(parameters);

            Assert.AreEqual(10, parameters.Parameters["i"]);
        }

    }

}
