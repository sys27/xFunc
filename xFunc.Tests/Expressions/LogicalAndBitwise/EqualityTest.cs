// Copyright 2012-2018 Dmitry Kischenko
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

    public class EqualityTest
    {

        [Fact]
        public void ExecuteTest1()
        {
            var eq = new Equality(new Bool(true), new Bool(true));

            Assert.True((bool)eq.Execute());
        }

        [Fact]
        public void ExecuteTest2()
        {
            var eq = new Equality(new Bool(true), new Bool(false));

            Assert.False((bool)eq.Execute());
        }

        [Fact]
        public void ExecuteResultIsNotSupported()
        {
            var eq = new Equality(new Number(1), new Number(2));

            Assert.Throws<ResultIsNotSupportedException>(() => eq.Execute());
        }

        [Fact]
        public void CloneTest()
        {
            var exp = new Equality(new Bool(true), new Bool(false));
            var clone = exp.Clone();

            Assert.Equal(exp, clone);
        }

    }

}
