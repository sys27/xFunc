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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{

    public class LogicalBitwiseTests : TypeAnalyzerBaseTests
    {

        [Fact]
        public void TestAndUndefined()
        {
            var exp = new And(Variable.X, new Variable("y"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestAndNumber()
        {
            var exp = new And(new Number(1), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestAndNumberComplexException()
        {
            var exp = new And(new Number(1), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAndComplexNumberException()
        {
            var exp = new And(new ComplexNumber(1, 1), new Number(2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAndBoolean()
        {
            var exp = new And(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestAndBoolComplexException()
        {
            var exp = new And(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAndComplexBoolException()
        {
            var exp = new And(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestAndException()
        {
            var exp = new And(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

        [Fact]
        public void TestBool()
        {
            var exp = new Bool(false);

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestEqualityUndefined()
        {
            var exp = new Equality(Variable.X, new Variable("y"));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestEqualityBoolean()
        {
            var exp = new Equality(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestEqualityBoolComplexException()
        {
            var exp = new Equality(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestEqualityComplexBoolException()
        {
            var exp = new Equality(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestEqualityException()
        {
            var exp = new Equality(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

        [Fact]
        public void TestImplicationUndefined()
        {
            var exp = new Implication(Variable.X, new Variable("y"));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestImplicationBoolean()
        {
            var exp = new Implication(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestImplicationBoolComplexException()
        {
            var exp = new Implication(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestImplicationComplexBoolExcetpion()
        {
            var exp = new Implication(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestImplicationException()
        {
            var exp = new Implication(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

        [Fact]
        public void TestNAndUndefined()
        {
            var exp = new NAnd(Variable.X, new Variable("y"));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestNAndBoolean()
        {
            var exp = new NAnd(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestNAndBoolComplexException()
        {
            var exp = new NAnd(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestNAndComplexBoolException()
        {
            var exp = new NAnd(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestNAndException()
        {
            var exp = new NAnd(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

        [Fact]
        public void TestNOrUndefined()
        {
            var exp = new NOr(Variable.X, new Variable("y"));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestNOrBoolean()
        {
            var exp = new NOr(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestNOrBoolComplexException()
        {
            var exp = new NOr(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestNOrComplexBoolException()
        {
            var exp = new NOr(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestNOrException()
        {
            var exp = new NOr(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

        [Fact]
        public void TestNotUndefined()
        {
            var exp = new Not(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestNotNumber()
        {
            var exp = new Not(new Number(1));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestNotBoolean()
        {
            var exp = new Not(new Bool(true));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestNotException()
        {
            var exp = new Not(new ComplexNumber(1, 2));

            TestException(exp);
        }

        [Fact]
        public void TestOrUndefined()
        {
            var exp = new Or(Variable.X, new Variable("y"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestOrNumber()
        {
            var exp = new Or(new Number(1), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestOrNumberComplexException()
        {
            var exp = new Or(new Number(1), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestOrComplexNumberException()
        {
            var exp = new Or(new ComplexNumber(1, 1), new Number(2));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestOrBoolean()
        {
            var exp = new Or(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestOrBoolComplexException()
        {
            var exp = new Or(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestOrComplexBoolException()
        {
            var exp = new Or(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestOrException()
        {
            var exp = new Or(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

        [Fact]
        public void TestXOrUndefined()
        {
            var exp = new XOr(Variable.X, new Variable("y"));

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestXOrNumber()
        {
            var exp = new XOr(new Number(1), new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestXOrNumberComplexException()
        {
            var exp = new XOr(new Number(1), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestXOrComplexNumberException()
        {
            var exp = new XOr(new ComplexNumber(1, 1), new Number(1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestXOrBoolean()
        {
            var exp = new XOr(new Bool(true), new Bool(false));

            Test(exp, ResultType.Boolean);
        }

        [Fact]
        public void TestXOrBoolComplexNumberException()
        {
            var exp = new XOr(new Bool(true), new ComplexNumber(1, 1));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestXOrComplexNumberBoolException()
        {
            var exp = new XOr(new ComplexNumber(1, 1), new Bool(false));

            TestBinaryException(exp);
        }

        [Fact]
        public void TestXOrException()
        {
            var exp = new XOr(new ComplexNumber(1, 2), new ComplexNumber(2, 3));

            TestException(exp);
        }

    }

}
