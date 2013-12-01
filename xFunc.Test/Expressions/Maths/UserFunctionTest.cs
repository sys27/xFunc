using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using System.Collections.Generic;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class UserFunctionTest
    {

        [TestMethod]
        public void CalculateTest1()
        {
            var functions = new FunctionCollection();
            functions.Add(new UserFunction("f", new IExpression[] { new Variable("x") }, 1), new Ln(new Variable("x")));

            var func = new UserFunction("f", new IExpression[] { new Number(1) }, 1);
            Assert.AreEqual(Math.Log(1), func.Calculate(functions));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void CalculateTest2()
        {
            var functions = new FunctionCollection();

            var func = new UserFunction("f", new IExpression[] { new Number(1) }, 1);
            Assert.AreEqual(Math.Log(1), func.Calculate(functions));
        }

    }

}
