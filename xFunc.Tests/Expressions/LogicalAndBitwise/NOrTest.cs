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

    public class NOrTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var nor = new NOr(new Bool(false), new Bool(true));

            Assert.False((bool)nor.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var nor = new NOr(new Bool(false), new Bool(false));

            Assert.True((bool)nor.Execute());
        }

        [Fact]
        public void ExecuteResultIsNotSupported()
        {
            var exp = new NOr(new Number(1), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => exp.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new NOr(new Bool(true), new Bool(false));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}