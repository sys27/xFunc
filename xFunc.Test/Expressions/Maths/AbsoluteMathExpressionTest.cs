using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AbsoluteMathExpressionTest
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
            IMathExpression exp = parser.Parse("abs(-1)");

            Assert.AreEqual(1, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = MathParser.Derivative(parser.Parse("abs(x)"));

            Assert.AreEqual("x / abs(x)", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            NumberMathExpression num = new NumberMathExpression(2);
            VariableMathExpression x = new VariableMathExpression('x');
            MultiplicationMathExpression mul = new MultiplicationMathExpression(num, x);

            IMathExpression exp = new AbsoluteMathExpression(mul);
            IMathExpression deriv = MathParser.Derivative(exp);

            Assert.AreEqual("2 * ((2 * x) / abs(2 * x))", deriv.ToString());

            num.Number = 3;
            Assert.AreEqual("abs(3 * x)", exp.ToString());
            Assert.AreEqual("2 * ((2 * x) / abs(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = parser.Parse("deriv(abs(xy), x)").Derivative();
            Assert.AreEqual("y * ((x * y) / abs(x * y))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = parser.Parse("deriv(abs(xy), y)").Derivative();
            Assert.AreEqual("x * ((x * y) / abs(x * y))", exp.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = parser.Parse("deriv(abs(x), y)").Derivative();
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void EqualsTest1()
        {
            VariableMathExpression x1 = 'x';
            NumberMathExpression num1 = 2;
            MultiplicationMathExpression mul1 = new MultiplicationMathExpression(num1, x1);
            AbsoluteMathExpression abs1 = new AbsoluteMathExpression(mul1);

            VariableMathExpression x2 = 'x';
            NumberMathExpression num2 = 2;
            MultiplicationMathExpression mul2 = new MultiplicationMathExpression(num2, x2);
            AbsoluteMathExpression abs2 = new AbsoluteMathExpression(mul2);

            Assert.IsTrue(abs1.Equals(abs2));
            Assert.IsTrue(abs1.Equals(abs1));
        }

        [TestMethod]
        public void EqualsTest2()
        {
            VariableMathExpression x1 = 'x';
            NumberMathExpression num1 = 2;
            MultiplicationMathExpression mul1 = new MultiplicationMathExpression(num1, x1);
            AbsoluteMathExpression abs1 = new AbsoluteMathExpression(mul1);

            VariableMathExpression x2 = 'x';
            NumberMathExpression num2 = 3;
            MultiplicationMathExpression mul2 = new MultiplicationMathExpression(num2, x2);
            AbsoluteMathExpression abs2 = new AbsoluteMathExpression(mul2);

            Assert.IsFalse(abs1.Equals(abs2));
            Assert.IsFalse(abs1.Equals(mul2));
            Assert.IsFalse(abs1.Equals(null));
        }

        [TestMethod]
        public void ToStringTest()
        {
            IMathExpression exp = parser.Parse("abs(-1)");

            Assert.AreEqual("abs(-1)", exp.ToString());
        }

    }

}
