using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExponentialTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Exponential(new Number(2));

            Assert.AreEqual(Math.Exp(2), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Exponential(new Variable('x'));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("1 * exp(x)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IMathExpression exp = new Exponential(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // exp(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Exponential(mul);
            IMathExpression deriv = exp.Differentiation();

            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("exp(6 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Exponential(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiation();
            Assert.AreEqual("(1 * y) * exp(x * y)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Exponential(new Multiplication(new Variable('x'), new Variable('y')));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("(x * 1) * exp(x * y)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IMathExpression exp = new Exponential(new Variable('x'));
            IMathExpression deriv = exp.Differentiation(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }
        
    }

}
