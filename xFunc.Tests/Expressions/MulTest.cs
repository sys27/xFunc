// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class MulTest
    {

        [Fact]
        public void ExecuteMulNumberByNumberTest()
        {
            var exp = new Mul(new Number(2), new Number(2));

            Assert.Equal(4.0, exp.Execute());
        }

        [Fact]
        public void ExecuteMulComplexByComplexTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));
            var expected = new Complex(-4, 19);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulComplexByNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new Number(2));
            var expected = new Complex(4, 10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberByComplexTest()
        {
            var exp = new Mul(new Number(2), new ComplexNumber(3, 2));
            var expected = new Complex(6, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberBySqrtComplexTest()
        {
            var exp = new Mul(new Number(2), new Sqrt(new Number(-9)));
            var expected = new Complex(0, 6);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteDotProductTest()
        {
            var vector1 = new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Maths.Expressions.Matrices.Vector(new[] { new Number(4), new Number(5), new Number(6) });
            var exp = new Mul(vector1, vector2);

            Assert.Equal(32.0, exp.Execute());
        }

        [Fact]
        public void ExecuteMulComplexByBool()
        {
            var complex = new ComplexNumber(3, 2);
            var boolean = new Bool(true);
            var mul = new Mul(complex, boolean);

            Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute());
        }

        [Fact]
        public void ExecuteMulBoolByComplex()
        {
            var boolean = new Bool(true);
            var complex = new ComplexNumber(3, 2);
            var mul = new Mul(boolean, complex);

            Assert.Throws<ResultIsNotSupportedException>(() => mul.Execute());
        }

        [Fact]
        public void ExecuteMulVectorByMatrixTest()
        {
            var vector = new Maths.Expressions.Matrices.Vector(new[]
            {
                new Number(1),
                new Number(2),
                new Number(3)
            });
            var matrix = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(5) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(6) })
            });
            var exp = new Mul(vector, matrix);

            var expected = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(32) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulMatrixByVectorTest()
        {
            var matrix = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(5) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(6) })
            });
            var vector = new Maths.Expressions.Matrices.Vector(new[]
            {
                new Number(1),
                new Number(2),
                new Number(3)
            });
            var exp = new Mul(matrix, vector);

            var expected = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(32) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulMatrixByMatrixTest()
        {
            var matrix1 = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2), new Number(3) })
            });
            var matrix2 = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(5) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(6) })
            });
            var exp = new Mul(matrix1, matrix2);

            var expected = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(32) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNunberByVectorTest()
        {
            var number = new Number(5);
            var vector = new Maths.Expressions.Matrices.Vector(new[]
            {
                new Number(1),
                new Number(2),
                new Number(3)
            });
            var exp = new Mul(number, vector);

            var expected = new Maths.Expressions.Matrices.Vector(new[]
            {
                new Number(5),
                new Number(10),
                new Number(15)
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulMatrixByNumberTest()
        {
            var matrix = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(3), new Number(4) })
            });
            var number = new Number(5);
            var exp = new Mul(matrix, number);

            var expected = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(5), new Number(10) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(15), new Number(20) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulNumberByMatrixTest()
        {
            var number = new Number(5);
            var matrix = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(3), new Number(4) })
            });
            var exp = new Mul(number, matrix);

            var expected = new Maths.Expressions.Matrices.Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(5), new Number(10) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(15), new Number(20) })
            });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteMulBoolByBoolTest()
        {
            var bool1 = new Bool(true);
            var bool2 = new Bool(true);
            var exp = new Mul(bool1, bool2);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Mul(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
