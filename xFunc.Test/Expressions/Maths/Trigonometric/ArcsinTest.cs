using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class ArcsinTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arcsin(new Number(1));

            Assert.AreEqual(Math.Asin(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arcsin(new Number(1));

            Assert.AreEqual(Math.Asin(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arcsin(new Number(1));

            Assert.AreEqual(Math.Asin(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Arcsin(new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 / sqrt(1 - (x ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IExpression exp = new Arcsin(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / sqrt(1 - ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // arcsin(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arcsin(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / sqrt(1 - ((2 * x) ^ 2))", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("arcsin(5 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / sqrt(1 - ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Arcsin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) / sqrt(1 - ((x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Arcsin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) / sqrt(1 - ((x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression exp = new Arcsin(new Variable("x"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
