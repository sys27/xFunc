using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class SubTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Sub(new Number(1), new Number(2));

            Assert.AreEqual(-1.0, exp.Calculate());
        }

        [TestMethod]
        public void SubTwoVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });
            var sub = new Sub(vector1, vector2);

            var expected = new Vector(new[] { new Number(-5), new Number(2) });
            var result = sub.Calculate();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SubTwoMatricesTest()
        {
            var matrix1 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(6), new Number(3) }), 
                new Vector(new[] { new Number(2), new Number(1) }) 
            });
            var matrix2 = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(9), new Number(2) }), 
                new Vector(new[] { new Number(4), new Number(3) }) 
            });
            var sub = new Sub(matrix1, matrix2);

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(-3), new Number(1) }), 
                new Vector(new[] { new Number(-2), new Number(-2) }) 
            });
            var result = sub.Calculate();

            Assert.AreEqual(expected, result);
        }
        
    }

}
