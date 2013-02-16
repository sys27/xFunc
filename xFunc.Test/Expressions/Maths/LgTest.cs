using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LgTest
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
            IMathExpression exp = parser.Parse("lg(2)");

            Assert.AreEqual(Math.Log10(2), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Differentiation(parser.Parse("lg(2x)"));

            Assert.AreEqual("2 / ((2 * x) * ln(10))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // lg(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Lg(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("2 / ((2 * x) * ln(10))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("lg(3 * x)", exp.ToString());
            Assert.AreEqual("2 / ((2 * x) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(lg(2xy), x)").Differentiation();
            Assert.AreEqual("(2 * y) / (((2 * x) * y) * ln(10))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(lg(x), y)").Differentiation();
            Assert.AreEqual("0", exp.ToString());
        }
        
        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("lg(2)");

            Assert.AreEqual("lg(2)", exp.ToString());
        }

    }

}
