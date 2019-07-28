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
            var exp = new Matrix(new IExpression[] { new Vector(2), new Vector(2) });

            Test(exp, ResultType.Matrix);
        }

        [Fact]
        public void TestMatrixUndefinedElement()
        {
            var exp = new Matrix(new IExpression[] { new Vector(new IExpression[] { Variable.X }) });

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestMatrixNotVectorElement()
        {
            Assert.Throws<MatrixIsInvalidException>(() => new Matrix(new IExpression[] { new Number(2) }));
        }

        [Fact]
        public void TestDeterminantUndefined()
        {
            var exp = new Determinant(Variable.X);

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDeterminantMatrix()
        {
            var exp = new Determinant(Matrix.Create(2, 2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestDeterminantException()
        {
            var exp = new Determinant(new ComplexNumber(2, 2));

            TestException(exp);
        }

        [Fact]
        public void TestDeterminantsException2()
        {
            var exp = new Determinant(
                new Vector(new IExpression[]
                {
                    new Number(3),
                    new Number(7),
                    new Number(2),
                    new Number(5)
                }));

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
            var exp = new Inverse(Matrix.Create(2, 2));

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
            var exp = new Inverse(
                new Vector(new IExpression[]
                {
                    new Number(3),
                    new Number(7),
                    new Number(2),
                    new Number(5)
                }));

            TestException(exp);
        }

        [Fact]
        public void DotProductUndefined()
        {
            var exp = new DotProduct(Variable.X, Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void DotProductLeftUndefined()
        {
            var exp = new DotProduct(Variable.X, new Vector(new IExpression[] { new Number(1) }));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void DotProductRightUndefined()
        {
            var exp = new DotProduct(new Vector(new IExpression[] { new Number(1) }), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void DotProduct()
        {
            var exp = new DotProduct(
                new Vector(new IExpression[] { new Number(1) }),
                new Vector(new IExpression[] { new Number(2) }));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void DotProductLeftException()
        {
            var exp = new DotProduct(
                new Number(1),
                new Vector(new IExpression[] { new Number(2) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void DotProductRightException()
        {
            var exp = new DotProduct(
                new Vector(new IExpression[] { new Number(2) }),
                new Number(1));

            TestBinaryException(exp);
        }

        [Fact]
        public void CrossProductUndefined()
        {
            var exp = new CrossProduct(Variable.X, Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void CrossProductLeftUndefined()
        {
            var exp = new CrossProduct(Variable.X, new Vector(new IExpression[] { new Number(1) }));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void CrossProductRightUndefined()
        {
            var exp = new CrossProduct(new Vector(new IExpression[] { new Number(1) }), Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void CrossProduct()
        {
            var exp = new CrossProduct(
                new Vector(new IExpression[] { new Number(1) }),
                new Vector(new IExpression[] { new Number(2) }));

            Test(exp, ResultType.Vector);
        }

        [Fact]
        public void CrossProductLeftException()
        {
            var exp = new CrossProduct(
                new Number(1),
                new Vector(new IExpression[] { new Number(2) }));

            TestBinaryException(exp);
        }

        [Fact]
        public void CrossProductRightException()
        {
            var exp = new CrossProduct(
                new Vector(new IExpression[] { new Number(2) }),
                new Number(1));

            TestBinaryException(exp);
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
            var exp = new Transpose(Matrix.Create(2, 2));

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
