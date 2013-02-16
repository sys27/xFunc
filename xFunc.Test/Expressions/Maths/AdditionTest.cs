using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AdditionTest
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
            IMathExpression exp = parser.Parse("1+2");

            Assert.AreEqual(3, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateTest1()
        {
            IMathExpression exp = parser.Parse("-3+2");

            Assert.AreEqual(-1, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("2x + 3"));

            Assert.AreEqual("2", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("2x + 3x"));

            Assert.AreEqual("5", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // 2x + 3
            Number num1 = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul1 = new Multiplication(num1, x);

            Number num2 = new Number(3);

            IMathExpression exp = new Addition(mul1, num2);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("2", deriv.ToString());

            num1.Value = 5;
            Assert.AreEqual("(5 * x) + 3", exp.ToString());
            Assert.AreEqual("2", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(xy + x + y, x)").Differentiation();
            Assert.AreEqual("y + 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(xy + x + y, y)").Differentiation();
            Assert.AreEqual("x + 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(x + 1, y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("sin(1)+2");

            Assert.AreEqual("sin(1) + 2", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest1()
        {
            IMathExpression exp = parser.Parse("sin(1) + x + 2");

            Assert.AreEqual("(sin(1) + x) + 2", exp.ToString());
        }

    }

}
