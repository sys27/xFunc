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
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{

    public class HyperbolicTests : TypeAnalyzerBaseTests
    {

        [Fact]
        public void TestArcoshUndefined()
        {
            var exp = new Arcosh(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcoshNumber()
        {
            var exp = new Arcosh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcoshComplexNumber()
        {
            var exp = new Arcosh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcoshException()
        {
            var exp = new Arcosh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcothUndefined()
        {
            var exp = new Arcoth(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcothNumber()
        {
            var exp = new Arcoth(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcothComplexNumber()
        {
            var exp = new Arcoth(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcothException()
        {
            var exp = new Arcoth(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArcschUndefined()
        {
            var exp = new Arcsch(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArcschNumber()
        {
            var exp = new Arcsch(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArcschComplexNumber()
        {
            var exp = new Arcsch(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArcschException()
        {
            var exp = new Arcsch(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArsechUndefined()
        {
            var exp = new Arsech(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArsechNumber()
        {
            var exp = new Arsech(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArsechComplexNumber()
        {
            var exp = new Arsech(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArsechException()
        {
            var exp = new Arsech(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArsinhUndefined()
        {
            var exp = new Arsinh(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArsinhNumber()
        {
            var exp = new Arsinh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArsinhComplexNumber()
        {
            var exp = new Arsinh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArsinhException()
        {
            var exp = new Arsinh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestArtanhUndefined()
        {
            var exp = new Artanh(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestArtanhNumber()
        {
            var exp = new Artanh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestArtanhComplexNumber()
        {
            var exp = new Artanh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestArtanhException()
        {
            var exp = new Artanh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCoshUndefined()
        {
            var exp = new Cosh(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCoshNumber()
        {
            var exp = new Cosh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCoshComplexNumber()
        {
            var exp = new Cosh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCoshException()
        {
            var exp = new Cosh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCothUndefined()
        {
            var exp = new Coth(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCothNumber()
        {
            var exp = new Coth(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCothComplexNumber()
        {
            var exp = new Coth(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCothException()
        {
            var exp = new Coth(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestCschUndefined()
        {
            var exp = new Csch(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestCschNumber()
        {
            var exp = new Csch(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestCschComplexNumber()
        {
            var exp = new Csch(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestCschException()
        {
            var exp = new Csch(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSechUndefined()
        {
            var exp = new Sech(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSechNumber()
        {
            var exp = new Sech(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSechComplexNumber()
        {
            var exp = new Sech(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSechException()
        {
            var exp = new Sech(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestSinhUndefined()
        {
            var exp = new Sinh(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestSinhNumber()
        {
            var exp = new Sinh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestSinhComplexNumber()
        {
            var exp = new Sinh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestSinhException()
        {
            var exp = new Sinh(new Bool(false));

            TestException(exp);
        }

        [Fact]
        public void TestTanhUndefined()
        {
            var exp = new Tanh(Variable.X);

            Test(exp, ResultType.Undefined);
        }

        [Fact]
        public void TestTanhNumber()
        {
            var exp = new Tanh(new Number(2));

            Test(exp, ResultType.Number);
        }

        [Fact]
        public void TestTanhComplexNumber()
        {
            var exp = new Tanh(new ComplexNumber(2, 2));

            Test(exp, ResultType.ComplexNumber);
        }

        [Fact]
        public void TestTanhException()
        {
            var exp = new Tanh(new Bool(false));

            TestException(exp);
        }

    }

}
