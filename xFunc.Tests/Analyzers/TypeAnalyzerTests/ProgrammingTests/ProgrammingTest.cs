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
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests.ProgrammingTests
{
    public class ProgrammingTest : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestDec()
        {
            var exp = new Dec(Variable.X);

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestForUndefined()
        {
            var exp = new For(Variable.X, Variable.X, Variable.X, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestForNumber()
        {
            var exp = new For(Variable.X, Variable.X, Bool.False, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestForException()
        {
            var exp = new For(Variable.X, Variable.X, new ComplexNumber(2, 3), Variable.X);

            TestException(exp);
        }

        [Fact]
        public void TestIfUndefined()
        {
            var exp = new If(Variable.X, new Number(10));

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestIfBool()
        {
            var exp = new If(Bool.False, new Number(10));

            Test(exp, ResultTypes.Number);
        }

        [Fact]
        public void TestIfElseBool()
        {
            var exp = new If(Bool.False, new Number(10), Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestIfException()
        {
            var exp = new If(new ComplexNumber(2, 4), new Number(10));

            TestDiffParamException(exp);
        }

        [Fact]
        public void TestInc()
        {
            var exp = new Inc(Variable.X);

            Test(exp, ResultTypes.Number);
        }


        [Fact]
        public void TestWhileUndefined()
        {
            var exp = new While(Variable.X, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestWhileNumber()
        {
            var exp = new While(Variable.X, Bool.False);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestWhileException()
        {
            var exp = new While(Variable.X, Number.One);

            TestException(exp);
        }
    }
}