using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SqrtTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Sqrt(new Number(4));

            Assert.AreEqual(Math.Sqrt(4), exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Sqrt(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Sqrt(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sqrt(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            // sqrt(2xy)
            IExpression exp = new Sqrt(new Mul(new Mul(new Number(2), new Variable("x")), new Variable("y")));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("((2 * 1) * y) / (2 * sqrt((2 * x) * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Sqrt(new Variable("y"));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
