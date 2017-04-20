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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Matrices;
using Xunit;

namespace xFunc.Tests.Analyzers
{

    public class TypeAnalyzerTest
    {

        private IAnalyzer<ResultType> analyzer;

        public TypeAnalyzerTest()
        {
            analyzer = new TypeAnalyzer();
        }

        private void Test(IExpression exp, ResultType expected)
        {
            var simple = exp.Analyze(analyzer);

            Assert.Equal(expected, simple);
        }

        #region Standard

        [Fact]
        public void TestAbsNumber()
        {
            var exp = new Abs(new Number(-2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAbsComplexNumber()
        {
            var exp = new Abs(new ComplexNumber(2, 2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAbsVariable()
        {
            var exp = new Abs(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAbsVector()
        {
            var exp = new Abs(new Vector(new[] { new Number(1) }));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAddTwoNumberTest()
        {
            var add = new Add(new Number(1), new Number(2));

            Test(add, ResultType.Number);
        }

        [Fact]
        public void TestAddNumberVarTest()
        {
            var add = new Add(new Number(1), new Variable("x"));

            Test(add, ResultType.Undefined);
        }

        [Fact]
        public void TestAddComplicatedTest()
        {
            var add = new Add(new Mul(new Number(1), new Number(2)), new Variable("x"));

            Test(add, ResultType.Undefined);
        }

        [Fact]
        public void TestAddTwoVectorTest()
        {
            var add = new Add(new Vector(new[] { new Number(1) }),
                              new Vector(new[] { new Number(2) }));

            Test(add, ResultType.Vector);
        }

        [Fact]
        public void TestAddTwoMatrixTest()
        {
            var add = new Add(new Matrix(new[] { new Vector(new[] { new Number(1) }) }),
                              new Matrix(new[] { new Vector(new[] { new Number(2) }) }));

            Test(add, ResultType.Matrix);
        }

        [Fact]
        public void TestAddNumberVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Number(1), new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void TestAddVectorNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Vector(new[] { new Number(1) }), new Number(1)));
        }

        [Fact]
        public void TestAddNumberMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Number(1), new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void TestAddMatrixNumberTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }), new Number(1)));
        }

        [Fact]
        public void TestAddVectorMatrixTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Vector(new[] { new Number(1) }),
                                                                        new Matrix(new[] { new Vector(new[] { new Number(2) }) })));
        }

        [Fact]
        public void TestAddMatrixVectorTest()
        {
            Assert.Throws<ParameterTypeMismatchException>(() => new Add(new Matrix(new[] { new Vector(new[] { new Number(2) }) }),
                                                                        new Vector(new[] { new Number(1) })));
        }

        [Fact]
        public void TestAddNumberComplexNumberTest()
        {
            var add = new Add(new Number(1), new ComplexNumber(2, 1));

            Test(add, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestAddComplexNumberNumberTest()
        {
            var add = new Add(new ComplexNumber(1, 3), new Number(2));

            Test(add, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestAddNumberAllTest()
        {
            var exp = new Add(new Number(1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddComplexNumberAllTest()
        {
            var exp = new Add(new ComplexNumber(3, 2), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddVectorAllTest()
        {
            var exp = new Add(new Vector(1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddMatrixAllTest()
        {
            var exp = new Add(new Matrix(1, 1), new UserFunction("f", 1));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddNumberComplexTest()
        {
            var exp = new Add(new Number(2), new Sqrt(new Number(-9)));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddTwoVarTest()
        {
            var exp = new Add(new Variable("x"), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAddThreeVarTest()
        {
            var exp = new Add(new Add(new Variable("x"), new Variable("x")), new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCeilNumber()
        {
            var exp = new Ceil(new Number(-2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCeilVariable()
        {
            var exp = new Ceil(new Variable("x"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestdDefineUndefine()
        {
            var exp = new Define(new Variable("x"), new Number(-2));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestDelVector()
        {
            var exp = new Del(new Number(2));

            Test(exp, ResultType.Vector);
        }

        #endregion Standard

    }

}
