using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class DivisionMathExpressionTest
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
            IMathExpression exp = parser.Parse("1 / 2");

            Assert.AreEqual(1.0 / 2.0, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("1 / x"));

            Assert.AreEqual("-1 / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("sin(x) / x"));

            Assert.AreEqual("((cos(x) * x) - sin(x)) / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // (2x) / (3x)
            Number num1 = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul1 = new Multiplication(num1, x);

            Number num2 = new Number(3);
            Multiplication mul2 = new Multiplication(num2, x.Clone());

            IMathExpression exp = new Division(mul1, mul2);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("((6 * x) - (6 * x)) / ((3 * x) ^ 2)", deriv.ToString());

            num1.Value = 4;
            num2.Value = 5;
            Assert.AreEqual("(4 * x) / (5 * x)", exp.ToString());
            Assert.AreEqual("((6 * x) - (6 * x)) / ((3 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv((y + x ^ 2) / x, x)").Differentiation();
            Assert.AreEqual("(((2 * x) * x) - (y + (x ^ 2))) / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(y / x, x)").Differentiation();
            Assert.AreEqual("-y / (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(y / x, y)").Differentiation();
            Assert.AreEqual("1 / x", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IMathExpression exp = parser.Parse("deriv((x + 1) / x, y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("1 / 2");

            Assert.AreEqual("1 / 2", exp.ToString());
        }

    }

}
