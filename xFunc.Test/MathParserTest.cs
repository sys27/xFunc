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
        public void SimplifyNumberAndVariableTest()
        {
            IMathExpression exp = new NumberMathExpression(1);
            IMathExpression simple = MathParser.SimplifyExpressions(exp);

            Assert.AreEqual(exp, simple);

            exp = new VariableMathExpression('x');
            simple = MathParser.SimplifyExpressions(exp);

            Assert.AreEqual(exp, simple);
        }

        [TestMethod]
        public void SimplifyDoubleUnaryMinusTest()
        {
            IMathExpression exp = parser.Parse("-(-x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());
        }

        [TestMethod]
        public void SimplifyUnaryMinusBeforeNumberTest()
        {
            IMathExpression exp = parser.Parse("-(7+0)");

            Assert.IsNull(exp.Parent);
            Assert.IsTrue(exp is NumberMathExpression);
            Assert.AreEqual(-7, ((NumberMathExpression)exp).Number);
        }

        [TestMethod]
        public void SimplifyPlusZeroTest()
        {
            IMathExpression exp = parser.Parse("sin(x) + 0");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("sin(x)", exp.ToString());

            exp = parser.Parse("0 + sin(x)");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifyAddTwoNumbersTest()
        {
            IMathExpression exp = parser.Parse("4 + 5");

            Assert.IsNull(exp.Parent);
            Assert.IsTrue(exp is NumberMathExpression);
            Assert.AreEqual(9, ((NumberMathExpression)exp).Number);
        }

        [TestMethod]
        public void SimplifyAddFirstUnaryMinusTest()
        {
            IMathExpression exp = parser.Parse("-sin(x) + 6");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 - sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifyAddSecondUnaryMinusTest()
        {
            IMathExpression exp = parser.Parse("6 + (-sin(x))");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 - sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifySubZeroTest()
        {
            IMathExpression exp = parser.Parse("sin(x) - 0");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("sin(x)", exp.ToString());

            exp = parser.Parse("0 - sin(x)");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifySubTwoNumbersTest()
        {
            IMathExpression exp = parser.Parse("4 - 5");

            Assert.IsNull(exp.Parent);
            Assert.IsTrue(exp is NumberMathExpression);
            Assert.AreEqual(-1, ((NumberMathExpression)exp).Number);
        }

        [TestMethod]
        public void SimplifySubSecondUnaryMinusTest()
        {
            IMathExpression exp = parser.Parse("6 - (-sin(x))");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 + sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifyMulZeroTest()
        {
            IMathExpression exp = parser.Parse("sin(x) * 0");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        public void SimplifyMubOneTest()
        {
            IMathExpression exp = parser.Parse("sin(x) * 1");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("sin(x)", exp.ToString());

            exp = parser.Parse("1 * sin(x)");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifyZeroDivByTest()
        {
            IMathExpression exp = parser.Parse("0 / sin(x)");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("0", exp.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void SimplifyDivByZeroTest()
        {
            IMathExpression exp = parser.Parse("x / 0");
        }

        [TestMethod]
        public void SimplifyDivByOneTest()
        {
            IMathExpression exp = parser.Parse("x / 1");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());
        }

        [TestMethod]
        public void SimplifyInvZeroTest()
        {
            IMathExpression exp = parser.Parse("sin(x) ^ 0");
            Assert.IsNull(exp.Parent);
            Assert.AreEqual("1", exp.ToString());
        }

        [TestMethod]
        public void SimplifyInvOneTest()
        {
            IMathExpression exp = parser.Parse("sin(x) ^ 1");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("sin(x)", exp.ToString());
        }

        [TestMethod]
        public void SimplifyDiffAdd()
        {
            IMathExpression exp = parser.Parse("(2 + x) + 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x + 4", exp.ToString());

            exp = parser.Parse("(x + 2) + 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x + 4", exp.ToString());

            exp = parser.Parse("2 + (2 + x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x + 4", exp.ToString());

            exp = parser.Parse("2 + (x + 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x + 4", exp.ToString());

            exp = parser.Parse("-2 + (2 + x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("-2 + (x + 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("(2 - x) + 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("4 - x", exp.ToString());

            exp = parser.Parse("(x - 2) + 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("2 + (2 - x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("4 - x", exp.ToString());

            exp = parser.Parse("2 + (x - 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("-2 + (2 - x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-x", exp.ToString());

            exp = parser.Parse("-2 + (x - 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-4 + x", exp.ToString());
        }

        [TestMethod]
        public void SimplifyDiffSub()
        {
            IMathExpression exp = parser.Parse("(2 + x) - 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("(x + 2) - 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("2 - (2 + x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-x", exp.ToString());

            exp = parser.Parse("2 - (x + 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-x", exp.ToString());

            exp = parser.Parse("-2 - (2 + x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-4 - x", exp.ToString());

            exp = parser.Parse("-2 - (x + 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-4 - x", exp.ToString());

            exp = parser.Parse("(2 - x) - 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-x", exp.ToString());

            exp = parser.Parse("(x - 2) - 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x - 4", exp.ToString());

            exp = parser.Parse("2 - (2 - x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("2 - (x - 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("4 - x", exp.ToString());

            exp = parser.Parse("-2 - (2 - x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-4 + x", exp.ToString());

            exp = parser.Parse("-2 - (x - 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("-x", exp.ToString());
        }

        [TestMethod]
        public void SimplifyDiffMul()
        {
            IMathExpression exp = parser.Parse("(3 * x) * 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 * x", exp.ToString());

            exp = parser.Parse("(x * 3) * 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 * x", exp.ToString());

            exp = parser.Parse("2 * (3 * x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 * x", exp.ToString());

            exp = parser.Parse("2 * (x * 3)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 * x", exp.ToString());

            exp = parser.Parse("2 * (3 / x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 / x", exp.ToString());

            exp = parser.Parse("2 * (x / 3)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("0.666666666666667 * x", exp.ToString());

            exp = parser.Parse("(3 / x) * 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("6 / x", exp.ToString());

            exp = parser.Parse("(x / 3) * 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("0.666666666666667 * x", exp.ToString());
        }

        [TestMethod]
        public void SimplifyDiffDiv()
        {
            IMathExpression exp = parser.Parse("(2 * x) / 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("(x * 2) / 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("2 / (2 * x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("1 / x", exp.ToString());

            exp = parser.Parse("2 / (x * 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("1 / x", exp.ToString());

            exp = parser.Parse("(2 / x) / 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("1 / x", exp.ToString());

            exp = parser.Parse("(x / 2) / 2");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x / 4", exp.ToString());

            exp = parser.Parse("2 / (2 / x)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("x", exp.ToString());

            exp = parser.Parse("2 / (x / 2)");

            Assert.IsNull(exp.Parent);
            Assert.AreEqual("4 / x", exp.ToString());
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
