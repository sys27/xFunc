using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SqrtTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Sqrt(new Number(4));

            Assert.AreEqual(Math.Sqrt(4), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Sqrt(new Multiplication(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Sqrt(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sqrt(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            // sqrt(2xy)
            IMathExpression exp = new Sqrt(new Multiplication(new Multiplication(new Number(2), new Variable("x")), new Variable("y")));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("((2 * 1) * y) / (2 * sqrt((2 * x) * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Sqrt(new Variable("y"));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
