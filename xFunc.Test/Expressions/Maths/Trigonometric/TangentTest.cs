using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class TangentTest
    {

        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Tan(new Number(1));

            Assert.AreEqual(Math.Tan(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Tan(new Number(1));

            Assert.AreEqual(Math.Tan(1 * Math.PI / 180), exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Tan(new Number(1));

            Assert.AreEqual(Math.Tan(1 * Math.PI / 200), exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Tan(new Variable("x"));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("1 / (cos(x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            IExpression exp = new Tan(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest3()
        {
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Tan(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());

            num.Value = 5;
            Assert.AreEqual("tan(5 * x)", exp.ToString());
            Assert.AreEqual("(2 * 1) / (cos(2 * x) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest1()
        {
            IExpression exp = new Tan(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate();
            Assert.AreEqual("(1 * y) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest2()
        {
            IExpression exp = new Tan(new Mul(new Variable("x"), new Variable("y")));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("(x * 1) / (cos(x * y) ^ 2)", deriv.ToString());
        }

        [TestMethod]
        public void PartialDerivativeTest3()
        {
            IExpression exp = new Tan(new Variable("x"));
            IExpression deriv = exp.Differentiate(new Variable("y"));
            Assert.AreEqual("0", deriv.ToString());
        }

    }

}
