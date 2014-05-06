using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExpTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Exp(new Number(2));

            Assert.AreEqual(Math.Exp(2), exp.Calculate());
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Exp(new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 * exp(x)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IExpression exp = new Exp(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            // exp(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Exp(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());

            num.Value = 6;
            Assert.AreEqual("exp(6 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) * exp(2 * x)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Exp(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) * exp(x * y)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Exp(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) * exp(x * y)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression exp = new Exp(new Variable("x"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }
        
    }

}
