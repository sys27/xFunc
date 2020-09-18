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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests
{
    public class RootTests : TypeAnalyzerBaseTests
    {
        [Fact]
        public void TestRootUndefined()
        {
            var exp = new Root(Variable.X, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestRootUndefinedAndNumber()
        {
            var exp = new Root(Variable.X, Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestRootNumberAndUndefined()
        {
            var exp = new Root(Number.Two, Variable.X);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestRootNumber()
        {
            var exp = new Root(new Number(4), Number.Two);

            Test(exp, ResultTypes.Undefined);
        }

        [Fact]
        public void TestRootUndefinedAndBool()
        {
            var exp = new Root(Variable.X, Bool.False);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootBoolAndUndefined()
        {
            var exp = new Root(Bool.False, Variable.X);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootNumberAndBool()
        {
            var exp = new Root(Number.Two, Bool.False);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootBoolAndNumber()
        {
            var exp = new Root(Bool.False, Number.Two);

            TestBinaryException(exp);
        }

        [Fact]
        public void TestRootInvalidArgsException()
        {
            var exp = new Root(Bool.False, Bool.False);

            TestException(exp);
        }
    }
}