using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Library.Maths.Expressions;
using xFunc.Library.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ArccotMathExpressionTest
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
            IMathExpression exp = parser.Parse("arccot(1)");

            Assert.AreEqual(MathExtentions.Acot(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("arctan(1)");

            Assert.AreEqual(MathExtentions.Acot(1) / Math.PI * 180, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("arctan(1)");

            Assert.AreEqual(MathExtentions.Acot(1) / Math.PI * 200, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("arccot(x)"));

            Assert.AreEqual("-(1 / (1 + (x ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("arccot(2x)"));

            Assert.AreEqual("-(2 / (1 + ((2 * x) ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(arccot(xy), x)").Derivative();
            Assert.AreEqual("-(y / (1 + ((x * y) ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(arccot(xy), y)").Derivative();
            Assert.AreEqual("-(x / (1 + ((x * y) ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(arccot(x), y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("arccot(1)");

            Assert.AreEqual("arccot(1)", exp.ToString());
        }

    }

}
