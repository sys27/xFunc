using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExponentiationTest
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
            IMathExpression exp = parser.Parse("2^10");

            Assert.AreEqual(1024, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("x^3"));

            Assert.AreEqual("3 * (x ^ 2)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("2^(3x)"));

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * 3", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // x ^ 3
            Variable x = new Variable('x');
            Number num1 = new Number(3);

            IMathExpression exp = new Exponentiation(x, num1);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("3 * (x ^ 2)", deriv.ToString());

            num1.Value = 4;
            Assert.AreEqual("x ^ 4", exp.ToString());
            Assert.AreEqual("3 * (x ^ 2)", deriv.ToString());

            // 2 ^ (3x)
            Number num2 = new Number(2);
            num1 = new Number(3);
            Multiplication mul = new Multiplication(num1, x.Clone());

            exp = new Exponentiation(num2, mul);
            deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * 3", deriv.ToString());

            num1.Value = 4;
            Assert.AreEqual("2 ^ (4 * x)", exp.ToString());
            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * 3", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv((y * x) ^ 3, x)").Differentiation();
            Assert.AreEqual("y * (3 * ((y * x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv((y * x) ^ 3, y)").Differentiation();
            Assert.AreEqual("x * (3 * ((y * x) ^ 2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(y ^ 3, x)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest1()
        {
            IMathExpression exp = parser.Parse("x^10+1");

            Assert.AreEqual("(x ^ 10) + 1", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest2()
        {
            IMathExpression exp = parser.Parse("2^(3x)");

            Assert.AreEqual("2 ^ (3 * x)", exp.ToString());
        }

    }

}
