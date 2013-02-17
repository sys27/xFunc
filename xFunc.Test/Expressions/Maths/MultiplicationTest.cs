using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class MultiplicationTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Multiplication(new Number(2), new Number(2));

            Assert.AreEqual(4, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Multiplication(new Number(2), new Variable('x'));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // 2x
            Number num = new Number(2);
            Variable x = new Variable('x');

            IMathExpression exp = new Multiplication(num, x);
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("2 * 1", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("3 * x", exp.ToString());
            Assert.AreEqual("2 * 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            // (x + 1) * (y + x)
            IMathExpression exp = new Multiplication(new Addition(new Variable('x'), new Number(1)), new Addition(new Variable('y'), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();
            Assert.AreEqual("(1 * (y + x)) + ((x + 1) * 1)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            // (y + 1) * (3 + x)
            IMathExpression exp = new Multiplication(new Addition(new Variable('y'), new Number(1)), new Addition(new Number(3), new Variable('x')));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("1 * (3 + x)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            // (x + 1) * (y + x)
            IMathExpression exp = new Multiplication(new Addition(new Variable('x'), new Number(1)), new Addition(new Variable('y'), new Variable('x')));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("(x + 1) * 1", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest4()
        {
            // (x + 1) * (3 + x)
            IMathExpression exp = new Multiplication(new Addition(new Variable('x'), new Number(1)), new Addition(new Number(3), new Variable('x')));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
