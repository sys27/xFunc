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
using xFunc.Maths.Expressions.LogicalAndBitwise;
using Xunit;

namespace xFunc.Tests.Expressions.LogicalAndBitwise
{
    public class ImplicationTest
    {
        [Fact]
        public void ExecuteTest1()
        {
            var impl = new Implication(Bool.True, Bool.False);

            Assert.False((bool) impl.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var impl = new Implication(Bool.True, Bool.True);

            Assert.True((bool) impl.Execute());
        }

        [Fact]
        public void ExecuteResultIsNotSupported()
        {
            var exp = new Implication(new Number(1), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Implication(Bool.True, Bool.False);
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }
    }
}