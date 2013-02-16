using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LogTest
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
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("log(x, 2)"));

            Assert.AreEqual("1 / (x * ln(2))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // log(x, 2)
            Number num = new Number(2);
            Variable x = new Variable('x');

            IMathExpression exp = new Log(x, num);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("log(x, 4)", exp.ToString());
            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(log(x, 2), x)").Differentiation();
            Assert.AreEqual("1 / (x * ln(2))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(log(x, 2), y)").Differentiation();
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
