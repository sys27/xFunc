using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test
{

    [TestClass]
    public class DifferentiatorTest
    {

        private Differentiator differentiator;

        [TestInitializeAttribute]
        public void TestInit()
        {
            differentiator = new Differentiator();
        }

        private IExpression Differentiate(IExpression exp)
        {
            return differentiator.Differentiate(exp);
        }

        private IExpression Differentiate(IExpression exp, Variable variable)
        {
            return differentiator.Differentiate(exp, variable);
        }

        #region Common

        [TestMethod]
        public void AbsDerivativeTest1()
        {
            IExpression exp = Differentiate(new Abs(new Variable("x")));

            Assert.AreEqual("1 * (x / abs(x))", exp.ToString());
        }

        [TestMethod]
        public void AbsDerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Abs(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * ((2 * x) / abs(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("abs(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * ((2 * x) / abs(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void AbsPartialDerivativeTest1()
        {
            IExpression exp = new Abs(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [TestMethod]
        public void AbsPartialDerivativeTest2()
        {
            IExpression exp = new Abs(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [TestMethod]
        public void AbsPartialDerivativeTest3()
        {
            IExpression deriv = Differentiate(new Abs(new Variable("x")), new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void AddDerivativeTest1()
        {
            var exp = new Add(new Mul(new Number(2), new Variable("x")), new Number(3));
            var deriv = Differentiate(exp);

            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void AddDerivativeTest2()
        {
            var exp = new Add(new Mul(new Number(2), new Variable("x")), new Mul(new Number(3), new Variable("x")));
            var deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) + (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void AddDerivativeTest3()
        {
            // 2x + 3
            var num1 = new Number(2);
            var x = new Variable("x");
            var mul1 = new Mul(num1, x);

            var num2 = new Number(3);

            var exp = new Add(mul1, num2);
            var deriv = Differentiate(exp);

            Assert.AreEqual("2 * 1", deriv.ToString());

            num1.Value = 5;
            Assert.AreEqual("(5 * x) + 3", exp.ToString());
            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void AddPartialDerivativeTest1()
        {
            var exp = new Add(new Add(new Mul(new Variable("x"), new Variable("y")), new Variable("x")), new Variable("y"));
            var deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) + 1", deriv.ToString());
        }

        [TestMethod]
        public void AddPartialDerivativeTest2()
        {
            var exp = new Add(new Add(new Mul(new Variable("x"), new Variable("y")), new Variable("x")), new Variable("y"));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) + 1", deriv.ToString());
        }

        [TestMethod]
        public void AddPartialDerivativeTest3()
        {
            var exp = new Add(new Variable("x"), new Number(1));
            var deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void DivDerivativeTest1()
        {
            IExpression exp = new Div(new Number(1), new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(1 * 1) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DivDerivativeTest2()
        {
            // sin(x) / x
            IExpression exp = new Div(new Sin(new Variable("x")), new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(((cos(x) * 1) * x) - (sin(x) * 1)) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DivDerivativeTest3()
        {
            // (2x) / (3x)
            Number num1 = new Number(2);
            Variable x = new Variable("x");
            Mul mul1 = new Mul(num1, x);

            Number num2 = new Number(3);
            Mul mul2 = new Mul(num2, x.Clone());

            IExpression exp = new Div(mul1, mul2);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(((2 * 1) * (3 * x)) - ((2 * x) * (3 * 1))) / ((3 * x) ^ 2)", deriv.ToString());

            num1.Value = 4;
            num2.Value = 5;
            Assert.AreEqual("(4 * x) / (5 * x)", exp.ToString());
            Assert.AreEqual("(((2 * 1) * (3 * x)) - ((2 * x) * (3 * 1))) / ((3 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DivPartialDerivativeTest1()
        {
            // (y + x ^ 2) / x
            IExpression exp = new Div(new Add(new Variable("y"), new Pow(new Variable("x"), new Number(2))), new Variable("x"));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(((1 * (2 * (x ^ (2 - 1)))) * x) - ((y + (x ^ 2)) * 1)) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DivPartialDerivativeTest2()
        {
            IExpression exp = new Div(new Variable("y"), new Variable("x"));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("-(y * 1) / (x ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DivPartialDerivativeTest3()
        {
            IExpression exp = new Div(new Variable("y"), new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("1 / x", deriv.ToString());
        }

        [TestMethod]
        public void DivPartialDerivativeTest4()
        {
            // (x + 1) / x
            IExpression exp = new Div(new Add(new Variable("x"), new Number(1)), new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void ExpDerivativeTest1()
        {
            IExpression exp = new Exp(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 * exp(x)", deriv.ToString());
        }

        [TestMethod]
        public void ExpDerivativeTest2()
        {
            IExpression exp = new Exp(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void ExpDerivativeTest3()
        {
            // exp(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Exp(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("exp(6 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void ExpPartialDerivativeTest1()
        {
            IExpression exp = new Exp(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) * exp(x * y)", deriv.ToString());
        }

        [TestMethod]
        public void ExpPartialDerivativeTest2()
        {
            IExpression exp = new Exp(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) * exp(x * y)", deriv.ToString());
        }

        [TestMethod]
        public void ExpPartialDerivativeTest3()
        {
            IExpression exp = new Exp(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void LnDerivativeTest1()
        {
            IExpression exp = new Ln(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void LnDerivativeTest2()
        {
            // ln(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Ln(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (2 * x)", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("ln(5 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void LnPartialDerivativeTest1()
        {
            // ln(xy)
            IExpression exp = new Ln(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) / (x * y)", deriv.ToString());
        }

        [TestMethod]
        public void LnPartialDerivativeTest2()
        {
            // ln(xy)
            IExpression exp = new Ln(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) / (x * y)", deriv.ToString());
        }

        [TestMethod]
        public void LnPartialDerivativeTest3()
        {
            IExpression exp = new Ln(new Variable("y"));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void LgDerivativeTest1()
        {
            IExpression exp = new Lg(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / ((2 * x) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void LgDerivativeTest2()
        {
            // lg(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Lg(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / ((2 * x) * ln(10))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("lg(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / ((2 * x) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void LgPartialDerivativeTest1()
        {
            // lg(2xy)
            IExpression exp = new Lg(new Mul(new Mul(new Number(2), new Variable("x")), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("((2 * 1) * y) / (((2 * x) * y) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void LgPartialDerivativeTest2()
        {
            // lg(2xy)
            IExpression exp = new Lg(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void LogDerivativeTest1()
        {
            IExpression exp = new Log(new Variable("x"), new Number(2));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void LogDerivativeTest2()
        {
            // log(x, 2)
            Number num = new Number(2);
            Variable x = new Variable("x");

            IExpression exp = new Log(x, num);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("log(4, x)", exp.ToString());
            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void LogDerivativeTest3()
        {
            IExpression exp = new Log(new Number(2), new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(ln(2) * (1 / x)) / (ln(x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void LogPartialDerivativeTest1()
        {
            IExpression exp = new Log(new Variable("x"), new Number(2));
            IExpression deriv = Differentiate(exp, new Variable("x"));
            Assert.AreEqual("1 / (x * ln(2))", deriv.ToString());
        }

        [TestMethod]
        public void LogPartialDerivativeTest2()
        {
            IExpression exp = new Log(new Variable("x"), new Number(2));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void MulDerivativeTest1()
        {
            IExpression exp = new Mul(new Number(2), new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void MulDerivativeTest2()
        {
            // 2x
            Number num = new Number(2);
            Variable x = new Variable("x");

            IExpression exp = new Mul(num, x);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("2 * 1", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("3 * x", exp.ToString());
            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void MulPartialDerivativeTest1()
        {
            // (x + 1) * (y + x)
            IExpression exp = new Mul(new Add(new Variable("x"), new Number(1)), new Add(new Variable("y"), new Variable("x")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * (y + x)) + ((x + 1) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void MulPartialDerivativeTest2()
        {
            // (y + 1) * (3 + x)
            IExpression exp = new Mul(new Add(new Variable("y"), new Number(1)), new Add(new Number(3), new Variable("x")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("1 * (3 + x)", deriv.ToString());
        }

        [TestMethod]
        public void MulPartialDerivativeTest3()
        {
            // (x + 1) * (y + x)
            IExpression exp = new Mul(new Add(new Variable("x"), new Number(1)), new Add(new Variable("y"), new Variable("x")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x + 1) * 1", deriv.ToString());
        }

        [TestMethod]
        public void MulPartialDerivativeTest4()
        {
            // (x + 1) * (3 + x)
            IExpression exp = new Mul(new Add(new Variable("x"), new Number(1)), new Add(new Number(3), new Variable("x")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void PowDerivativeTest1()
        {
            IExpression exp = new Pow(new Variable("x"), new Number(3));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 * (3 * (x ^ (3 - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PowDerivativeTest2()
        {
            // 2 ^ (3x)
            IExpression exp = new Pow(new Number(2), new Mul(new Number(3), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PowDerivativeTest3()
        {
            // x ^ 3
            Variable x = new Variable("x");
            Number num1 = new Number(3);

            IExpression exp = new Pow(x, num1);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 * (3 * (x ^ (3 - 1)))", deriv.ToString());

            num1.Value = 4;
            Assert.AreEqual("x ^ 4", exp.ToString());
            Assert.AreEqual("1 * (3 * (x ^ (3 - 1)))", deriv.ToString());

            // 2 ^ (3x)
            Number num2 = new Number(2);
            num1 = new Number(3);
            Mul mul = new Mul(num1, x.Clone());

            exp = new Pow(num2, mul);
            deriv = Differentiate(exp);

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * (3 * 1)", deriv.ToString());

            num1.Value = 4;
            Assert.AreEqual("2 ^ (4 * x)", exp.ToString());
            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PowPartialDerivativeTest1()
        {
            // (yx) ^ 3
            IExpression exp = new Pow(new Mul(new Variable("y"), new Variable("x")), new Number(3));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(y * 1) * (3 * ((y * x) ^ (3 - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PowPartialDerivativeTest2()
        {
            // (yx) ^ 3
            IExpression exp = new Pow(new Mul(new Variable("y"), new Variable("x")), new Number(3));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(1 * x) * (3 * ((y * x) ^ (3 - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PowPartialDerivativeTest3()
        {
            IExpression exp = new Pow(new Variable("x"), new Number(3));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void RootDerivativeTest1()
        {
            IExpression exp = new Root(new Variable("x"), new Number(3));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 * ((1 / 3) * (x ^ ((1 / 3) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void RootDerivativeTest2()
        {
            // root(x, 3)
            Number num = new Number(3);
            Variable x = new Variable("x");

            IExpression exp = new Root(x, num);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 * ((1 / 3) * (x ^ ((1 / 3) - 1)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("root(x, 4)", exp.ToString());
            Assert.AreEqual("1 * ((1 / 3) * (x ^ ((1 / 3) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void RootPartialDerivativeTest1()
        {
            IExpression exp = new Root(new Mul(new Variable("x"), new Variable("y")), new Number(3));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) * ((1 / 3) * ((x * y) ^ ((1 / 3) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void RootPartialDerivativeTest2()
        {
            IExpression exp = new Root(new Variable("y"), new Number(3));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void SqrtDerivativeTest1()
        {
            IExpression exp = new Sqrt(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void SqrtDerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Sqrt(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sqrt(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (2 * sqrt(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void SqrtPartialDerivativeTest1()
        {
            // sqrt(2xy)
            IExpression exp = new Sqrt(new Mul(new Mul(new Number(2), new Variable("x")), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("((2 * 1) * y) / (2 * sqrt((2 * x) * y))", deriv.ToString());
        }

        [TestMethod]
        public void SqrtPartialDerivativeTest2()
        {
            IExpression exp = new Sqrt(new Variable("y"));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void SubDerivativeTest1()
        {
            // x - sin(x)
            IExpression exp = new Sub(new Variable("x"), new Sin(new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 - (cos(x) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void SubDerivativeTest2()
        {
            Number num1 = new Number(2);
            Variable x = new Variable("x");
            Mul mul1 = new Mul(num1, x);

            Number num2 = new Number(3);
            Mul mul2 = new Mul(num2, x.Clone());

            IExpression exp = new Sub(mul1, mul2);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) - (3 * 1)", deriv.ToString());

            num1.Value = 5;
            num2.Value = 4;
            Assert.AreEqual("(5 * x) - (4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) - (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void SubPartialDerivativeTest1()
        {
            IExpression exp = new Sub(new Mul(new Variable("x"), new Variable("y")), new Variable("y"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) - 1", deriv.ToString());
        }

        [TestMethod]
        public void SubPartialDerivativeTest2()
        {
            IExpression exp = new Sub(new Variable("x"), new Variable("y"));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("1", deriv.ToString());
        }

        [TestMethod]
        public void SubPartialDerivativeTest3()
        {
            IExpression exp = new Sub(new Variable("x"), new Variable("y"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("-1", deriv.ToString());
        }

        [TestMethod]
        public void SubPartialDerivativeTest4()
        {
            IExpression exp = new Sub(new Variable("x"), new Number(1));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        #endregion Common

        #region Trigonometric

        [TestMethod]
        public void SinDerivativeTest1()
        {
            IExpression exp = new Sin(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("cos(x) * 1", deriv.ToString());
        }

        [TestMethod]
        public void SinDerivativeTest2()
        {
            IExpression exp = new Sin(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void SinDerivativeTest3()
        {
            // sin(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Sin(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("sin(3 * x)", exp.ToString());
            Assert.AreEqual("cos(2 * x) * (2 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void SinPartialDerivativeTest1()
        {
            IExpression exp = new Sin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("cos(x * y) * (1 * y)", deriv.ToString());
        }

        [TestMethod]
        public void SinPartialDerivativeTest2()
        {
            IExpression exp = new Sin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("cos(x * y) * (x * 1)", deriv.ToString());
        }

        [TestMethod]
        public void SinPartialDerivativeTest3()
        {
            IExpression exp = new Sin(new Variable("y"));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void CosDerivativeTest1()
        {
            IExpression exp = new Cos(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(sin(x) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void CosDerivativeTest2()
        {
            IExpression exp = new Cos(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(sin(2 * x) * (2 * 1))", deriv.ToString());
        }

        [TestMethod]
        public void CosDerivativeTest3()
        {
            // cos(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Cos(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(sin(2 * x) * (2 * 1))", deriv.ToString());

            num.Value = 7;
            Assert.AreEqual("cos(7 * x)", exp.ToString());
            Assert.AreEqual("-(sin(2 * x) * (2 * 1))", deriv.ToString());
        }

        [TestMethod]
        public void CosPartialDerivativeTest1()
        {
            IExpression exp = new Cos(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("-(sin(x * y) * (1 * y))", deriv.ToString());
        }

        [TestMethod]
        public void CosPartialDerivativeTest2()
        {
            IExpression exp = new Cos(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("-(sin(x * y) * (x * 1))", deriv.ToString());
        }

        [TestMethod]
        public void CosPartialDerivativeTest3()
        {
            IExpression exp = new Cos(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void TanDerivativeTest1()
        {
            IExpression exp = new Tan(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 / (cos(x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void TanDerivativeTest2()
        {
            IExpression exp = new Tan(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void TanDerivativeTest3()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Tan(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("tan(5 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void TanPartialDerivativeTest1()
        {
            IExpression exp = new Tan(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void TanPartialDerivativeTest2()
        {
            IExpression exp = new Tan(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void TanPartialDerivativeTest3()
        {
            IExpression exp = new Tan(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void CotDerivativeTest1()
        {
            IExpression exp = new Cot(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(1 / (sin(x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void CotDerivativeTest2()
        {
            IExpression exp = new Cot(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void CotDerivativeTest3()
        {
            // cot(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Cot(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("cot(3 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (sin(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void CotPartialDerivativeTest1()
        {
            IExpression exp = new Cot(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("-((1 * y) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void CotPartialDerivativeTest2()
        {
            IExpression exp = new Cot(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("-((x * 1) / (sin(x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void CotPartialDerivativeTest3()
        {
            IExpression exp = new Cot(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void SecDerivativeTest1()
        {
            IExpression exp = new Sec(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void SecDerivativeTest2()
        {
            // sec(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Sec(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("sec(4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * (tan(2 * x) * sec(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void CscDerivativeTest()
        {
            IExpression exp = new Csc(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(2 * 1) * (cot(2 * x) * csc(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsinDerivativeTest1()
        {
            IExpression exp = new Arcsin(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 / sqrt(1 - (x ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsinDerivativeTest2()
        {
            IExpression exp = new Arcsin(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / sqrt(1 - ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsinDerivativeTest3()
        {
            // arcsin(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arcsin(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / sqrt(1 - ((2 * x) ^ 2))", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("arcsin(5 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / sqrt(1 - ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsinPartialDerivativeTest1()
        {
            IExpression exp = new Arcsin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) / sqrt(1 - ((x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsinPartialDerivativeTest2()
        {
            IExpression exp = new Arcsin(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) / sqrt(1 - ((x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsinPartialDerivativeTest3()
        {
            IExpression exp = new Arcsin(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void ArccosDerivativeTest1()
        {
            IExpression exp = new Arccos(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(1 / sqrt(1 - (x ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccosDerivativeTest2()
        {
            IExpression exp = new Arccos(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / sqrt(1 - ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccosDerivativeTest3()
        {
            // arccos(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arccos(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / sqrt(1 - ((2 * x) ^ 2)))", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("arccos(6 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / sqrt(1 - ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccosPartialDerivativeTest1()
        {
            IExpression exp = new Arccos(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("-((1 * y) / sqrt(1 - ((x * y) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccosPartialDerivativeTest2()
        {
            IExpression exp = new Arccos(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("-((x * 1) / sqrt(1 - ((x * y) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccosPartialDerivativeTest3()
        {
            IExpression exp = new Arccos(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void ArctanDerivativeTest1()
        {
            IExpression exp = new Arctan(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("1 / (1 + (x ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArctanDerivativeTest2()
        {
            IExpression exp = new Arctan(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (1 + ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArctanDerivativeTest3()
        {
            // arctan(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arctan(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (1 + ((2 * x) ^ 2))", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("arctan(6 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (1 + ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArctanPartialDerivativeTest1()
        {
            IExpression exp = new Arctan(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("(1 * y) / (1 + ((x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArctanPartialDerivativeTest2()
        {
            IExpression exp = new Arctan(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("(x * 1) / (1 + ((x * y) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArctanPartialDerivativeTest3()
        {
            IExpression exp = new Arctan(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void ArccotDerivativeTest1()
        {
            IExpression exp = new Arccot(new Variable("x"));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-(1 / (1 + (x ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccotDerivativeTest2()
        {
            IExpression exp = new Arccot(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (1 + ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccotDerivativeTest3()
        {
            // arccot(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arccot(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (1 + ((2 * x) ^ 2)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arccot(4 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (1 + ((2 * x) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccotPartialDerivativeTest1()
        {
            IExpression exp = new Arccot(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp);
            Assert.AreEqual("-((1 * y) / (1 + ((x * y) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccotPartialDerivativeTest2()
        {
            IExpression exp = new Arccot(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("-((x * 1) / (1 + ((x * y) ^ 2)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccotPartialDerivativeTest3()
        {
            IExpression exp = new Arccot(new Variable("x"));
            IExpression deriv = Differentiate(exp, new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void ArcsecDerivativeTest1()
        {
            IExpression exp = new Arcsec(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());
        }

        [TestMethod]
        public void ArcsecDerivativeTest2()
        {
            // arcsec(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arcsec(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arcsec(4 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1))", deriv.ToString());
        }

        [TestMethod]
        public void ArccscDerivativeTest1()
        {
            IExpression exp = new Arccsc(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void ArccscDerivativeTest2()
        {
            // arccsc(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arccsc(mul);
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arccsc(4 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());
        }

        #endregion Trigonometric

        #region Hyperbolic

        [TestMethod]
        public void SinhDerivativeTest()
        {
            IExpression exp = new Sinh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * cosh(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void CoshDerivativeTest()
        {
            IExpression exp = new Cosh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) * sinh(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void TanhDerivativeTest()
        {
            IExpression exp = new Tanh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (cosh(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void CothDerivativeTest()
        {
            IExpression exp = new Coth(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (sinh(2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void SechDerivativeTest()
        {
            IExpression exp = new Sech(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) * (tanh(2 * x) * sech(2 * x)))", deriv.ToString());
        }

        [TestMethod]
        public void CschDerivativeTest()
        {
            IExpression exp = new Csch(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) * (coth(2 * x) * csch(2 * x)))", deriv.ToString());
        }

        [TestMethod]
        public void ArsinehDerivativeTest()
        {
            IExpression exp = new Arsinh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / sqrt(((2 * x) ^ 2) + 1)", deriv.ToString());
        }

        [TestMethod]
        public void ArcoshDerivativeTest()
        {
            IExpression exp = new Arcosh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / sqrt(((2 * x) ^ 2) - 1)", deriv.ToString());
        }

        [TestMethod]
        public void ArtanhDerivativeTest()
        {
            IExpression exp = new Artanh(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (1 - ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArcothDerivativeTest()
        {
            IExpression exp = new Arcoth(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("(2 * 1) / (1 - ((2 * x) ^ 2))", deriv.ToString());
        }

        [TestMethod]
        public void ArsechDerivativeTest()
        {
            IExpression exp = new Arsech(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / ((2 * x) * sqrt(1 - ((2 * x) ^ 2))))", deriv.ToString());
        }

        [TestMethod]
        public void ArcschDerivativeTest()
        {
            IExpression exp = new Arcsch(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = Differentiate(exp);

            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(1 + ((2 * x) ^ 2))))", deriv.ToString());
        }

        #endregion Hyperbolic

    }

}
