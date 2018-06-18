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
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{

    public class MatrixTests : TypeAnalyzerBaseTests
    {

        [Fact]
        public void TestVectorUndefined()
        {
            var exp = new Vector(new IExpression[] { new Number(10), Variable.X });

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestVectorNumber()
        {
            var exp = new Vector(new IExpression[] { new Number(10), new Number(10), new Number(10) });

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void TestVectorException()
        {
            var exp = new Vector(new IExpression[] { new ComplexNumber(10), new Number(10) });

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestMatrixVector()
        {
            var exp = new Matrix(new Vector[] { new Vector(2), new Vector(2) });

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestMatrixUndefinedElement()
        {
            var exp = new Matrix(new Vector[] { new Vector(new[] { Variable.X }) });

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestMatrixNotVectorElement()
        {
            DifferentParametersExpression exp = new Matrix(2, 2)
            {
                Arguments = new[] { new Number(2) }
            };

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestEmptyMatrix()
        {
            var exp = new Matrix(0, 0);

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestNullMatrix()
        {
            var exp = new Matrix(0, 0);
            exp.Arguments = null;

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestDetermenantUndefined()
        {
            var exp = new Determinant(Variable.X);

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDetermenantMatrix()
        {
            var exp = new Determinant(new Matrix(2, 2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDetermenantException()
        {
            var exp = new Determinant(new ComplexNumber(2, 2));

            TestException(exp);
        }

        [Fact]
        public void TestDeterminantsException2()
        {
            var exp = new Determinant(new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }));

            TestException(exp);
        }

        [Fact]
        public void TestInverseUndefined()
        {
            var exp = new Inverse(Variable.X);

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestInverseMatrix()
        {
            var exp = new Inverse(new Matrix(2, 2));

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestInverseException()
        {
            var exp = new Inverse(new ComplexNumber(2, 2));

            TestException(exp);
        }

        [Fact]
        public void TestInverseException2()
        {
            var exp = new Inverse(new Vector(new[] { new Number(3), new Number(7), new Number(2), new Number(5) }));

            TestException(exp);
        }

        [Fact]
        public void TestTransposeUndefined()
        {
            var exp = new Transpose(Variable.X);

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestTransposeVector()
        {
            var exp = new Transpose(new Vector(2));

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestTransposeMatrix()
        {
            var exp = new Transpose(new Matrix(2, 2));

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestTransposeException()
        {
            var exp = new Transpose(new ComplexNumber(2, 2));

            TestException(exp);
        }

    }

}
