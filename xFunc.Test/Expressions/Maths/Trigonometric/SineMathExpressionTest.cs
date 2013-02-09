using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SineMathExpressionTest
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
            IMathExpression exp = parser.Parse("sin(1)");

            Assert.AreEqual(Math.Sin(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("sin(1)");

            Assert.AreEqual(Math.Sin(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("sin(1)");

            Assert.AreEqual(Math.Sin(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("sin(x)"));

            Assert.AreEqual("cos(x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("sin(2x)"));

            Assert.AreEqual("cos(2 * x) * 2", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // sin(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Sine(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("cos(2 * x) * 2", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sin(3 * x)", exp.ToString());
            Assert.AreEqual("cos(2 * x) * 2", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(sin(xy), x)").Differentiation();
            Assert.AreEqual("cos(x * y) * y", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTes2()
        {
            IMathExpression exp = parser.Parse("deriv(sin(xy), y)").Differentiation();
            Assert.AreEqual("cos(x * y) * x", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(sin(y), x)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("sin(2x)");

            Assert.AreEqual("sin(2 * x)", exp.ToString());
        }

    }

}
