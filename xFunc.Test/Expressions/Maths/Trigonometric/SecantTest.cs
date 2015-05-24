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
            IExpression exp = new Sec(new Number(1));

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Sec(new Number(1));

            Assert.AreEqual(MathExtentions.Sec(1), (double)exp.Calculate(AngleMeasurement.Radian), 0.0000000001);
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Sec(new Number(1));

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

    }
}
