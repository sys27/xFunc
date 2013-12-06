using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class VectorTest
    {

        [TestMethod]
        public void MulByNumberVectorTest()
        {
            var vector = new Vector(new[] { new Number(2), new Number(3) }, 2);
            var number = new Number(5);

            var expected = new Vector(new[] { new Number(10), new Number(15) }, 2);
            var result = vector.Mul(number);

            Assert.AreEqual(expected, result);
        }

    }

}
