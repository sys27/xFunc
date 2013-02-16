using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class CotangentTest
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
            IMathExpression exp = parser.Parse("cot(1)");

            Assert.AreEqual(MathExtentions.Cot(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("cot(1)");

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("cot(1)");

            Assert.AreEqual(MathExtentions.Cot(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("cot(x)"));

            Assert.AreEqual("-(1 / (sin(x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("cot(2x)"));

            Assert.AreEqual("-(2 / (sin(2 * x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // cot(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Cotangent(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("-(2 / (sin(2 * x) ^ 2))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("cot(3 * x)", exp.ToString());
            Assert.AreEqual("-(2 / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(cot(xy), x)").Differentiation();
            Assert.AreEqual("-(y / (sin(x * y) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(cot(xy), y)").Differentiation();
            Assert.AreEqual("-(x / (sin(x * y) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(cot(x), y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("cot(2x)");

            Assert.AreEqual("cot(2 * x)", exp.ToString());
        }

    }

}
