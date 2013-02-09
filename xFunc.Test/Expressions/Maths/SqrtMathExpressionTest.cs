using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SqrtMathExpressionTest
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
            IMathExpression exp = parser.Parse("sqrt(4)");

            Assert.AreEqual(Math.Sqrt(4), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("sqrt(2x)"));

            Assert.AreEqual("1 / sqrt(2 * x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Sqrt(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("1 / sqrt(2 * x)", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sqrt(3 * x)", exp.ToString());
            Assert.AreEqual("1 / sqrt(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(sqrt(2xy), x)").Differentiation();
            Assert.AreEqual("(2 * y) / (2 * sqrt((2 * x) * y))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(sqrt(y), x)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("sqrt(8)");

            Assert.AreEqual("sqrt(8)", exp.ToString());
        }

    }

}
