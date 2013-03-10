using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AbsoluteTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Absolute(new Number(-1));

            Assert.AreEqual(1, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Absolute(new Variable('x')).Differentiate();

            Assert.AreEqual("1 * (x / abs(x))", exp.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Absolute(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * ((2 * x) / abs(2 * x))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("abs(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * ((2 * x) / abs(2 * x))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Absolute(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Absolute(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("(x * 1) * ((x * y) / abs(x * y))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression deriv = new Absolute(new Variable('x')).Differentiate(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

        [TestMethod]
        public void EqualsTest1()
        {
            Variable x1 = 'x';
            Number num1 = 2;
            Multiplication mul1 = new Multiplication(num1, x1);
            Absolute abs1 = new Absolute(mul1);

            Variable x2 = 'x';
            Number num2 = 2;
            Multiplication mul2 = new Multiplication(num2, x2);
            Absolute abs2 = new Absolute(mul2);

            Assert.IsTrue(abs1.Equals(abs2));
            Assert.IsTrue(abs1.Equals(abs1));
        }

        [TestMethod]
        public void EqualsTest2()
        {
            Variable x1 = 'x';
            Number num1 = 2;
            Multiplication mul1 = new Multiplication(num1, x1);
            Absolute abs1 = new Absolute(mul1);

            Variable x2 = 'x';
            Number num2 = 3;
            Multiplication mul2 = new Multiplication(num2, x2);
            Absolute abs2 = new Absolute(mul2);

            Assert.IsFalse(abs1.Equals(abs2));
            Assert.IsFalse(abs1.Equals(mul2));
            Assert.IsFalse(abs1.Equals(null));
        }

    }

}
