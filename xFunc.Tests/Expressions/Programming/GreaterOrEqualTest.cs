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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Tests.Expressions.Programming
{
    public class GreaterOrEqualTest
    {
        [Fact]
        public void CalculateGreaterTrueTest1()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 463) };
            var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

            Assert.True((bool) greaterOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateGreaterTrueTest2()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 10) };
            var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

            Assert.True((bool) greaterOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateGreaterFalseTest()
        {
            var parameters = new ParameterCollection() { new Parameter("x", 0) };
            var greaterOrEqual = new GreaterOrEqual(Variable.X, new Number(10));

            Assert.False((bool) greaterOrEqual.Execute(parameters));
        }

        [Fact]
        public void CalculateInvalidTypeTest()
        {
            var greaterOrEqual = new GreaterOrEqual(Bool.True, Bool.True);

            Assert.Throws<ResultIsNotSupportedException>(() => greaterOrEqual.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new GreaterOrEqual(Number.Two, new Number(3));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}