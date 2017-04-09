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

    public class MulTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Mul(new Number(2), new Number(2));

            Assert.Equal(4.0, exp.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));
            var expected = new Complex(-4, 19);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new Number(2));
            var expected = new Complex(4, 10);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Mul(new Number(2), new ComplexNumber(3, 2));
            var expected = new Complex(6, 4);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteTest6()
        {
            var exp = new Mul(new Number(2), new Sqrt(new Number(-9)));
            var expected = new Complex(0, 6);

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteCrossTest()
        {
            var vector1 = new Vector(new[] { new Number(1), new Number(2), new Number(3) });
            var vector2 = new Vector(new[] { new Number(10), new Number(20), new Number(30) });
            var exp = new Mul(vector1, vector2);

            var expected = new Vector(new[] { new Number(0), new Number(0), new Number(0) });

            Assert.Equal(expected, exp.Execute());
        }

        [Fact]
        public void ExecuteCrossFailTest()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var vector1 = new Vector(new[] { new Number(1), new Number(3) });
                var vector2 = new Vector(new[] { new Number(10), new Number(20) });
                var exp = new Mul(vector1, vector2);
                exp.Execute();
            });
        }

        [Fact]
        public void ResultTypeTwoNumberTest()
        {
            var mul = new Mul(new Number(1), new Number(2));

            Assert.Equal(ResultType.Number, mul.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVarTest()
        {
            var mul = new Mul(new Number(1), new Variable("x"));

            Assert.Equal(ResultType.Number, mul.ResultType);
        }

        [Fact]
        public void ResultTypeComplicatedTest()
        {
            var mul = new Mul(new Add(new Number(1), new Number(2)), new Variable("x"));

            Assert.Equal(ResultType.Number, mul.ResultType);
        }

        [Fact]
        public void ResultTypeTwoMatrixTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVectorTest()
        {
            var mul = new Mul(new Number(1), new Vector(new[] { new Number(1) }));

            Assert.Equal(ResultType.Vector, mul.ResultType);
        }

        [Fact]
        public void ResultTypeVectorNumberTest()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }), new Number(1));

            Assert.Equal(ResultType.Vector, mul.ResultType);
        }

        [Fact]
        public void ResultTypeNumberMatrixTest()
        {
            var mul = new Mul(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixNumberTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1));

            Assert.Equal(ResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeVectorMatrixTest()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixVectorTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(2) }) }),
                              new Vector(new[] { new Number(1) }));

            Assert.Equal(ResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberComplexNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new ComplexNumber(3, 2));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberNumberTest()
        {
            var exp = new Mul(new ComplexNumber(2, 5), new Number(2));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumberComplexNumberTest()
        {
            var exp = new Mul(new Number(2), new ComplexNumber(3, 2));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumberAllTest()
        {
            var exp = new Mul(new Number(1), new UserFunction("f", 1));

            Assert.Equal(ResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeComplexNumberAllTest()
        {
            var exp = new Mul(new ComplexNumber(3, 2), new UserFunction("f", 1));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeVectorAllTest()
        {
            var exp = new Mul(new Vector(1), new UserFunction("f", 1));

            Assert.Equal(ResultType.Vector, exp.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixAllTest()
        {
            var exp = new Mul(new Matrix(1, 1), new UserFunction("f", 1));

            Assert.Equal(ResultType.Matrix, exp.ResultType);
        }

        [Fact]
        public void ResultTypeNumberComplexTest()
        {
            var exp = new Mul(new Number(2), new Sqrt(new Number(-9)));

            Assert.Equal(ResultType.ComplexNumber, exp.ResultType);
        }

        [Fact]
        public void ResultTypeTwoVarTest()
        {
            var exp = new Mul(new Variable("x"), new Variable("x"));

            Assert.Equal(ResultType.Number, exp.ResultType);
        }

        [Fact]
        public void ResultTypeThreeVarTest()
        {
            var exp = new Mul(new Add(new Variable("x"), new Variable("x")), new Variable("x"));

            Assert.Equal(ResultType.Number, exp.ResultType);
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Mul(new Variable("x"), new Number(0));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
