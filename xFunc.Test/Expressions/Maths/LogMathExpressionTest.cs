using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Library;
using xFunc.Library.Expressions.Maths;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LogMathExpressionTest
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
            IMathExpression exp = parser.Parse("log(10, 2)");

            Assert.AreEqual(Math.Log(10, 2), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("log(x, 2)"));

            Assert.AreEqual("1 / (x * ln(2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(log(x, 2), x)").Derivative();
            Assert.AreEqual("1 / (x * ln(2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(log(x, 2), y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("log(10, 2)");

            Assert.AreEqual("log(10, 2)", exp.ToString());
        }

    }

}
