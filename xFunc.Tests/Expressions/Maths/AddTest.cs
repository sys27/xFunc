// Copyright 2012-2016 Dmitry Kischenko
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
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{

    public class AddTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Add(new Number(1), new Number(2));

            Assert.Equal(3.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Add(new Number(-3), new Number(2));

            Assert.Equal(-1.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Add(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
            var expected = new Complex(9, 7);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Add(new Number(7), new ComplexNumber(2, 4));
            var expected = new Complex(9, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest5()
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
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });
            var add = new Add(vector1, vector2);

            var expected = new Vector(new[] { new Number(9), new Number(4) });
            var result = add.Execute();

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
        public void ResultTypeTwoNumberTest()
        {
            var add = new Add(new Number(1), new Number(2));

            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.LeftType);
            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.RightType);
            Assert.Equal(ExpressionResultType.Number, add.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVarTest()
        {
            var add = new Add(new Number(1), new Variable("x"));

            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.LeftType);
            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.RightType);
            Assert.Equal(ExpressionResultType.Number, add.ResultType);
        }

        [Fact]
        public void ResultTypeComplicatedTest()
        {
            var add = new Add(new Mul(new Number(1), new Number(2)), new Variable("x"));

            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.LeftType);
            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.RightType);
            Assert.Equal(ExpressionResultType.Number, add.ResultType);
        }

        [Fact]
        public void ResultTypeTwoVectorTest()
        {
            var add = new Add(new Vector(new[] { new Number(1) }),
                              new Vector(new[] { new Number(2) }));

            Assert.Equal(ExpressionResultType.Vector, add.LeftType);
            Assert.Equal(ExpressionResultType.Vector, add.RightType);
            Assert.Equal(ExpressionResultType.Vector, add.ResultType);
        }

        [Fact]
        public void ResultTypeTwoMatrixTest()
        {
            var add = new Add(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ExpressionResultType.Matrix, add.LeftType);
            Assert.Equal(ExpressionResultType.Matrix, add.RightType);
            Assert.Equal(ExpressionResultType.Matrix, add.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Number(1), new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void ResultTypeVectorNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Vector(new[] { new Number(1) }), new Number(1)));
        }

        [Fact]
        public void ResultTypeNumberMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void ResultTypeMatrixNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1)));
        }

        [Fact]
        public void ResultTypeVectorMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Vector(new[] { new Number(1) }),
                                                                        new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void ResultTypeMatrixVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }),
                                                                        new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void ResultTypeNumberComplexNumberTest()
        {
            var add = new Add(new Number(1), new ComplexNumber(2, 1));

            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.LeftType);
            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.RightType);
            Assert.Equal(ExpressionResultType.ComplexNumber, add.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), new Number(2));

            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.LeftType);
            Assert.Equal(ExpressionResultType.Number | ExpressionResultType.ComplexNumber, add.RightType);
            Assert.Equal(ExpressionResultType.ComplexNumber, add.ResultType);
        }

        [Fact]
        public void ResultTypeNumberAllTest()
        {
            var exp = new Add(new Number(1), new UserFunction("f", 1));

            Assert.Equal(ExpressionResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberAllTest()
        {
            var exp = new Add(new ComplexNumber(3, 2), new UserFunction("f", 1));

            Assert.Equal(ExpressionResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeVectorAllTest()
        {
            var exp = new Add(new Vector(1), new UserFunction("f", 1));

            Assert.Equal(ExpressionResultType.Vector, exp.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixAllTest()
        {
            var exp = new Add(new Matrix(1, 1), new UserFunction("f", 1));

            Assert.Equal(ExpressionResultType.Matrix, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumberComplexTest()
        {
            var exp = new Add(new Number(2), new Sqrt(new Number(-9)));

            Assert.Equal(ExpressionResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Add(new Variable("x"), new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
