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
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Expressions.Maths
{

    public class MulTest
    {

        [Fact]
        public void ExecuteTest()
        {
            var exp = new Mul(new Number(2), new Number(2));

            Assert.Equal(4.0, exp.Execute());
        }

        [Fact]
        public void ResultTypeTwoNumberTest()
        {
            var mul = new Mul(new Number(1), new Number(2));

            Assert.Equal(ExpressionResultType.Number, mul.LeftType);
            Assert.Equal(ExpressionResultType.Number, mul.RightType);
            Assert.Equal(ExpressionResultType.Number, mul.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVarTest()
        {
            var mul = new Mul(new Number(1), new Variable("x"));

            Assert.Equal(ExpressionResultType.Number, mul.LeftType);
            Assert.Equal(ExpressionResultType.Number, mul.RightType);
            Assert.Equal(ExpressionResultType.Number, mul.ResultType);
        }

        [Fact]
        public void ResultTypeComplicatedTest()
        {
            var mul = new Mul(new Add(new Number(1), new Number(2)), new Variable("x"));

            Assert.Equal(ExpressionResultType.Number, mul.LeftType);
            Assert.Equal(ExpressionResultType.Number, mul.RightType);
            Assert.Equal(ExpressionResultType.Number, mul.ResultType);
        }

        [Fact]
        public void ResultTypeTwoVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Mul(new Vector(new[] { new Number(1) }),
                                                                       new Vector(new[] { new Number(2) })));
        }

        [Fact]
        public void ResultTypeTwoMatrixTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ExpressionResultType.Matrix, mul.LeftType);
            Assert.Equal(ExpressionResultType.Matrix, mul.RightType);
            Assert.Equal(ExpressionResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeNumberVectorTest()
        {
            var mul = new Mul(new Number(1), new Vector(new[] { new Number(1) }));

            Assert.Equal(ExpressionResultType.Number, mul.LeftType);
            Assert.Equal(ExpressionResultType.Vector, mul.RightType);
            Assert.Equal(ExpressionResultType.Vector, mul.ResultType);
        }

        [Fact]
        public void ResultTypeVectorNumberTest()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }), new Number(1));

            Assert.Equal(ExpressionResultType.Vector, mul.LeftType);
            Assert.Equal(ExpressionResultType.Number, mul.RightType);
            Assert.Equal(ExpressionResultType.Vector, mul.ResultType);
        }

        [Fact]
        public void ResultTypeNumberMatrixTest()
        {
            var mul = new Mul(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ExpressionResultType.Number, mul.LeftType);
            Assert.Equal(ExpressionResultType.Matrix, mul.RightType);
            Assert.Equal(ExpressionResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixNumberTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1));

            Assert.Equal(ExpressionResultType.Matrix, mul.LeftType);
            Assert.Equal(ExpressionResultType.Number, mul.RightType);
            Assert.Equal(ExpressionResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeVectorMatrixTest()
        {
            var mul = new Mul(new Vector(new[] { new Number(1) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Assert.Equal(ExpressionResultType.Vector, mul.LeftType);
            Assert.Equal(ExpressionResultType.Matrix, mul.RightType);
            Assert.Equal(ExpressionResultType.Matrix, mul.ResultType);
        }

        [Fact]
        public void ResultTypeMatrixVectorTest()
        {
            var mul = new Mul(new Matrix(new[] { new Vector(new[] { new Number(2) }) }),
                              new Vector(new[] { new Number(1) }));

            Assert.Equal(ExpressionResultType.Matrix, mul.LeftType);
            Assert.Equal(ExpressionResultType.Vector, mul.RightType);
            Assert.Equal(ExpressionResultType.Matrix, mul.ResultType);
        }

    }

}
