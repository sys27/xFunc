using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class MultiplicationTest
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
            IMathExpression exp = parser.Parse("2 * 2");

            Assert.AreEqual(2 * 2, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("sin(2x)"));

            Assert.AreEqual("cos(2 * x) * 2", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // 2x
            Number num = new Number(2);
            Variable x = new Variable('x');

            IMathExpression exp = new Multiplication(num, x);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("2", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("3 * x", exp.ToString());
            Assert.AreEqual("2", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv((x + 1) * (y + x), x)").Differentiation();
            Assert.AreEqual("(y + x) + (x + 1)", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv((y + 1) * (3 + x), y)").Differentiation();
            Assert.AreEqual("3 + x", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv((x + 1) * (y + x), y)").Differentiation();
            Assert.AreEqual("x + 1", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            IMathExpression exp = parser.Parse("deriv((x + 1) * (3 + x), y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("2sin(x)");

            Assert.AreEqual("2 * sin(x)", exp.ToString());
        }

    }

}
