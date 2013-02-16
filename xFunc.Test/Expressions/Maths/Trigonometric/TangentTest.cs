using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class TangentTest
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
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("tan(x)"));

            Assert.AreEqual("1 / (cos(x) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("tan(2x)"));

            Assert.AreEqual("2 / (cos(2 * x) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Tangent(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("2 / (cos(2 * x) ^ 2)", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("tan(5 * x)", exp.ToString());
            Assert.AreEqual("2 / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(tan(xy), x)").Differentiation();
            Assert.AreEqual("y / (cos(x * y) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(tan(xy), y)").Differentiation();
            Assert.AreEqual("x / (cos(x * y) ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(tan(x), y)").Differentiation();
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
