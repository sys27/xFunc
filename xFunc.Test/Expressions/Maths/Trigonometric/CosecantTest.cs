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
            IExpression exp = new Csc(new Number(1));

            Assert.AreEqual(MathExtentions.Csc(Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Csc(new Number(1));

            Assert.AreEqual(MathExtentions.Csc(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Csc(new Number(1));

            Assert.AreEqual(MathExtentions.Csc(Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IExpression exp = new Csc(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("-(2 * 1) * (cot(2 * x) * csc(2 * x))", deriv.ToString());
        }

    }

}
