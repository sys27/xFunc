using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library;
using xFunc.Library.Expressions.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class TangentMathExpressionTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("tan(1)");

            Assert.AreEqual(Math.Tan(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("tan(1)");

            Assert.AreEqual(Math.Tan(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("tan(1)");

            Assert.AreEqual(Math.Tan(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("tan(x)"));

            Assert.AreEqual("1 / (cos(x) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("tan(2x)"));

            Assert.AreEqual("2 / (cos(2 * x) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(tan(xy), x)").Derivative();
            Assert.AreEqual("y / (cos(x * y) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(tan(xy), y)").Derivative();
            Assert.AreEqual("x / (cos(x * y) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(tan(x), y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("tan(2x)");

            Assert.AreEqual("tan(2 * x)", exp.ToString());
        }

    }

}
