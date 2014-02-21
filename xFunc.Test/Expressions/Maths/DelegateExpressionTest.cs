using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DelegateExpressionTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var parameters = new ParameterCollection()
            {
                new Parameter("x", 10)
            };
            var func = new DelegateExpression(p => p.Parameters["x"] + 1);

            var result = func.Calculate(parameters);

            Assert.AreEqual(11.0, result);
        }

        [TestMethod]
        public void CalculateTest2()
        {
            var func = new DelegateExpression(p => 10.0);

            var result = func.Calculate(null);

            Assert.AreEqual(10.0, result);
        }

        [TestMethod]
        public void CalculateTest3()
        {
            var uf1 = new UserFunction("func", new[] { new Variable("x") }, 1);
            var func = new DelegateExpression(p => p.Parameters["x"] == 10 ? 0 : 1);
            var funcs = new FunctionCollection();
            funcs.Add(uf1, func);

            var uf2 = new UserFunction("func", new[] { new Number(12) }, 1);
            var result = uf2.Calculate(new ExpressionParameters(funcs));

            Assert.AreEqual(1, result);
        }

    }

}
