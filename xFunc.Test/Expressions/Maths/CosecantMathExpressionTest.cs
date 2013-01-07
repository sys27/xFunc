using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class CosecantMathExpressionTest
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
            IMathExpression exp = parser.Parse("csc(1)");

            double expected = 57.2986884985502;
            Assert.IsTrue(Math.Abs(expected - exp.Calculate(null)) < E);
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("csc(1)");

            double expected = 1.1883951057781;
            Assert.IsTrue(Math.Abs(expected - exp.Calculate(null)) < E);
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("csc(1)");

            double expected = 63.6645953060006;
            Assert.IsTrue(Math.Abs(expected - exp.Calculate(null)) < E);
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(csc(2x), x)");

            // todo: bug -2
            Assert.AreEqual("-2 * (cot(2 * x) * csc(2 * x))", exp.Derivative().ToString());
        }

    }

}
