using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths.Trigonometric
{

    [TestClass]
    public class ArccscTest
    {
        
        [TestMethod]
        public void CalculateRadianTest()
        {
            IExpression exp = new Arccsc(new Number(1));

            Assert.AreEqual(MathExtentions.Acsc(1), exp.Calculate(AngleMeasurement.Radian));
        }

        [TestMethod]
        public void CalculateDegreeTest()
        {
            IExpression exp = new Arccsc(new Number(1));

            Assert.AreEqual(MathExtentions.Acsc(1) / Math.PI * 180, exp.Calculate(AngleMeasurement.Degree));
        }

        [TestMethod]
        public void CalculateGradianTest()
        {
            IExpression exp = new Arccsc(new Number(1));

            Assert.AreEqual(MathExtentions.Acsc(1) / Math.PI * 200, exp.Calculate(AngleMeasurement.Gradian));
        }

        [TestMethod]
        public void DerivativeTest1()
        {
            IExpression exp = new Arccsc(new Mul(new Number(2), new Variable("x")));
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());
        }

        [TestMethod]
        public void DerivativeTest2()
        {
            // arccsc(2x)
            Number num = new Number(2);
            Variable x = new Variable("x");
            Mul mul = new Mul(num, x);

            IExpression exp = new Arccsc(mul);
            IExpression deriv = exp.Differentiate();

            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());

            num.Value = 4;
            Assert.AreEqual("arccsc(4 * x)", exp.ToString());
            Assert.AreEqual("-((2 * 1) / (abs(2 * x) * sqrt(((2 * x) ^ 2) - 1)))", deriv.ToString());
        }

    }

}
