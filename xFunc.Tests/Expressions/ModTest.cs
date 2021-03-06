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
    public class ModTest : BaseExpressionTests
    {
        [Fact]
        public void ExecuteTest1()
        {
            var exp = new Mod(new Number(25), new Number(7));
            var result = exp.Execute();
            var expected = new NumberValue(4.0);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ExecuteTest2()
        {
            var exp = new Mod(new Number(25), new Number(5));
            var result = exp.Execute();

            Assert.Equal(new NumberValue(0.0), result);
        }

        [Fact]
        public void ExecuteTest3()
        {
            var exp = new Mod(Number.Zero, new Number(5));
            var result = exp.Execute();

            Assert.Equal(new NumberValue(0.0), result);
        }

        [Fact]
        public void ExecuteTest4()
        {
            var exp = new Mod(new Number(5), Number.Zero);
            var result = (NumberValue)exp.Execute();

            Assert.True(result.IsNaN);
        }

        [Fact]
        public void ExecuteResultIsNotSupported()
            => TestNotSupported(new Mod(Bool.True, Bool.False));

        [Fact]
        public void CloneTest()
        {
            var exp = new Mod(new Number(5), Number.Zero);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}