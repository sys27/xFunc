using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LnTest
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
            IMathExpression exp = MathParser.Differentiation(parser.Parse("ln(2x)"));

            Assert.AreEqual("1 / x", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // ln(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Ln(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("1 / x", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("ln(5 * x)", exp.ToString());
            Assert.AreEqual("1 / x", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(ln(xy), x)").Differentiation();
            Assert.AreEqual("y / (x * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(ln(xy), y)").Differentiation();
            Assert.AreEqual("x / (x * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(ln(y), x)").Differentiation();
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
