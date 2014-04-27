using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;

namespace xFunc.Test.Expressions.Maths.Programming
{

    [TestClass]
    public class WhileTest
    {

        [TestMethod]
        public void CalculateWhileTest()
        {
            var parameters = new ExpressionParameters();
            parameters.Parameters.Add(new Parameter("x", 0));

            var body = new Define(new Variable("x"), new Add(new Variable("x"), new Number(2)));
            var cond = new LessThen(new Variable("x"), new Number(10));

            var @while = new While(body, cond);
            @while.Calculate(parameters);

            Assert.AreEqual(10, parameters.Parameters["x"]);
        }

    }

}
