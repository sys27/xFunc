// Copyright 2012-2018 Dmitry Kischenko
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
using System;
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions
{

    public class AddTest
    {

        [Fact]
        public void ExecuteTestNumber1()
        {
            var exp = new Add(new Number(1), new Number(2));

            Assert.Equal(3.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTestNumber2()
        {
            var exp = new Add(new Number(-3), new Number(2));

            Assert.Equal(-1.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTestComplexNumber()
        {
            var exp = new Add(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
            var expected = new Complex(9, 7);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestNumberComplexNumber()
        {
            var exp = new Add(new Number(7), new ComplexNumber(2, 4));
            var expected = new Complex(9, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTestComplexNumberNumber()
        {
            var exp = new Add(new ComplexNumber(7, 3), new Number(2));
            var expected = new Complex(9, 3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Add(new Number(2), new Sqrt(new Number(-9)));
            var expected = new Complex(2, 3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void AddTwoVectorsTest()
        {
            var vector1 = new Maths.Expressions.Matrices.Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Maths.Expressions.Matrices.Vector(new[] { new Number(7), new Number(1) });
            var add = new Add(vector1, vector2);

            var expected = new Maths.Expressions.Matrices.Vector(new[] { new Number(9), new Number(4) });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddTwoMatricesTest()
        {
            var matrix1 = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(6), new Number(3) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(2), new Number(1) })
            });
            var matrix2 = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(9), new Number(2) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4), new Number(3) })
            });
            var add = new Add(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(15), new Number(5) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(6), new Number(4) })
            });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add4MatricesTest()
        {
            var vector1 = new Maths.Expressions.Matrices.Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector2 = new Maths.Expressions.Matrices.Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector3 = new Maths.Expressions.Matrices.Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector4 = new Maths.Expressions.Matrices.Vector(new IExpression[] { new Number(1), new Number(2) });
            var add1 = new Add(vector1, vector2);
            var add2 = new Add(vector3, vector4);
            var add3 = new Add(add1, add2);

            var expected = new Maths.Expressions.Matrices.Vector(new IExpression[] { new Number(4), new Number(8) });

            Assert.Equal(expected, add3.Execute());
        }

        [Fact]
        public void ExecuteTestExecption()
        {
            var exp = new Add(new Bool(false), new Bool(false));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Add(Variable.X, new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
