using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class RootMathExpressionTest
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
            IMathExpression exp = parser.Parse("root(8, 3)");

            Assert.AreEqual(Math.Pow(8, 1.0 / 3.0), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("root(x, 3)"));

            Assert.AreEqual("(1 / 3) * (x ^ ((1 / 3) - 1))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // root(x, 3)
            NumberMathExpression num = new NumberMathExpression(3);
            VariableMathExpression x = new VariableMathExpression('x');

            IMathExpression exp = new RootMathExpression(x, num);
            IMathExpression deriv = MathParser.Derivative(exp);

            Assert.AreEqual("(1 / 3) * (x ^ ((1 / 3) - 1))", deriv.ToString());

            num.Number = 4;
            Assert.AreEqual("root(x, 4)", exp.ToString());
            Assert.AreEqual("(1 / 3) * (x ^ ((1 / 3) - 1))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(root(xy, 3), x)").Derivative();
            Assert.AreEqual("y * ((1 / 3) * ((x * y) ^ ((1 / 3) - 1)))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(root(y, 3), x)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("root(8, 3)");

            Assert.AreEqual("root(8, 3)", exp.ToString());
        }

    }

}
