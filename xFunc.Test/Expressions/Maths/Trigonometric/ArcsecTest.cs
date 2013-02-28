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
            IMathExpression exp = new Arcsec(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Asec(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Arcsec(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Asec(1) / Math.PI * 180, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Arcsec(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Asec(1) / Math.PI * 200, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Arcsec(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // arcsec(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Arcsec(mul);
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arcsec(4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());
        }

    }

}
