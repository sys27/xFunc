using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SubtractionTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Sub(new Number(1), new Number(2));

            Assert.AreEqual(-1, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            // x - sin(x)
            IMathExpression exp = new Sub(new Variable("x"), new Sin(new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 - (cos(x) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num1 = new Number(2);
            Variable x = new Variable("x");
            Mul mul1 = new Mul(num1, x);

            Number num2 = new Number(3);
            Mul mul2 = new Mul(num2, x.Clone());

            IMathExpression exp = new Sub(mul1, mul2);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) - (3 * 1)", deriv.ToString());

            num1.Value = 5;
            num2.Value = 4;
            Assert.AreEqual("(5 * x) - (4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) - (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Sub(new Mul(new Variable("x"), new Variable("y")), new Variable("y"));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) - 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Sub(new Variable("x"), new Variable("y"));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Sub(new Variable("x"), new Variable("y"));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("-1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IMathExpression exp = new Sub(new Variable("x"), new Number(1));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
