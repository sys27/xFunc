using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class CosineTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Cos(new Number(1));

            Assert.AreEqual(Math.Cos(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Cos(new Number(1));

            Assert.AreEqual(Math.Cos(1 * Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Cos(new Number(1));

            Assert.AreEqual(Math.Cos(1 * Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
