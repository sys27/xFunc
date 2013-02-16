using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class ArccosTest
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
            IMathExpression exp = parser.Parse("arccos(1)");

            Assert.AreEqual(Math.Acos(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("arccos(1)");

            Assert.AreEqual(Math.Acos(1) / Math.PI * 180, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("arccos(1)");

            Assert.AreEqual(Math.Acos(1) / Math.PI * 200, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("arccos(x)"));

            Assert.AreEqual("-(1 / sqrt(1 - (x ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("arccos(2x)"));

            Assert.AreEqual("-(2 / sqrt(1 - ((2 * x) ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // arccos(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Arccos(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("-(2 / sqrt(1 - ((2 * x) ^ 2)))", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("arccos(6 * x)", exp.ToString());
            Assert.AreEqual("-(2 / sqrt(1 - ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(arccos(xy), x)").Differentiation();
            Assert.AreEqual("-(y / sqrt(1 - ((x * y) ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(arccos(xy), y)").Differentiation();
            Assert.AreEqual("-(x / sqrt(1 - ((x * y) ^ 2)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(arccos(x), y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("arccos(1)");

            Assert.AreEqual("arccos(1)", exp.ToString());
        }

    }

}
