using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class CotangentMathExpressionTest
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
            IMathExpression exp = MathParser.Derivative(parser.Parse("cot(x)"));

            Assert.AreEqual("-(1 / (sin(x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("cot(2x)"));

            Assert.AreEqual("-(2 / (sin(2 * x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // cot(2x)
            NumberMathExpression num = new NumberMathExpression(2);
            VariableMathExpression x = new VariableMathExpression('x');
            MultiplicationMathExpression mul = new MultiplicationMathExpression(num, x);

            IMathExpression exp = new CotangentMathExpression(mul);
            IMathExpression deriv = MathParser.Derivative(exp);

            Assert.AreEqual("-(2 / (sin(2 * x) ^ 2))", deriv.ToString());

            num.Number = 3;
            Assert.AreEqual("cot(3 * x)", exp.ToString());
            Assert.AreEqual("-(2 / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(cot(xy), x)").Derivative();
            Assert.AreEqual("-(y / (sin(x * y) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(cot(xy), y)").Derivative();
            Assert.AreEqual("-(x / (sin(x * y) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(cot(x), y)").Derivative();
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
