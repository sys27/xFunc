using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExpTest
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
            IMathExpression exp = parser.Parse("exp(2)");

            Assert.AreEqual(Math.Exp(2), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("exp(x)"));

            Assert.AreEqual("exp(x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("exp(2x)"));

            Assert.AreEqual("2 * exp(2 * x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // exp(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Exponential(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("2 * exp(2 * x)", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("exp(6 * x)", exp.ToString());
            Assert.AreEqual("2 * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(exp(xy), x)").Differentiation();
            Assert.AreEqual("y * exp(x * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(exp(xy), y)").Differentiation();
            Assert.AreEqual("x * exp(x * y)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(exp(x), y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("exp(2)");

            Assert.AreEqual("exp(2)", exp.ToString());
        }

    }

}
