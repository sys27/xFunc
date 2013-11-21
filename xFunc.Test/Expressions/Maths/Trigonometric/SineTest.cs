using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class SineTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Sin(new Number(1));

            Assert.AreEqual(Math.Sin(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Sin(new Number(1));

            Assert.AreEqual(Math.Sin(1 * Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Sin(new Number(1));

            Assert.AreEqual(Math.Sin(1 * Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Sin(new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("cos(x) * 1", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IExpression exp = new Sin(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // sin(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Sin(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sin(3 * x)", exp.ToString());
            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Sin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("cos(x * y) * (1 * y)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Sin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("cos(x * y) * (x * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression exp = new Sin(new Variable("y"));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
