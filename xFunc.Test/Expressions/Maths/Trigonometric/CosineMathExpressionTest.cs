using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class CosineMathExpressionTest
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
            IMathExpression exp = parser.Parse("cos(1)");

            Assert.AreEqual(Math.Cos(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("cos(1)");

            Assert.AreEqual(Math.Cos(1 * Math.PI / 180), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("cos(1)");

            Assert.AreEqual(Math.Cos(1 * Math.PI / 200), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("cos(x)"));

            Assert.AreEqual("-sin(x)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(cos(xy), x)").Derivative();
            Assert.AreEqual("-(sin(x * y) * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(cos(xy), y)").Derivative();
            Assert.AreEqual("-(sin(x * y) * x)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(cos(x), y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("cos(2x)"));

            Assert.AreEqual("-(sin(2 * x) * 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // cos(2x)
            NumberMathExpression num = new NumberMathExpression(2);
            VariableMathExpression x = new VariableMathExpression('x');
            MultiplicationMathExpression mul = new MultiplicationMathExpression(num, x);

            IMathExpression exp = new CosineMathExpression(mul);
            IMathExpression deriv = MathParser.Derivative(exp);

            Assert.AreEqual("-(sin(2 * x) * 2)", deriv.ToString());

            num.Number = 7;
            Assert.AreEqual("cos(7 * x)", exp.ToString());
            Assert.AreEqual("-(sin(2 * x) * 2)", deriv.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("cos(2x)");

            Assert.AreEqual("cos(2 * x)", exp.ToString());
        }

    }

}
