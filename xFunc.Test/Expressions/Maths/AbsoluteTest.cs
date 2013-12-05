using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AbsoluteTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Abs(new Number(-1));

            Assert.AreEqual(1.0, exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Abs(new Variable("x")).Differentiate();

            Assert.AreEqual("1 * (x / abs(x))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Abs(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * ((2 * x) / abs(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("abs(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * ((2 * x) / abs(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Abs(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Abs(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression deriv = new Abs(new Variable("x")).Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void EqualsTest1()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 2;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.IsTrue(abs1.Equals(abs2));
            Assert.IsTrue(abs1.Equals(abs1));
        }

        [TestMethod]
        public void EqualsTest2()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 3;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.IsFalse(abs1.Equals(abs2));
            Assert.IsFalse(abs1.Equals(mul2));
            Assert.IsFalse(abs1.Equals(null));
        }

    }

}
