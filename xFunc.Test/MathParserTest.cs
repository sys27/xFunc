using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test
{

    [TestClass]
    public class MathParserTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void ParseTest()
        {
            IMathExpression exp = parser.Parse("1 + x * sin(x)");

            Assert.AreEqual("1 + (x * sin(x))", exp.ToString());
        }

        [TestMethod]
        public void RPNTest()
        {
            IMathExpression exp = parser.Parse("log(x^2, 4)");

            Assert.AreEqual("log((x ^ 2), 4)", exp.ToString());
            Assert.AreEqual(1, exp.Calculate(new MathParameterCollection() { { 'x', 2 } }));
        }

        [TestMethod]
        public void AngleTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("sin(x)");

            Assert.AreEqual(AngleMeasurement.Degree, ((SineMathExpression)exp).AngleMeasurement);
        }

        [TestMethod]
        public void DerivativeTest()
        {
            IMathExpression exp = parser.Parse("deriv(xy + y + x, x)");

            Assert.AreEqual("deriv(((x * y) + y) + x, x)", exp.ToString());
            Assert.AreEqual("y + 1", exp.Derivative().ToString());


            exp = parser.Parse("deriv(xy + y + x, y)");

            Assert.AreEqual("deriv(((x * y) + y) + x, y)", exp.ToString());
            Assert.AreEqual("x + 1", exp.Derivative().ToString());


            exp = parser.Parse("deriv(x * (y + 1) + y + x, x)");

            Assert.AreEqual("y + 2", exp.Derivative().ToString());


            exp = parser.Parse("deriv(y / x, x)");

            Assert.AreEqual("deriv(y / x, x)", exp.ToString());
            Assert.AreEqual("-y / (x ^ 2)", exp.Derivative().ToString());


            exp = parser.Parse("deriv(y / x, y)");

            Assert.AreEqual("deriv(y / x, y)", exp.ToString());
            Assert.AreEqual("1 / x", exp.Derivative().ToString());
        }

    }

}
