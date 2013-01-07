using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SecantMathExpressionTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("sec(1)");

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("sec(1)");

            Assert.AreEqual(MathExtentions.Sec(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("sec(1)");

            Assert.AreEqual(MathExtentions.Sec(Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(sec(2x), x)");

            Assert.AreEqual("2 * (tan(2 * x) * sec(2 * x))", exp.Derivative().ToString());
        }

    }
}
