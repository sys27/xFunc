using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class SecantTest
    {
        
        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Secant(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Secant(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Sec(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Secant(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Secant(new Multiplication(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // sec(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Secant(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("sec(4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

    }
}
