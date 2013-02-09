using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

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
        public void DerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(sec(2x), x)").Differentiation();

            Assert.AreEqual("2 * (tan(2 * x) * sec(2 * x))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // sec(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Secant(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("2 * (tan(2 * x) * sec(2 * x))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("sec(4 * x)", exp.ToString());
            Assert.AreEqual("2 * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

    }
}
