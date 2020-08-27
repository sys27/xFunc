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

using System;
using System.Numerics;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Angles;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;
using Vector = xFunc.Maths.Expressions.Matrices.Vector;
using Matrix = xFunc.Maths.Expressions.Matrices.Matrix;

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
            var vector1 = new Vector(new IExpression[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new IExpression[] { new Number(7), new Number(1) });
            var add = new Add(vector1, vector2);

            var expected = new Vector(new IExpression[] { new Number(9), new Number(4) });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddTwoMatricesTest()
        {
            var matrix1 = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(6), new Number(3) }),
                new Vector(new IExpression[] { new Number(2), new Number(1) })
            });
            var matrix2 = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(9), new Number(2) }),
                new Vector(new IExpression[] { new Number(4), new Number(3) })
            });
            var add = new Add(matrix1, matrix2);

            var expected = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(15), new Number(5) }),
                new Vector(new IExpression[] { new Number(6), new Number(4) })
            });
            var result = add.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add4MatricesTest()
        {
            var vector1 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector2 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector3 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector4 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var add1 = new Add(vector1, vector2);
            var add2 = new Add(vector3, vector4);
            var add3 = new Add(add1, add2);

            var expected = new Vector(new IExpression[] { new Number(4), new Number(8) });

            Assert.Equal(expected, add3.Execute());
        }

        [Fact]
        public void AddNumberAndDegree()
        {
            var exp = new Add(new Number(1), Angle.Degree(10).AsExpression());
            var actual = exp.Execute();
            var expected = Angle.Degree(11);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddRadianAndNumber()
        {
            var exp = new Add(Angle.Radian(10).AsExpression(), new Number(1));
            var actual = exp.Execute();
            var expected = Angle.Radian(11);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddDegreeAndRadian()
        {
            var exp = new Add(
                Angle.Degree(10).AsExpression(),
                Angle.Radian(Math.PI).AsExpression()
            );
            var actual = exp.Execute();
            var expected = Angle.Degree(190);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddGradianAndGradian()
        {
            var exp = new Add(
                Angle.Gradian(10).AsExpression(),
                Angle.Gradian(20).AsExpression()
            );
            var actual = exp.Execute();
            var expected = Angle.Gradian(30);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteTestException()
        {
            var exp = new Add(Bool.False, Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteComplexNumberAndBool()
        {
            var exp = new Add(new ComplexNumber(7, 3), Bool.False);

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void ExecuteBoolAndComplexNumber()
        {
            var exp = new Add(Bool.False, new ComplexNumber(7, 3));

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