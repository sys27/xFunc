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

using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions
{
    public class FactTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTest1()
        {
            var fact = new Fact(new Number(4));
            var expected = new NumberValue(24.0);

            Assert.Equal(expected, fact.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var fact = new Fact(Number.Zero);
            var expected = new NumberValue(1.0);

            Assert.Equal(expected, fact.Execute());
        }

        [Fact]
        public void ExecuteTest3()
        {
            var fact = new Fact(Number.One);
            var expected = new NumberValue(1.0);

            Assert.Equal(expected, fact.Execute());
        }

        [Fact]
        public void ExecuteTest4()
        {
            var fact = new Fact(new Number(-1));
            var actual = (NumberValue)fact.Execute();

            Assert.True(actual.IsNaN);
        }

        [Fact]
        public void ExecuteTestException()
            => TestNotSupported(new Fact(Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Fact(Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}