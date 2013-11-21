using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LogTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Log(new Number(10), new Number(2));

            Assert.AreEqual(Math.Log(10, 2), exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Log(new Variable("x"), new Number(2));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // log(x, 2)
            Number num = new Number(2);
            Variable x = new Variable("x");

            IExpression exp = new Log(x, num);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("log(4, x)", exp.ToString());
            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            IExpression exp = new Log(new Number(2), new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(ln(2) * (1 / x)) / (ln(x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Log(new Variable("x"), new Number(2));
            IExpression deriv = exp.Differentiate(new Variable("x"));
            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Log(new Variable("x"), new Number(2));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
