using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

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

        #endregion Common

    }

}
