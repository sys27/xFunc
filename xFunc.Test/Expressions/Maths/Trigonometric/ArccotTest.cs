using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;
using xFunc.Maths;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class ArccotTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arccot(new Number(1));

            Assert.AreEqual(MathExtentions.Acot(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arccot(new Number(1));

            Assert.AreEqual(MathExtentions.Acot(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arccot(new Number(1));

            Assert.AreEqual(MathExtentions.Acot(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
