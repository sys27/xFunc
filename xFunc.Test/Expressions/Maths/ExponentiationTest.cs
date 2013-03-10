using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExponentiationTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Exponentiation(new Number(2), new Number(10));

            Assert.AreEqual(1024, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Exponentiation(new Variable('x'), new Number(3));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 * (3 * (x ^ (3 - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // 2 ^ (3x)
            IMathExpression exp = new Exponentiation(new Number(2), new Multiplication(new Number(3), new Variable('x')));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // x ^ 3
            Variable x = new Variable('x');
            Number num1 = new Number(3);

            IMathExpression exp = new Exponentiation(x, num1);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 * (3 * (x ^ (3 - 1)))", deriv.ToString());

            num1.Value = 4;
            Assert.AreEqual("x ^ 4", exp.ToString());
            Assert.AreEqual("1 * (3 * (x ^ (3 - 1)))", deriv.ToString());

            // 2 ^ (3x)
            Number num2 = new Number(2);
            num1 = new Number(3);
            Multiplication mul = new Multiplication(num1, x.Clone());

            exp = new Exponentiation(num2, mul);
            deriv = exp.Differentiate();

            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * (3 * 1)", deriv.ToString());

            num1.Value = 4;
            Assert.AreEqual("2 ^ (4 * x)", exp.ToString());
            Assert.AreEqual("(ln(2) * (2 ^ (3 * x))) * (3 * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            // (yx) ^ 3
            IMathExpression exp = new Exponentiation(new Multiplication(new Variable('y'), new Variable('x')), new Number(3));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("(y * 1) * (3 * ((y * x) ^ (3 - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            // (yx) ^ 3
            IMathExpression exp = new Exponentiation(new Multiplication(new Variable('y'), new Variable('x')), new Number(3));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("(1 * x) * (3 * ((y * x) ^ (3 - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Exponentiation(new Variable('x'), new Number(3));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
