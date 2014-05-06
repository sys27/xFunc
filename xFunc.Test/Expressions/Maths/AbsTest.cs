using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AbsTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Abs(new Number(-1));

            Assert.AreEqual(1.0, exp.Calculate());
        }

        [TestMethod]
        public void EqualsTest1()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 2;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.IsTrue(abs1.Equals(abs2));
            Assert.IsTrue(abs1.Equals(abs1));
        }

        [TestMethod]
        public void EqualsTest2()
        {
            Variable x1 = "x";
            Number num1 = 2;
            Mul mul1 = new Mul(num1, x1);
            Abs abs1 = new Abs(mul1);

            Variable x2 = "x";
            Number num2 = 3;
            Mul mul2 = new Mul(num2, x2);
            Abs abs2 = new Abs(mul2);

            Assert.IsFalse(abs1.Equals(abs2));
            Assert.IsFalse(abs1.Equals(mul2));
            Assert.IsFalse(abs1.Equals(null));
        }

    }

}
