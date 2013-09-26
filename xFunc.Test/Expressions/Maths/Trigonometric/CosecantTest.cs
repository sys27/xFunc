using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class CosecantTest
    {

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IMathExpression exp = new Csc(new Number(1)) { AngleMeasurement = AngleMeasurement.Degree };

            Assert.AreEqual(MathExtentions.Csc(Math.PI / 180), exp.Calculate());
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            IMathExpression exp = new Csc(new Number(1)) { AngleMeasurement = AngleMeasurement.Radian };

            Assert.AreEqual(MathExtentions.Csc(1), exp.Calculate());
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IMathExpression exp = new Csc(new Number(1)) { AngleMeasurement = AngleMeasurement.Gradian };

            Assert.AreEqual(MathExtentions.Csc(Math.PI / 200), exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = new Csc(new Mul(new Number(2), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(2 * 1) * (cot(2 * x) * csc(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTestWithAngle()
        {
            var exp = new Csc(new Variable("x"), AngleMeasurement.Radian);
            var deriv = exp.Differentiate();

            var mul1 = deriv as Mul;
            var mul2 = mul1.Right as Mul;
            var cot = mul2.Left as Cot;
            var csc = mul2.Right as Csc;

            Assert.AreEqual(AngleMeasurement.Radian, cot.AngleMeasurement);
            Assert.AreEqual(AngleMeasurement.Radian, csc.AngleMeasurement);
        }

    }

}
