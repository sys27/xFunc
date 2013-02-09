using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ArccscMathExpressionTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateRadianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("arccsc(1)");

            Assert.AreEqual(MathExtentions.Acsc(1), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Degree;
            IMathExpression exp = parser.Parse("arccsc(1)");

            Assert.AreEqual(MathExtentions.Acsc(1) / Math.PI * 180, exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            parser.AngleMeasurement = AngleMeasurement.Gradian;
            IMathExpression exp = parser.Parse("arccsc(1)");

            Assert.AreEqual(MathExtentions.Acsc(1) / Math.PI * 200, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(arccsc(2x), x)").Differentiation();

            Assert.AreEqual("-(2 / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // arccsc(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Arccsc(mul);
            IMathExpression deriv = MathParser.Differentiation(exp);

            Assert.AreEqual("-(2 / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arccsc(4 * x)", exp.ToString());
            Assert.AreEqual("-(2 / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());
        }

    }

}
