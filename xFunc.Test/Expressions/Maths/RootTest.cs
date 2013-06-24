using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class RootTest
    {
        
        [TestMethod]
        public void CalculateRootTest1()
        {
            IMathExpression exp = new Root(new Number(8), new Number(3));

            Assert.AreEqual(Math.Pow(8, 1.0 / 3.0), exp.Calculate(null));
        }

        [TestMethod]
        public void CalculateRootTest2()
        {
            IMathExpression exp = new Root(new Number(-8), new Number(3));

            Assert.AreEqual(-2, exp.Calculate(null));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IMathExpression exp = new Root(new Variable("x"), new Number(3));
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 * ((1 / 3) * (x ^ ((1 / 3) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // root(x, 3)
            Number num = new Number(3);
            Variable x = new Variable("x");

            IMathExpression exp = new Root(x, num);
            IMathExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 * ((1 / 3) * (x ^ ((1 / 3) - 1)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("root(x, 4)", exp.ToString());
            Assert.AreEqual("1 * ((1 / 3) * (x ^ ((1 / 3) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IMathExpression exp = new Root(new Mul(new Variable("x"), new Variable("y")), new Number(3));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) * ((1 / 3) * ((x * y) ^ ((1 / 3) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IMathExpression exp = new Root(new Variable("y"), new Number(3));
            IMathExpression deriv = exp.Differentiate();
            Assert.AreEqual("0", deriv.ToString());
        }
        
    }

}
