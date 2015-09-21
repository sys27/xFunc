using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class AddTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Add(new Number(1), new Number(2));

            Assert.Equal(3.0, exp.Calculate());
        }

        [Fact]
        public void CalculateTest1()
        {
            IExpression exp = new Add(new Number(-3), new Number(2));

            Assert.Equal(-1.0, exp.Calculate());
        }

        [Fact]
        public void AddTwoVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });
            var add = new Add(vector1, vector2);

            var expected = new Vector(new[] { new Number(9), new Number(4) });
            var result = add.Calculate();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddTwoMatricesTest()
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
            var add = new Add(matrix1, matrix2);

            var expected = new Matrix(new[] 
            { 
                new Vector(new[] { new Number(15), new Number(5) }), 
                new Vector(new[] { new Number(6), new Number(4) }) 
            });
            var result = add.Calculate();

            Assert.Equal(expected, result);
        }

    }

}
