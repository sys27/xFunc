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
        private const double E = 0.0000000001;

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

            double expected = 1.00015232804391;
            Assert.IsTrue(Math.Abs(expected - exp.Calculate(null)) < E);
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("sec(1)");

            double expected = 1.85081571768093;
            Assert.IsTrue(Math.Abs(expected - exp.Calculate(null)) < E);
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("sec(1)");

            double expected = 1.0001233827398;
            Assert.IsTrue(Math.Abs(expected - exp.Calculate(null)) < E);
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(sec(2x), x)");

            Assert.AreEqual("2 * (tan(2 * x) * sec(2 * x))", exp.Derivative().ToString());
        }

    }
}
