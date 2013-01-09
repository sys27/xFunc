using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library.Maths;
using xFunc.Library.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LnMathExpressionTest
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
            IMathExpression exp = parser.Parse("ln(2)");

            Assert.AreEqual(Math.Log(2, Math.E), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("ln(2x)"));

            Assert.AreEqual("1 / x", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // ln(2x)
            NumberMathExpression num = new NumberMathExpression(2);
            VariableMathExpression x = new VariableMathExpression('x');
            MultiplicationMathExpression mul = new MultiplicationMathExpression(num, x);

            IMathExpression exp = new LnMathExpression(mul);
            IMathExpression deriv = MathParser.Derivative(exp);

            Assert.AreEqual("1 / x", deriv.ToString());

            num.Number = 5;
            Assert.AreEqual("ln(5 * x)", exp.ToString());
            Assert.AreEqual("1 / x", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(ln(xy), x)").Derivative();
            Assert.AreEqual("y / (x * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(ln(xy), y)").Derivative();
            Assert.AreEqual("x / (x * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(ln(y), x)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("ln(2)");

            Assert.AreEqual("ln(2)", exp.ToString());
        }

    }

}
