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
            IMathExpression exp = MathParser.Differentiation(parser.Parse("x - sin(x)"));

            Assert.AreEqual("1 - cos(x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num1 = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul1 = new Multiplication(num1, x);

            Number num2 = new Number(3);
            Multiplication mul2 = new Multiplication(num2, x.Clone());

            IMathExpression exp = new Subtraction(mul1, mul2);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("-1", deriv.ToString());

            num1.Value = 5;
            num2.Value = 4;
            Assert.AreEqual("(5 * x) - (4 * x)", exp.ToString());
            Assert.AreEqual("-1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(xy - y, y)").Differentiation();
            Assert.AreEqual("x - 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(x - y, x)").Differentiation();
            Assert.AreEqual("1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(x - y, y)").Differentiation();
            Assert.AreEqual("-1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IMathExpression exp = parser.Parse("deriv(x - 1, y)").Differentiation();
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
