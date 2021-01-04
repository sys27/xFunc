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
    public class ComplexNumberTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestComplexNumber()
        {
            var exp = new ComplexNumber(2, 2);

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestConjugateUndefined()
        {
            var exp = new Conjugate(Variable.X);

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestConjugateComplexNumber()
        {
            var exp = new Conjugate(new ComplexNumber(2, 3));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestConjugateException()
        {
            var exp = new Conjugate(Number.Two);

            TestException(exp);
        }

        [Fact]
        public void TestImUndefined()
        {
            var exp = new Im(Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestImComplexNumber()
        {
            var exp = new Im(new ComplexNumber(2, 3));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestImException()
        {
            var exp = new Im(Number.Two);

            TestException(exp);
        }

        [Fact]
        public void TestPhaseUndefined()
        {
            var exp = new Phase(Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestPhaseComplexNumber()
        {
            var exp = new Phase(new ComplexNumber(2, 3));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestPhaseException()
        {
            var exp = new Phase(Number.Two);

            TestException(exp);
        }

        [Fact]
        public void TestReUndefined()
        {
            var exp = new Re(Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestReComplexNumber()
        {
            var exp = new Re(new ComplexNumber(2, 3));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestReException()
        {
            var exp = new Re(Number.Two);

            TestException(exp);
        }

        [Fact]
        public void TestReciprocalUndefined()
        {
            var exp = new Reciprocal(Variable.X);

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestReciprocalComplexNumber()
        {
            var exp = new Reciprocal(new ComplexNumber(2, 3));

            Test(exp, ResultTypes.ComplexNumber);
        }

        [Fact]
        public void TestReciprocalException()
        {
            var exp = new Reciprocal(Number.Two);

            TestException(exp);
        }

        [Fact]
        public void ToComplexUndefined()
            => Test(new ToComplex(Variable.X), ResultTypes.ComplexNumber);

        [Fact]
        public void ToComplexNubmer()
            => Test(new ToComplex(Number.One), ResultTypes.ComplexNumber);

        [Fact]
        public void ToComplexException()
            => TestException(new ToComplex(Bool.False));
    }
}