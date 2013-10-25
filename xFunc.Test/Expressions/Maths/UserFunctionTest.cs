using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class UserFunctionTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var functions = new MathFunctionCollection();
            functions.Add(new UserFunction("f", new IMathExpression[] { new Variable("x") }, 1), new Ln(new Variable("x")));

            var func = new UserFunction("f", new IMathExpression[] { new Number(1) }, 1);
            Assert.AreEqual(Math.Log(1), func.Calculate(functions));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateTest2()
        {
            var functions = new MathFunctionCollection();

            var func = new UserFunction("f", new IMathExpression[] { new Number(1) }, 1);
            Assert.AreEqual(Math.Log(1), func.Calculate(functions));
        }

    }

}
