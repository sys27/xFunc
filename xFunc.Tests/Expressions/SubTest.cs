// Copyright 2012-2017 Dmitry Kischenko
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

namespace xFunc.Tests.Expressionss
{

    public class SubTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Sub(new Number(1), new Number(2));

            Assert.Equal(-1.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Sub(new ComplexNumber(7, 3), new ComplexNumber(2, 4));
            var expected = new Complex(5, -1);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Sub(new Number(7), new ComplexNumber(2, 4));
            var expected = new Complex(5, -4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Sub(new ComplexNumber(7, 3), new Number(2));
            var expected = new Complex(5, 3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Sub(new Number(2), new Sqrt(new Number(-9)));
            var expected = new Complex(2, -3);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void SubTwoVectorsTest()
        {
            var vector1 = new Vector(new[] { new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(7), new Number(1) });
            var sub = new Sub(vector1, vector2);

            var expected = new Vector(new[] { new Number(-5), new Number(2) });
            var result = sub.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
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
            var result = sub.Execute();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sub4MatricesTest()
        {
            var vector1 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector2 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector3 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var vector4 = new Vector(new IExpression[] { new Number(1), new Number(2) });
            var sub1 = new Sub(vector1, vector2);
            var sub2 = new Sub(vector3, vector4);
            var sub3 = new Sub(sub1, sub2);

            var expected = new Vector(new IExpression[] { new Number(0), new Number(0) });

            Assert.Equal(expected, sub3.Execute());
        }

        [Fact]
        public void ResultTypeTwoNumberTest()
        {
            var sub = new Sub(new Number(1), new Number(2));

            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.LeftType);
            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.RightType);
            Assert.Equal(ResultType.Number, sub.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVarTest()
        {
            var sub = new Sub(new Number(1), new Variable("x"));

            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.LeftType);
            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.RightType);
            Assert.Equal(ResultType.Number, sub.ResultType);
        }

        [Fact]
        public void ResultTypeComplicatedTest()
        {
            var sub = new Sub(new Mul(new Number(1), new Number(2)), new Variable("x"));

            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.LeftType);
            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.RightType);
            Assert.Equal(ResultType.Number, sub.ResultType);
        }

        [Fact]
        public void ResultTypeTwoVectorTest()
        {
            var sub = new Sub(new Vector(new[] { new Number(1) }),
                              new Vector(new[] { new Number(2) }));

            Assert.Equal(ResultType.Vector, sub.LeftType);
            Assert.Equal(ResultType.Vector, sub.RightType);
            Assert.Equal(ResultType.Vector, sub.ResultType);
        }

        [Fact]
        public void ResultTypeTwoMatrixTest()
        {
            var sub = new Sub(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ResultType.Matrix, sub.LeftType);
            Assert.Equal(ResultType.Matrix, sub.RightType);
            Assert.Equal(ResultType.Matrix, sub.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Sub(new Number(1), new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void ResultTypeVectorNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Sub(new Vector(new[] { new Number(1) }), new Number(1)));
        }

        [Fact]
        public void ResultTypeNumberMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Sub(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void ResultTypeMatrixNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Sub(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1)));
        }

        [Fact]
        public void ResultTypeVectorMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Sub(new Vector(new[] { new Number(1) }),
                                                                        new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void ResultTypeMatrixVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Sub(new Matrix(new[] { new Vector(new[] { new Number(2) }) }),
                                                                        new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void ResultTypeNumberComplexNumberTest()
        {
            var sub = new Sub(new Number(1), new ComplexNumber(2, 1));

            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.LeftType);
            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.RightType);
            Assert.Equal(ResultType.ComplexNumber, sub.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberNumberTest()
        {
            var sub = new Sub(new ComplexNumber(1, 3), new Number(2));

            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.LeftType);
            Assert.Equal(ResultType.Number | ResultType.ComplexNumber, sub.RightType);
            Assert.Equal(ResultType.ComplexNumber, sub.ResultType);
        }

        [Fact]
        public void ResultTypeNumberAllTest()
        {
            var exp = new Sub(new Number(1), new UserFunction("f", 1));

            Assert.Equal(ResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberAllTest()
        {
            var exp = new Sub(new ComplexNumber(3, 2), new UserFunction("f", 1));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeVectorAllTest()
        {
            var exp = new Sub(new Vector(1), new UserFunction("f", 1));

            Assert.Equal(ResultType.Vector, exp.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixAllTest()
        {
            var exp = new Sub(new Matrix(1, 1), new UserFunction("f", 1));

            Assert.Equal(ResultType.Matrix, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumberComplexTest()
        {
            var exp = new Sub(new Number(2), new Sqrt(new Number(-9)));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeTwoVarTest()
        {
            var exp = new Sub(new Variable("x"), new Variable("x"));

            Assert.Equal(ResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeThreeVarTest()
        {
            var exp = new Sub(new Add(new Variable("x"), new Variable("x")), new Variable("x"));

            Assert.Equal(ResultType.Number, exp.ResultType);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Sub(new Number(5), new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
