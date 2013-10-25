using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AdditionTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Add(new Number(1), new Number(2));

            Assert.AreEqual(3, exp.Calculate());
        }

        [TestMethod]
        public void CalculateTest1()
        {
            IMathExpression exp = new Add(new Number(-3), new Number(2));

            Assert.AreEqual(-1, exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Add(new Mul(new Number(2), new Variable("x")), new Number(3));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Add(new Mul(new Number(2), new Variable("x")), new Mul(new Number(3), new Variable("x")));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) + (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // 2x + 3
            Number num1 = new Number(2);
            Variable x = new Variable("x");
            Mul mul1 = new Mul(num1, x);

            Number num2 = new Number(3);

            IMathExpression exp = new Add(mul1, num2);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("2 * 1", deriv.ToString());

            num1.Value = 5;
            Assert.AreEqual("(5 * x) + 3", exp.ToString());
            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Add(new Add(new Mul(new Variable("x"), new Variable("y")), new Variable("x")), new Variable("y"));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) + 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Add(new Add(new Mul(new Variable("x"), new Variable("y")), new Variable("x")), new Variable("y"));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) + 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Add(new Variable("x"), new Number(1));
            IMathExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
