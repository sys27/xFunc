using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SubtractionMathExpressionTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = parser.Parse("1 - 2");

            Assert.AreEqual(-1, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("x - sin(x)"));

            Assert.AreEqual("1 - cos(x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            NumberMathExpression num1 = new NumberMathExpression(2);
            VariableMathExpression x = new VariableMathExpression('x');
            MultiplicationMathExpression mul1 = new MultiplicationMathExpression(num1, x);

            NumberMathExpression num2 = new NumberMathExpression(3);
            MultiplicationMathExpression mul2 = new MultiplicationMathExpression(num2, x.Clone());

            IMathExpression exp = new SubtractionMathExpression(mul1, mul2);
            IMathExpression deriv = MathParser.Derivative(exp);

            Assert.AreEqual("-1", deriv.ToString());

            num1.Number = 5;
            num2.Number = 4;
            Assert.AreEqual("(5 * x) - (4 * x)", exp.ToString());
            Assert.AreEqual("-1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(xy - y, y)").Derivative();
            Assert.AreEqual("x - 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(x - y, x)").Derivative();
            Assert.AreEqual("1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(x - y, y)").Derivative();
            Assert.AreEqual("-1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IMathExpression exp = parser.Parse("deriv(x - 1, y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("(1 - x) * 5");

            Assert.AreEqual("(1 - x) * 5", exp.ToString());
        }

    }

}
