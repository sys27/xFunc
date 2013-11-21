using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DivisionTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Div(new Number(1), new Number(2));

            Assert.AreEqual(1.0 / 2.0, exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Div(new Number(1), new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(1 * 1) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // sin(x) / x
            IExpression exp = new Div(new Sin(new Variable("x")), new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(((cos(x) * 1) * x) - (sin(x) * 1)) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // (2x) / (3x)
            Number num1 = new Number(2);
            Variable x = new Variable("x");
            Mul mul1 = new Mul(num1, x);

            Number num2 = new Number(3);
            Mul mul2 = new Mul(num2, x.Clone());

            IExpression exp = new Div(mul1, mul2);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(((2 * 1) * (3 * x)) - ((2 * x) * (3 * 1))) / ((3 * x) ^ 2)", deriv.ToString());

            num1.Value = 4;
            num2.Value = 5;
            Assert.AreEqual("(4 * x) / (5 * x)", exp.ToString());
            Assert.AreEqual("(((2 * 1) * (3 * x)) - ((2 * x) * (3 * 1))) / ((3 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            // (y + x ^ 2) / x
            IExpression exp = new Div(new Add(new Variable("y"), new Pow(new Variable("x"), new Number(2))), new Variable("x"));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("(((1 * (2 * (x ^ (2 - 1)))) * x) - ((y + (x ^ 2)) * 1)) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Div(new Variable("y"), new Variable("x"));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("-(y * 1) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression exp = new Div(new Variable("y"), new Variable("x"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("1 / x", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            // (x + 1) / x
            IExpression exp = new Div(new Add(new Variable("x"), new Number(1)), new Variable("x"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
