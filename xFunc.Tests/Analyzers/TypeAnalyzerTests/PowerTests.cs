// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class PowerTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestPowComplexAndUndefined()
        {
            var exp = new Pow(new ComplexNumber(2, 2), Variable.X);

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestPowComplexAndNumber()
        {
            var exp = new Pow(new ComplexNumber(2, 4), new Number(4));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestPowComplexAndComplex()
        {
            var exp = new Pow(new ComplexNumber(4, 2), new ComplexNumber(2, 4));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestPowUndefinedAndNumber()
        {
            var exp = new Pow(Variable.X, Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestPowNumberAndUndefined()
        {
            var exp = new Pow(Number.Two, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestPowNumber()
        {
            var exp = new Pow(new Number(4), Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestPowException()
        {
            var exp = new Pow(Bool.False, Bool.False);

            TestBinaryException(exp);
        }
    }
}