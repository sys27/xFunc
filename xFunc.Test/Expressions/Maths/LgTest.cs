using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class LgTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new Lg(new Number(2));

            Assert.AreEqual(Math.Log10(2), exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Lg(new Multiplication(new Number(2), new Variable('x')));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / ((2 * x) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // lg(2x)
            Number num = new Number(2);
            Variable x = new Variable('x');
            Multiplication mul = new Multiplication(num, x);

            IMathExpression exp = new Lg(mul);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / ((2 * x) * ln(10))", deriv.ToString());

            num.Value = 3;
            Assert.AreEqual("lg(3 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / ((2 * x) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            // lg(2xy)
            IMathExpression exp = new Lg(new Multiplication(new Multiplication(new Number(2), new Variable('x')), new Variable('y')));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("((2 * 1) * y) / (((2 * x) * y) * ln(10))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            // lg(2xy)
            IMathExpression exp = new Lg(new Variable('x'));
            IMathExpression deriv = exp.Differentiate(new Variable('y'));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
