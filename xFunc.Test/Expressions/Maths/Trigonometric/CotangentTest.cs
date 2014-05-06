using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class CotangentTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Cot(new Number(1));

            Assert.AreEqual(MathExtentions.Cot(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Cot(new Number(1));

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Cot(new Number(1));

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

    }

}
