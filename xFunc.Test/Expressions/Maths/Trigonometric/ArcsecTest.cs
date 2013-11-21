using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class ArcsecTest
    {
        
        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arcsec(new Number(1));

            Assert.AreEqual(MathExtentions.Asec(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arcsec(new Number(1));

            Assert.AreEqual(MathExtentions.Asec(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arcsec(new Number(1));

            Assert.AreEqual(MathExtentions.Asec(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Arcsec(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // arcsec(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arcsec(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arcsec(4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());
        }

    }

}
