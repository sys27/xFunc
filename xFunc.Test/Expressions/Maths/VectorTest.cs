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
            var vector = new Vector(new[] { new Number(2), new Number(3) });
            var number = new Number(5);

            var expected = new Vector(new[] { new Number(10), new Number(15) });
            var result = vector.Mul(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AddVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });

            var expected = new Vector(new[] { new Number(9), new Number(4) });
            var result = vector1.Add(vector2);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(VectorIsInvalidException))]
        public void AddVectorsDiffSizeTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1), new Number(3) });

            var expected = new Vector(new[] { new Number(9), new Number(4) });
            var result = vector1.Add(vector2);
        }

    }

}
