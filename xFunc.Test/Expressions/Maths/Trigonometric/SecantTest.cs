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
            IMathExpression exp = new Sec(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Sec(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Sec(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Sec(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Sec(new Mul(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // sec(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IMathExpression exp = new Sec(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("sec(4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTestWithAngle()
        {
            var exp = new Sec(new Variable("x"), AngleMeasurement.Radian);
            var deriv = exp.Differentiate();

            var mul1 = deriv as Mul;
            var mul2 = mul1.SecondMathExpression as Mul;
            var tan = mul2.FirstMathExpression as Tan;
            var sec = mul2.SecondMathExpression as Sec;

            Assert.AreEqual(AngleMeasurement.Radian, tan.AngleMeasurement);
            Assert.AreEqual(AngleMeasurement.Radian, sec.AngleMeasurement);
        }

    }
}
