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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class TrigonometricTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestArccosUndefined()
        {
            var exp = new Arccos(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArccosNumber()
        {
            var exp = new Arccos(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArccosComplexNumber()
        {
            var exp = new Arccos(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArccosException()
        {
            var exp = new Arccos(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArccotUndefined()
        {
            var exp = new Arccot(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArccotNumber()
        {
            var exp = new Arccot(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArccotComplexNumber()
        {
            var exp = new Arccot(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArccotException()
        {
            var exp = new Arccot(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArccscUndefined()
        {
            var exp = new Arccsc(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArccscNumber()
        {
            var exp = new Arccsc(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArccscComplexNumber()
        {
            var exp = new Arccsc(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArccscException()
        {
            var exp = new Arccsc(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcsecUndefined()
        {
            var exp = new Arcsec(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcsecNumber()
        {
            var exp = new Arcsec(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcsecComplexNumber()
        {
            var exp = new Arcsec(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcsecException()
        {
            var exp = new Arcsec(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcsinUndefined()
        {
            var exp = new Arcsin(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcsinNumber()
        {
            var exp = new Arcsin(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcsinComplexNumber()
        {
            var exp = new Arcsin(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcsinException()
        {
            var exp = new Arcsin(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArctanUndefined()
        {
            var exp = new Arctan(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArctanNumber()
        {
            var exp = new Arctan(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArctanComplexNumber()
        {
            var exp = new Arctan(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArctanException()
        {
            var exp = new Arctan(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCosUndefined()
        {
            var exp = new Cos(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCosNumber()
        {
            var exp = new Cos(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCosComplexNumber()
        {
            var exp = new Cos(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCosException()
        {
            var exp = new Cos(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCotUndefined()
        {
            var exp = new Cot(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCotNumber()
        {
            var exp = new Cot(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCotComplexNumber()
        {
            var exp = new Cot(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCotException()
        {
            var exp = new Cot(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCscUndefined()
        {
            var exp = new Csc(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCscNumber()
        {
            var exp = new Csc(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCscComplexNumber()
        {
            var exp = new Csc(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCscException()
        {
            var exp = new Csc(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSecUndefined()
        {
            var exp = new Sec(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSecNumber()
        {
            var exp = new Sec(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSecComplexNumber()
        {
            var exp = new Sec(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSecException()
        {
            var exp = new Sec(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSinUndefined()
        {
            var exp = new Sin(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSinNumber()
        {
            var exp = new Sin(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSinComplexNumber()
        {
            var exp = new Sin(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSinException()
        {
            var exp = new Sin(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestTanUndefined()
        {
            var exp = new Tan(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestTanNumber()
        {
            var exp = new Tan(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestTanComplexNumber()
        {
            var exp = new Tan(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestTanException()
        {
            var exp = new Tan(new Bool(false));

            TestException(exp);
        }
    }
}